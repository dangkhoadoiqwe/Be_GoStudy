using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Net.payOS.Types;
using Net.payOS;
using Microsoft.EntityFrameworkCore;
using GO_Study_Logic.ViewModel.ZaloPay;
using GO_Study_Logic.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
namespace GO_Study_Logic.Service.VNPAY
{
    public interface IPaymentService
    {
        Task<PaymentTransaction> InsertPaymentTransactionAsync(PaymentTransactionViewModel dto);
        Task<bool> UpdatePaymentTransactionStatusAsync(int transactionId, string status);
        string GenerateQueryString(VnPayReturnRequest request);
        Task<bool> UpdatePaymentTransactionStatusByOrderCodeAsync(string orderCode, string status);
       
        Task AddTransactionAsync(PaymentTransaction transaction);
        Task<PaymentTransaction> GetTransactionByIdAsync(int id);

        Task<PaymentTransaction> GetTransactionByOrderCodeAsync(string orderCode);

        Task<PaymentStatusViewModel> GetPaymentByPaymentRefefid(string paymentRefId);
        Task<CheckoutPayment> Checkout(int userId, int packageId);
        Task<bool> DeletePaymentTransactionByPaymentRefIdAsync(string paymentRefId);
    }

    public class PaymentService : IPaymentService
    {
        private readonly GOStudyContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IUserRepository _userRepository;
        private const string ApiUrl = "https://api.payos.com/payment";
        private const string ClientId = "4aa34269-86b3-4b50-8272-c052e7f8fe2e";
        private const string ApiKey = "90ef19a8-9071-4413-8944-7afed876ae64";
        private const string ChecksumKey = "41c2d708bbbdf66f67008b3c8f8e06e6789a0cb07a2304a81e4e6659d11a4fc4";

        public PaymentService(GOStudyContext context, IPaymentTransactionRepository paymentTransactionRepository,
            IHttpClientFactory httpClientFactory, IPackageRepository package, IUserRepository user)
        {
            _context = context;
            _paymentTransactionRepository = paymentTransactionRepository;
            _httpClientFactory = httpClientFactory;
            _packageRepository = package;
            _userRepository = user;
        }

        public async Task<bool> DeletePaymentTransactionByPaymentRefIdAsync(string paymentRefId)
        {
            // Kiểm tra và lấy thông tin PaymentTransaction từ repository
            var paymentTransaction = await _paymentTransactionRepository.GetPaymentByPaymentRefId(paymentRefId);

            if (paymentTransaction == null)
            {
                // Nếu không tìm thấy, trả về false
                return false;
            }

            // Xóa PaymentTransaction khỏi repository
            _paymentTransactionRepository.DeletePaymentByPaymentRefId(paymentTransaction.PaymentRefId);

            // Lưu thay đổi vào context
            await _context.SaveChangesAsync();

            // Trả về true nếu thành công
            return true;
        }

        private string GenerateChecksum(string data, string key)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public async Task<PaymentTransaction> GetTransactionByOrderCodeAsync(string orderCode)
        {
            // Tìm giao dịch trong cơ sở dữ liệu dựa trên PaymentRefId (orderCode)
            return await _context.PaymentTransactions.FirstOrDefaultAsync(pt => pt.PaymentRefId == orderCode);
        }
        public async Task<bool> UpdatePaymentTransactionStatusByOrderCodeAsync(string orderCode, string status)
        {
            var paymentTransaction = await _context.PaymentTransactions
                .FirstOrDefaultAsync(pt => pt.PaymentRefId == orderCode); // Giả sử PaymentRefId là OrderCode

            if (paymentTransaction == null)
            {
                return false; // Không tìm thấy giao dịch
            }

            paymentTransaction.Status = status; // Cập nhật trạng thái
            _context.PaymentTransactions.Update(paymentTransaction);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PaymentTransaction> GetTransactionByIdAsync(int id)
        {
            return await _paymentTransactionRepository.GetTransactionByIdAsync(id);
        }

        public async Task AddTransactionAsync(PaymentTransaction transaction)
        {
            await _paymentTransactionRepository.AddTransactionAsync(transaction);
        }

       

        public async Task<PaymentTransaction> InsertPaymentTransactionAsync(PaymentTransactionViewModel dto)
        {
            var paymentTransaction = new PaymentTransaction
            {
                UserId = dto.UserId,
                PackageId = dto.PackageId,
                TransactionDate = DateTime.UtcNow,
                PaymentMethod = dto.PaymentMethod,
                Amount = dto.Amount,
                Status = "Pending"
            };

            _context.PaymentTransactions.Add(paymentTransaction);
            await _context.SaveChangesAsync();

            return paymentTransaction;
        }

        public async Task<bool> UpdatePaymentTransactionStatusAsync(int transactionId, string status)
        {
            var paymentTransaction = await _context.PaymentTransactions.FindAsync(transactionId);

            if (paymentTransaction == null)
            {
                return false;
            }

            paymentTransaction.Status = status;
            _context.PaymentTransactions.Update(paymentTransaction);
            await _context.SaveChangesAsync();

            return true;
        }

        public string GenerateQueryString(VnPayReturnRequest request)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "vnp_Amount", request.vnp_Amount },
                { "vnp_BankCode", request.vnp_BankCode },
                { "vnp_BankTranNo", request.vnp_BankTranNo },
                { "vnp_CardType", request.vnp_CardType },
                { "vnp_OrderInfo", request.vnp_OrderInfo },
                { "vnp_PayDate", request.vnp_PayDate },
                { "vnp_ResponseCode", request.vnp_ResponseCode },
                { "vnp_TmnCode", request.vnp_TmnCode },
                { "vnp_TransactionNo", request.vnp_TransactionNo },
                { "vnp_TransactionStatus", request.vnp_TransactionStatus },
                { "vnp_TxnRef", request.vnp_TxnRef }
            };

            var queryString = string.Join("&", queryParams.OrderBy(q => q.Key).Select(q => $"{q.Key}={q.Value}"));
            return queryString;
        }

        public async Task<CheckoutPayment> Checkout(int userId, int packageId)
        {
            // Tạo một đối tượng CheckoutPayment để trả về
            var checkoutPayment = new CheckoutPayment();

            // Lấy thông tin người dùng
            var user = await _userRepository.GetByIdAsync(userId); // Bạn cần có UserRepository
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Chuyển đổi thông tin người dùng sang UserViewModel
            checkoutPayment.user = new UserViewModel
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                PasswordHash = user.PasswordHash // Cẩn thận với việc lưu trữ PasswordHash
            };

            // Lấy thông tin gói
            var package = await _packageRepository.GetPackageByIdAsync(packageId);
            if (package == null)
            {
                throw new Exception("Package not found.");
            }

            // Chuyển đổi thông tin gói sang PackageViewModel
            checkoutPayment.package = new PackageViewModel
            {
                PackageId = package.PackageId,
                Name = package.Name,
                Price = package.Price,
             //   Features = package.Features
            };

            return checkoutPayment;
        }

        public async Task<PaymentStatusViewModel> GetPaymentByPaymentRefefid(string paymentRefId)
        {
            // Thực hiện truy vấn dữ liệu bằng cách tìm kiếm theo PaymentRefId
            var paymentTransaction = await _context.PaymentTransactions
                .FirstOrDefaultAsync(pt => pt.PaymentRefId == paymentRefId);

            // Nếu không tìm thấy giao dịch, ném ngoại lệ
            if (paymentTransaction == null)
            {
                throw new Exception("Payment transaction not found.");
            }

            // Trả về đối tượng PaymentStatusViewModel
            return new PaymentStatusViewModel
            {
                PaymentRefId = paymentTransaction.PaymentRefId,
                Status = paymentTransaction.Status
            };
        }

    }
}

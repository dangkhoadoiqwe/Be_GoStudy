using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IPaymentTransactionRepository
    {
        Task<IEnumerable<PaymentTransaction>> GetAllTransactionsAsync();
        Task<PaymentTransaction> GetTransactionByIdAsync(int id);
        Task AddTransactionAsync(PaymentTransaction transaction);
        Task UpdateTransactionAsync(PaymentTransaction transaction);
        Task DeleteTransactionAsync(int id);
    }

    public class PaymentTransactionRepository : IPaymentTransactionRepository
    {
        private readonly GOStudyContext _context;

        public PaymentTransactionRepository(GOStudyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentTransaction>> GetAllTransactionsAsync()
        {
            return await _context.PaymentTransactions.ToListAsync();
        }

        public async Task<PaymentTransaction> GetTransactionByIdAsync(int id)
        {
            return await _context.PaymentTransactions.FindAsync(id);
        }

        public async Task AddTransactionAsync(PaymentTransaction transaction)
        {
            await _context.PaymentTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransactionAsync(PaymentTransaction transaction)
        {
            _context.PaymentTransactions.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _context.PaymentTransactions.FindAsync(id);
            if (transaction != null)
            {
                _context.PaymentTransactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }
    }
}
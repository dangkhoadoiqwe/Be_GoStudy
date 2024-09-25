using DataAccess.Model;
using Quartz;
using Quartz.Impl;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;  // Để sử dụng Include

namespace GOStudy_Logic.Service.Sentmail
{
    public class TaskReminderJob : IJob
    {
        private readonly GOStudyContext _context;

        // Nhận GOStudyContext thông qua DI
        public TaskReminderJob(GOStudyContext context)
        {
            _context = context;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                // Lấy danh sách các task mà ScheduledTime <= thời gian hiện tại và chưa hoàn thành, đồng thời lấy thông tin user
                var pendingTasks = await _context.Tasks
                    .Include(t => t.User)  // Lấy thông tin User cùng với Task
                    .Where(t => t.ScheduledTime == DateTime.Now && t.Status != "Completed" && t.IsDeleted == true)
                    .ToListAsync();

                foreach (var task in pendingTasks)
                {
                    var user = task.User;

                    if (user != null)
                    {
                        // Tạo nội dung thông báo
                        string message = $"Chào {user.FullName},\n\nBạn có nhiệm vụ \"{task.Title}\" đã đến hạn hoàn thành. \n\nCảm ơn!";

                        // Gửi email
                        SendEmail(user.Email, "Nhắc nhở nhiệm vụ", message);

                        // Lưu thông báo vào bảng Notifications
                        _context.Notifications.Add(new Notification
                        {
                            TaskId = task.TaskId,
                            UserId = user.UserId,
                            Message = message,
                            SentAt = DateTime.Now
                        });
                        await _context.SaveChangesAsync();
                    }
                }

                Console.WriteLine("TaskReminderJob executed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TaskReminderJob failed: {ex.Message}");
            }
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            var fromAddress = new MailAddress("khoapdse161076@fpt.edu.vn", "Task Reminder");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "mxgw vzpr uupu owwd";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",  // SMTP server của Gmail
                Port = 587,  // Cổng SMTP cho Gmail với TLS
                EnableSsl = true,  // Bật SSL
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                try
                {
                    smtp.Send(message);
                    Console.WriteLine($"Email sent to {toEmail}.");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine($"Failed to send email to {toEmail}. Error: {ex.Message}");
                }
            }
        }
    }
}

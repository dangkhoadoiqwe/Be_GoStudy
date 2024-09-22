using DataAccess.Model;
using MailKit.Net.Smtp;
using MimeKit;

namespace GOStudy_Logic.Service;

public class ContactInfoService
{
    private readonly string _emailFrom = "nguyenthaitoanphuc156@gmail.com"; // Email của bạn
    private readonly string _smtpHost = "smtp.gmail.com";        // Sử dụng SMTP của Gmail
    private readonly int _smtpPort = 587;                        // Cổng SMTP
    private readonly string _smtpUser = "nguyenthaitoanphuc156@gmail.com";  // Email của bạn
    private readonly string _smtpPass = "pcrm xzok bwwa vltj";   // Mật khẩu ứng dụng hoặc mật khẩu của bạn

    public async Task SendContactEmailAsync(ContactInfo contactInfo)
    {
        // Tạo đối tượng email
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Website Feedback", _emailFrom));
        message.To.Add(new MailboxAddress("Your Name", _emailFrom)); // Email nhận phản hồi
        message.Subject = "New Feedback from Website";

        // Nội dung email
        var bodyBuilder = new BodyBuilder
        {
            TextBody = $@"
                Contact Name: {contactInfo.ContactName}
                Email: {contactInfo.Email}
                Phone: {contactInfo.ContactPhone}
                Address: {contactInfo.StreetAddress}, {contactInfo.City}
                Feedback Content: {contactInfo.Content}
            "
        };

        // Đính kèm file nếu có
        if (!string.IsNullOrEmpty(contactInfo.UploadedFilePath))
        {
            bodyBuilder.Attachments.Add(contactInfo.UploadedFilePath);
        }

        message.Body = bodyBuilder.ToMessageBody();

        // Kết nối với SMTP và gửi email
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_smtpHost, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpUser, _smtpPass);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
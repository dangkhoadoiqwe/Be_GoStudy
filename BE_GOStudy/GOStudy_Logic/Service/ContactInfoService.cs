using DataAccess.Model;
using MailKit.Net.Smtp;
using MimeKit;

namespace GOStudy_Logic.Service;

public class ContactInfoService
{
    private readonly string _emailFrom = "gostudy.go01@gmail.com"; 
    private readonly string _smtpHost = "smtp.gmail.com";    
    private readonly int _smtpPort = 587;                       
    private readonly string _smtpUser = "gostudy.go01@gmail.com";  
    private readonly string _smtpPass = "acxs pzsr pobw mfjs";  

    public async Task SendContactEmailAsync(ContactInfo contactInfo)
    {
        // Tạo đối tượng email
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Website Feedback", _emailFrom));
        message.To.Add(new MailboxAddress("Your Name", _emailFrom));
        message.Subject = "New Feedback from Website";

        // Nội dung email
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
        <html>
            <body>
                <h2>New Feedback Submission</h2>
                <p><strong>Contact Name:</strong> {contactInfo.ContactName}</p>
                <p><strong>Email:</strong> {contactInfo.Email}</p>
                <p><strong>Phone:</strong> {contactInfo.ContactPhone}</p>
                <p><strong>Address:</strong> {contactInfo.StreetAddress}, {contactInfo.City}</p>
                <p><strong>Feedback Content:</strong></p>
                <p>{contactInfo.Content}</p>
            </body>
        </html>
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

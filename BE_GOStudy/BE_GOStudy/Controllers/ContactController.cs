using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using DataAccess.Model;
using GOStudy_Logic.Service;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly ContactInfoService _contactInfoService;

    public ContactController(ContactInfoService contactInfoService)
    {
        _contactInfoService = contactInfoService;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitFeedback([FromForm] ContactInfo contactInfo)
    {
        // Kiểm tra xem file có được tải lên không
        if (contactInfo.File != null)
        {
            // Lưu file vào thư mục và lấy đường dẫn
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Đặt tên file để lưu
            var filePath = Path.Combine(uploadsFolder, contactInfo.File.FileName);
        
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await contactInfo.File.CopyToAsync(stream); // Sử dụng contactInfo.File ở đây
            }

            // Lưu đường dẫn file vào UploadedFilePath
            contactInfo.UploadedFilePath = filePath; // Đường dẫn file đã lưu
        }

        // Gửi email với thông tin phản hồi
        await _contactInfoService.SendContactEmailAsync(contactInfo);
    
        return Ok();
    }

}
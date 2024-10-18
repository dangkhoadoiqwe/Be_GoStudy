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
        if (contactInfo.File != null)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var filePath = Path.Combine(uploadsFolder, contactInfo.File.FileName);
        
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await contactInfo.File.CopyToAsync(stream);
            }
            contactInfo.UploadedFilePath = filePath; 
        }
        await _contactInfoService.SendContactEmailAsync(contactInfo);
    
        return Ok();
    }

}
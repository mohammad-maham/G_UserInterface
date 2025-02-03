using G_UserInterface.Models;
using GoldHelpers.Helpers;
using GoldHelpers.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace G_UserInterface.Controllers;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;

[Route("api/[controller]")]
[GoldAuthorize]
[ApiController]
public class BannerController : ControllerBase
{
    private readonly GUserInterfaceDbContext _context;
    private readonly IWebHostEnvironment _env;

    public BannerController(GUserInterfaceDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // GET: api/Banner
    [HttpGet("GetBanner")]
    public async Task<IActionResult> GetBanner([FromQuery] int? bannerLocationId)
    {
        var banners = _context.Banners.AsQueryable();

        if (bannerLocationId.HasValue)
        {
            banners = banners.Where(b => b.BannerLocation == bannerLocationId.Value);
        }

        return Ok(new GApiResponse<List<Banner>>
        {
            Data = await banners.ToListAsync()
        });
        
      
    }

    // POST: api/Banner
    [HttpPost("CreateBanner")]
    public async Task<IActionResult> CreateBanner([FromForm] IFormFile file, [FromForm] string name, [FromForm] string? description, [FromForm] int bannerLocation, [FromForm] long regUserId)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is required.");

       
        // Ensure WebRootPath is set
        var webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        // Ensure directory exists
        var uploadPath = Path.Combine(webRootPath, "uploads");
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);
        
        

        // Generate GUID filename and save the file
        string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        string filePath = Path.Combine(uploadPath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Save banner details in the database
        var banner = new Banner
        {
            Name = name,
            Description = description,
            Status = 1, // Active by default
            //RegDate = NodaTime.LocalTime.FromTimeOnly(DateTime.Now.ti),
            BannerLocation = bannerLocation,
            ImagePath = Path.Combine("uploads", uniqueFileName),
            RegUserId = regUserId
        };

        _context.Banners.Add(banner);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBanner), new { id = banner.Id }, banner);
    }

    // PUT: api/Banner/{id}
    [HttpPut("UpdateBanner/{id}")]
    public async Task<IActionResult> UpdateBanner(int id, [FromBody] Banner updatedBanner)
    {
        var banner = await _context.Banners.FindAsync(id);
        if (banner == null)
            return NotFound();

        banner.Name = updatedBanner.Name;
        banner.Description = updatedBanner.Description;
        banner.BannerLocation = updatedBanner.BannerLocation;

        _context.Banners.Update(banner);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/Banner/ChangeStatus/{id}
    [HttpPatch("ChangeStatusBanner/{id}")]
    public async Task<IActionResult> ChangeStatusBanner(int id)
    {
        var banner = await _context.Banners.FindAsync(id);
        if (banner == null)
            return NotFound();

        banner.Status = banner.Status == 1 ? 0 : 1; // Toggle status

        _context.Banners.Update(banner);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}




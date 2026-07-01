using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraineeManagementApi.DTO.SubmissionFileDTO;
using TraineeManagementApi.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;

namespace TraineeManagementApi.Controllers;

[ApiController]

public class FileSubmissionController: ControllerBase
{
    private readonly IFileStorageService _filestorageservice;
    private readonly ILogger<FileSubmissionController> _logger;
   
    public FileSubmissionController(IFileStorageService fileStorageService,ILogger<FileSubmissionController> logger)
    {
        _filestorageservice=fileStorageService;
        _logger=logger;
    }
    
    [Authorize]
    [HttpPost("api/submission/{Id}/files")]
    public async Task<IActionResult> SaveAsync([FromRoute] Guid Id,IFormFile file,CancellationToken cancellationToken=default)
    {
         if (!Request.ContentType?.StartsWith("multipart/form-data") ?? true)
            {
                return BadRequest("The request does not contain valid multipart form data.");
            }
        string userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!ModelState.IsValid)
            {
                throw new BadRequestException("Invalid Data");
            }
        if (!Guid.TryParse(userId,out Guid UploadedBy_Id))
        {
            throw new UnauthorizedAccessException();
        }
        Guid response  = await _filestorageservice.SaveAsync(file,Submission_Id:Id,UploadedBy_Id,cancellationToken);
        _logger.LogInformation($"Files uploaded with submission Id: {Id}");
        return Created($"/api/submission/{Id}/files",new ApiResponse<Guid>
        {
            success=true,
            message="File Added To Queued with Tracking Id",
            Data=response
        });

       



    }

    [Authorize]
    [HttpGet("api/submission-files/{Id}/download")]
    public async Task<IActionResult> DownloadAsync([FromRoute] Guid Id,CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            {
                throw new BadRequestException("Invalid Data");
            }
        DonwloadFileResponseDTO response = await  _filestorageservice.DownloadAsync(Id,cancellationToken);
        return File(response.filestream,response.ContentType,response.downloadName);
        
    }

    [Authorize]
    [HttpDelete("api/submission-files/{Id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute]Guid Id,CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            {
                throw new BadRequestException("Invalid Data");
            }
        bool response = await _filestorageservice.DeleteAsync(Id,cancellationToken);
        return NoContent();
    }



}
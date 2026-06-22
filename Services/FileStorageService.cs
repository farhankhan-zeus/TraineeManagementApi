
using System.Net;
using System.Security.Cryptography;
using TraineeManagementApi.Context;
using TraineeManagementApi.DTO.SubmissionFileDTO;
using TraineeManagementApi.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;
using TraineeManagementApi.utils;

namespace TraineeManagementApi.Services;

public class FileStorageService : IFileStorageService
{
    private readonly ILogger<FileStorageService> _logger;
     private readonly IConfiguration _config;
     private readonly ApiContext _context;
    

    
    
    public FileStorageService(ILogger<FileStorageService> logger,IConfiguration configuration, ApiContext context )
    {
        _logger=logger;
        _config=configuration;
        _context= context;
    }

   
    private string[] permittedExtensions = { ".txt", ".pdf" };
    public async Task<List<SubmissionFileResponseDTO>> SaveAsync (IFormFileCollection files,Guid Submission_Id,Guid UploadedBy_Id,CancellationToken cancellationToken )
    {
        List<SubmissionFile> submissionFiles = new List<SubmissionFile>();
        
       foreach(IFormFile file in files)
        {
             if (file.Length == 0)
        {
            throw new BadRequestException("File is required");
        }   
        if (file.Length > 2097152)
        {
            throw new  BadRequestException("File is too large");
        }
        
        string unTrustedFileName = file.FileName;
        string TrustedFileNamefordisplay = WebUtility.HtmlEncode(file.FileName);
        var ext = Path.GetExtension(unTrustedFileName).ToLowerInvariant();
        if (!permittedExtensions.Contains(ext))
        {
            throw new  BadRequestException("File Type not supported");
        }
      
        string GeneratedFileName = Path.GetRandomFileName();
        string filePath = Path.Combine(_config["Storage:path"],GeneratedFileName);
       FileStream newFile = File.Create(filePath);

        await file.CopyToAsync(newFile,cancellationToken);

        byte[] hash = await SHA256.HashDataAsync(file.OpenReadStream(),cancellationToken);
        

        SubmissionFile submissionFile = new SubmissionFile
        {
            orignalName=unTrustedFileName,
            generatedName=GeneratedFileName,
            ContentType=file.ContentType,
            size=file.Length,
            UploadedById=UploadedBy_Id,
            CreatedAt=DateTime.Now,
            UpdatedAt= DateTime.Now,
            checksum= Convert.ToHexString(hash),
            SubmissionId=Submission_Id


        };
        _logger.LogInformation($"File with Id: {submissionFile.Id} for submission with Id: {submissionFile.SubmissionId} added to storage",submissionFile.Id,submissionFile.SubmissionId);
            submissionFiles.Add(submissionFile);

        }

        await _context.SubmissionFiles.AddRangeAsync(submissionFiles);
        await _context.SaveChangesAsync();
        return submissionFiles.Select(ResponseDTOMapper.MaptoSubmissionFile).ToList();
        



    }


    public async Task<DonwloadFileResponseDTO> DownloadAsync (Guid Id,CancellationToken cancellationToken)
    {
        SubmissionFile? file = await _context.SubmissionFiles.FindAsync(Id,cancellationToken);
        if(file is null)
        {
            throw new NotFoundException("File",Id);
        }
        string filepath=Path.Combine(_config["Storage:path"], file.generatedName);
        if (!File.Exists(filepath))
        {
             _logger.LogWarning($"File with Id:{Id} doesnt exist in storage",Id);
           throw new NotFoundException("File",Id);
        }
        FileStream fileStream = File.OpenRead(filepath);
        return new DonwloadFileResponseDTO{
            filestream=fileStream,
            ContentType=file.ContentType,
            downloadName=file.orignalName

        };


    }


    public async Task<bool> ExistAsync (Guid Id,CancellationToken cancellationToken)
    {
         SubmissionFile? file = await _context.SubmissionFiles.FindAsync(Id,cancellationToken);
        if(file is null)
        {
           return false;
        }
        string filepath=Path.Combine(_config["Storage:path"], file.generatedName);
        if (!File.Exists(filepath))
        {
             _logger.LogWarning($"File with Id:{Id} doesnt exist in storage",Id);
           return false;
        }
        return true;
        
    }


    public async Task<bool> DeleteAsync(Guid Id, CancellationToken cancellationToken)
    {
         SubmissionFile? file = await _context.SubmissionFiles.FindAsync(Id,cancellationToken);
        if(file is null)
        {
            throw new NotFoundException("File",Id);
        }
        string filepath=Path.Combine(_config["Storage:path"], file.generatedName);
        if (!File.Exists(filepath))
        {
            _logger.LogWarning($"File with Id:{Id} doesnt exist in storage",Id);
            throw new NotFoundException("File",Id);
        }
        File.Delete(filepath);
        _context.SubmissionFiles.Remove(file);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"File with Id:{Id} deleted",Id);
        return true;
    }

}

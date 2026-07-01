using TraineeManagementApi.DTO.SubmissionFileDTO;

namespace TraineeManagementApi.Services.Interfaces;

public  interface IFileStorageService
{
    Task<Guid> SaveAsync (IFormFile file,Guid Submission_Id,Guid UploadedBy_Id,CancellationToken cancellationToken );
   Task<DonwloadFileResponseDTO> DownloadAsync (Guid Id,CancellationToken cancellationToken);
    
    Task<bool> ExistAsync (Guid Id, CancellationToken cancellationToken);

    Task<bool> DeleteAsync (Guid Id ,CancellationToken cancellationToken);

}
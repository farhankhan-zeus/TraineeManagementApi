
using TraineeManagementApi.DTO.LearningTaskDTO;
using TraineeManagementApi.DTO.MentorDTO;
using TraineeManagementApi.DTO.ReviewDTO;
using TraineeManagementApi.DTO.SubmissionDTO;
using TraineeManagementApi.DTO.SubmissionFileDTO;
using TraineeManagementApi.DTO.TaskAssignmentDTO;
using TraineeManagementApi.DTO.TraineeDTO;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.utils;

public static class ResponseDTOMapper
{
     public static TraineeResponseDTO MapTraineetoDTO (Trainee trainee){
        return new TraineeResponseDTO{
            Id = trainee.Id,
            FirstName=trainee.FirstName,
            LastName=trainee.LastName,
            Email=trainee.Email,
            TechStack=trainee.TechStack,
            Status = trainee.Status,
           

        };
    }
    public static MentorResponseDTO MapMentorToDTO (Mentor mentor)
    {
        return new MentorResponseDTO
        {
            Id= mentor.Id,
            FirstName = mentor.FirstName,
            LastName = mentor.LastName,
            Email = mentor.Email,
            Expertise = mentor.Expertise,
            Status = mentor.Status
        };

    }
     public static LearningTaskResponseDTO MapTaskToDTO (LearningTask task)
    {
        return new LearningTaskResponseDTO
        {
           Id = task.Id,
           Title= task.Title,
           Descriptiion = task.Descriptiion,
           ExpectedTechStack = task.ExpectedTechStack,
           Status = task.Status,
           DueDate=task.DueDate
        };

    }
    

    public  static TaskAssignmentResponseDTO MaptoTaskAssignmentResponse(TaskAssignment task)
    {
        TaskAssignmentResponseDTO response=new TaskAssignmentResponseDTO{
        Id = task.Id,
        TraineeId=task.TraineeId,
        MentorId = task.MentorId,
        LearningTaskId = task.LearningTaskId,
        Status = task.Status,
        AssignDate= task.AssignDate,
        DueDate= task.DueDate,
       

    };
        if(task.Remarks != null)
        {
            response.Remarks = task.Remarks;
        }
        if(task.Mentor != null)
        {
        response.Mentor= MapMentorToDTO(task.Mentor);
        response.LearningTask=MapTaskToDTO(task.LearningTask);
        response.Trainee=MapTraineetoDTO(task.Trainee);
            
        }
      
        return response;
    }
    public static  SubmissionResponseDTO MaptoSubmissionResponse( Submission submit)
    {
        SubmissionResponseDTO response=new SubmissionResponseDTO{
        Id = submit.Id,
        TaskAssignmentId = submit.TaskAssignmentId,
        SubmissionUrl = submit.SubmissionUrl,
        SubmittedDate = submit.SubmittedDate,
        Status = submit.Status,

    };
        if(submit.Notes != null)
        {
            response.Notes = submit.Notes;
        }
        if (submit.TaskAssignment != null)
        {
            response.TaskAssignment = MaptoTaskAssignmentResponse(submit.TaskAssignment);
        }
        return response;
    }

    public static ReviewResponseDTO MaptoReviewResponse( Review review)
    {
        ReviewResponseDTO response=new ReviewResponseDTO{
        Id = review.Id,
        SubmissionId = review.SubmissionId,
        MentorId = review.MentorId,
        Feedback = review.Feedback,
        ReviewedDate = review.ReviewedDate,
        ReviewedStatus=review.ReviewedStatus,

    };
        if(review.Score != null)
        {
            response.Score = review.Score;
        }
        if(review.Mentor != null && review.Submission!=null)
        {
            response.Mentor= MapMentorToDTO(review.Mentor);
            response.Submission=MaptoSubmissionResponse(review.Submission);
        }
        return response;
    }

     public static SubmissionFileResponseDTO MaptoSubmissionFile(SubmissionFile submissionfile)
    {
        SubmissionFileResponseDTO response= new SubmissionFileResponseDTO
        {
            Id=submissionfile.Id,
            checksum=submissionfile.checksum,
            orignalName=submissionfile.orignalName,
            generatedName=submissionfile.generatedName,
            size=submissionfile.size,
            UpdatedAt=submissionfile.UpdatedAt,
            CreatedAt=submissionfile.UpdatedAt,
            UploadedById=submissionfile.UploadedById,
            SubmissionId=submissionfile.SubmissionId,
            ContentType=submissionfile.ContentType,
        };
        if (submissionfile.Submission !=null)
        {
            // response.UploadedBy= MapTraineetoDTO(submissionfile.UploadedBy);
            response.Submission=MaptoSubmissionResponse(submissionfile.Submission);
        }
        return response;
            
    }
}
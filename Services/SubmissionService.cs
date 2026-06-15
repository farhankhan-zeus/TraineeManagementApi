

using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Context;
using TraineeManagementApi.DTO.SubmissionDTO;
using TraineeManagementApi.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;

namespace TraineeManagementApi.Services;

public class SubmissionService : ISubmissionService
{
    private readonly ApiContext _context;
    private readonly ILogger<SubmissionService> _logger;

    public SubmissionService(ApiContext context, ILogger<SubmissionService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public SubmissionResponseDTO MaptoSubmissionResponse( Submission submit)
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
        return response;
    }

     public async Task<List<SubmissionResponseDTO?>> Getall()
    {
        try
        {
            List<Submission>? submissions = await _context.Submissions.ToListAsync();
            if(submissions.Count == 0)
            {
                return [];
            }
            List<SubmissionResponseDTO?> result =submissions.Select(MaptoSubmissionResponse).ToList();
            return result;
            


        }
        catch (Exception e)
        {
            _logger.LogWarning("Failed to get the submissions");
            throw new Exception("Error getting the submissions",e);
        }
    }

    public async Task<SubmissionResponseDTO?> GetById(Guid Id)
    {
        try
        {
            Submission? submission = await _context.Submissions.FindAsync(Id);
            if(submission == null)
            {
                throw new NotFoundException("Submission",Id);
            }
            return MaptoSubmissionResponse(submission);
        }
        catch (Exception e)
        {
            _logger.LogWarning($"failed to retrive submission with Id: {Id}",Id);
            throw new Exception("Failed to Retrive submission",e);
        }
    }

    public async Task<SubmissionResponseDTO> AddSubmission(CreateorUpdateSubmissionRequestDTO submission)
       
        
    {
        try
        {
        TaskAssignment? existtask = await _context.TaskAssignments.FindAsync(submission.TaskAssignmentId);
        if(existtask == null)
        {
            throw new NotFoundException("Task Assignment",submission.TaskAssignmentId);
        }
            Submission newsubmission = new Submission
            {
             
                TaskAssignmentId = submission.TaskAssignmentId,
                Status= "Submitted",
                SubmissionUrl = submission.SubmissionUrl,
                SubmittedDate = DateTime.Now,
            };
            if (submission.Notes != null)
            {
               newsubmission.Notes = submission.Notes;
            }
            await _context.Submissions.AddAsync(newsubmission);
            await _context.SaveChangesAsync();
            return MaptoSubmissionResponse(newsubmission);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Failed to add submission");
            throw new Exception("Failed to add submission",e);
        }
        
    }

    public async Task<SubmissionResponseDTO?> UpdateSubmission(Guid Id,CreateorUpdateSubmissionRequestDTO submission)
    {
        try
        {
            Submission? existsubmission = await _context.Submissions.FindAsync(Id);
            if(existsubmission == null)
            {
                throw new NotFoundException("Submission",Id);
            }

                
           
                existsubmission.TaskAssignmentId = submission.TaskAssignmentId;
                existsubmission.Status="Resubmitted";
                existsubmission.SubmissionUrl = submission.SubmissionUrl;
                existsubmission.SubmittedDate = DateTime.Now;
           
            if (submission.Notes != null)
            {
               existsubmission.Notes = submission.Notes;
            }
            await _context.SaveChangesAsync();
            return MaptoSubmissionResponse(existsubmission);

        }
        catch (Exception e)
        {
            _logger.LogWarning($"Failed to update Task with Id: {Id}",Id);
            
            throw new Exception("Failed to update Taskassignment",e);
        }
    }

  






}
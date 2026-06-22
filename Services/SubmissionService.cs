

using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Context;
using TraineeManagementApi.DTO.SubmissionDTO;
using TraineeManagementApi.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;
using TraineeManagementApi.utils;

namespace TraineeManagementApi.Services;

public class SubmissionService : ISubmissionService
{
    private readonly ApiContext _context;
    private readonly IRedisService _redisservice;
    private readonly ILogger<SubmissionService> _logger;

    public SubmissionService(ApiContext context,IRedisService redisService, ILogger<SubmissionService> logger)
    {
        _context = context;
        _logger = logger;
        _redisservice = redisService;
    }

    

     public async Task<List<SubmissionResponseDTO>> Getall(CancellationToken cancellationToken)
    {
       
            List<Submission>? submissions = await _context.Submissions.Include(t=>t.TaskAssignment).ToListAsync();
            if(submissions.Count == 0)
            {
                return [];
            }
            List<SubmissionResponseDTO> result =submissions.Select(ResponseDTOMapper.MaptoSubmissionResponse).ToList();
            return result;

    }

    public async Task<SubmissionResponseDTO> GetById(Guid Id,CancellationToken cancellationToken)
    {   
        string stringId= Convert.ToString(Id);
            if(stringId is null)
        {
            throw new BadRequestException("valid Id is required");
            
        }
        
        string _keyPrefix = $"{typeof(Submission).Name}:";
            string validKey= _keyPrefix+":"+stringId;
        
        Submission? submission = await _redisservice.GetorSetAsync(validKey,async () =>
        {
            
            Submission? submission =  _context.Submissions.Include(t=>t.TaskAssignment).Where(t=>t.Id==Id).FirstOrDefault();
            return submission;
        },cancellationToken);
      
            if(submission == null)
            {
                throw new NotFoundException("Submission",Id);
            }
            return ResponseDTOMapper.MaptoSubmissionResponse(submission);
       
    }

    public async Task<SubmissionResponseDTO> AddSubmission(CreateorUpdateSubmissionRequestDTO submission,CancellationToken cancellationToken)
       
        
    {
       
        TaskAssignment? existtask = await _context.TaskAssignments.FindAsync(submission.TaskAssignmentId,cancellationToken);
        if(existtask == null)
        {
            throw new NotFoundException("Task Assignment",submission.TaskAssignmentId);
        }
        Submission? existsubmint =  _context.Submissions.Where(t=>t.TaskAssignmentId==submission.TaskAssignmentId).FirstOrDefault();
        if(existsubmint != null)
        {
            throw new BadRequestException("Submission already exist for this task");
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
            await _context.SaveChangesAsync(cancellationToken);
            
            return ResponseDTOMapper.MaptoSubmissionResponse(newsubmission);
        
        
    }

    public async Task<SubmissionResponseDTO> UpdateSubmission(Guid Id,CreateorUpdateSubmissionRequestDTO submission,CancellationToken cancellationToken)
    {
        string stringId= Convert.ToString(Id);
            if(stringId is null)
        {
            throw new BadRequestException("valid Id is required");
        }
       
            Submission? existsubmission = await _context.Submissions.FindAsync(Id,cancellationToken);
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
            await _context.SaveChangesAsync(cancellationToken);
             string _keyPrefix = $"{typeof(Submission).Name}:";
            string validKey= _keyPrefix+":"+stringId;
            await _redisservice.InvalidateAsync<Submission>(validKey,cancellationToken);
       
            return ResponseDTOMapper.MaptoSubmissionResponse(existsubmission);

       
    }

  






}
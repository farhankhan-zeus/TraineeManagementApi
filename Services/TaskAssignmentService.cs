

using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Context;
using TraineeManagementApi.DTO.TaskAssignmentDTO;
using TraineeManagementApi.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;

namespace TraineeManagementApi.Services;

public class TaskAssignmentService : ITaskAssignmentService
{
    private readonly ApiContext _context;
    private readonly ILogger<TaskAssignmentService> _logger;

    public TaskAssignmentService(ApiContext context, ILogger<TaskAssignmentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public TaskAssignmentResponseDTO MaptoTaskAssignmentResponse( TaskAssignment task)
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
        return response;
    }

     public async Task<List<TaskAssignmentResponseDTO?>> Getall()
    {
        try
        {
            List<TaskAssignment>? tasks = await _context.TaskAssignments.Include(t=>t.Trainee).Include(t=>t.LearningTask).Include(t=>t.Mentor).ToListAsync();
            if(tasks.Count == 0)
            {
                return [];
            }
            List<TaskAssignmentResponseDTO?> result = tasks.Select(MaptoTaskAssignmentResponse).ToList();
            return result;
            


        }
        catch (Exception e)
        {
            _logger.LogWarning("Failed to get the tasks");
            throw new Exception("Error getting the Tasks",e);
        }
    }

    public async Task<TaskAssignmentResponseDTO?> GetById(Guid Id)
    {
        try
        {
            TaskAssignment? task = await _context.TaskAssignments.Include(t=>t.Trainee).Include(t=>t.LearningTask).Include(t=>t.Mentor).FirstOrDefaultAsync(ta => ta.Id == Id); 
            if(task == null)
            {
                return null;
            }
            return MaptoTaskAssignmentResponse(task);
        }
        catch (Exception e)
        {
            _logger.LogWarning($"failed to retrive mentor with Id: {Id}",Id);
            throw new Exception("Failed to Retrive Mentor",e);
        }
    }

    public async Task<TaskAssignmentResponseDTO> AddTask(CreateorUpdateTaskAssignmentRequestDTO task )
    {
        Trainee? existtrainee = await _context.Trainees.FindAsync(task.TraineeId);
        if(existtrainee == null)
        {
            throw new NotFoundException("Trainee",task.TraineeId);
        }
        Mentor? existmentor = await _context.Mentors.FindAsync(task.MentorId);
        if(existmentor == null)
        {
            throw new NotFoundException("Mentor",task.MentorId);
        }
        LearningTask? existLearningtask = await _context.LearningTasks.FindAsync(task.LearningTaskId);
        if(existLearningtask == null)
        {
            throw new NotFoundException("LearningTask",task.LearningTaskId);
        }
        
        try
        {
            TaskAssignment newTask = new TaskAssignment
            {
                TraineeId = task.TraineeId,
                MentorId = task.MentorId,
                LearningTaskId = task.LearningTaskId,
                Status= task.Status,
                AssignDate = task.AssignDate,
                DueDate = task.DueDate,
                CreatedDate= DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            if (task.Remarks != null)
            {
                newTask.Remarks = task.Remarks;
            }
            await _context.TaskAssignments.AddAsync(newTask);
            await _context.SaveChangesAsync();
            return MaptoTaskAssignmentResponse(newTask);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Failed to add Task");
            throw new Exception("Failed to add Task",e);
        }
        
    }

    public async Task<TaskAssignmentResponseDTO?> UpdateTask(Guid Id,CreateorUpdateTaskAssignmentRequestDTO task)
    {
        try
        {
            TaskAssignment? newTask = await _context.TaskAssignments.FindAsync(Id);
            if(newTask == null)
            {
                return null;
            }
                
                newTask.Status= task.Status;                
            newTask.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return MaptoTaskAssignmentResponse(newTask);

        }
        catch (Exception e)
        {
            _logger.LogWarning($"Failed to update Task with Id: {Id}",Id);
            
            throw new Exception("Failed to update Taskassignment",e);
        }
    }

    public async Task<bool?> DeleteTask (Guid Id)
    {
        try
        {
            TaskAssignment? task = await _context.TaskAssignments.FindAsync(Id);
            if(task== null)
            {
                throw new NotFoundException("TaskAssignment",Id);
            }
            _context.TaskAssignments.Remove(task);
            await _context.SaveChangesAsync();
            return true;


        }
        catch (Exception)
        {   
            _logger.LogWarning($"Failed to delete Task assignment with Id: {Id}",Id);
            return false;
           
        }
    }






}
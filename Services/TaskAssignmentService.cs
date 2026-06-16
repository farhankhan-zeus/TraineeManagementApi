

using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Context;
using TraineeManagementApi.DTO.TaskAssignmentDTO;
using TraineeManagementApi.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;
using TraineeManagementApi.utils;
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

 
     public async Task<List<TaskAssignmentResponseDTO>> Getall()
    {
       
            List<TaskAssignment> tasks = await _context.TaskAssignments.Include(t=>t.Trainee).Include(t=>t.LearningTask).Include(t=>t.Mentor).ToListAsync();
            if(tasks.Count == 0)
            {
                return [];
            }
            List<TaskAssignmentResponseDTO> result = tasks.Select(ResponseDTOMapper.MaptoTaskAssignmentResponse).ToList();
            return result;
            


       
    }

    public async Task<TaskAssignmentResponseDTO> GetById(Guid Id)
    {
       
            TaskAssignment? task = await _context.TaskAssignments.Include(t=>t.Trainee).Include(t=>t.LearningTask).Include(t=>t.Mentor).FirstOrDefaultAsync(ta => ta.Id == Id); 
            if(task == null)
            {
                throw new NotFoundException("Task Assignment",Id);
            }
            return ResponseDTOMapper.MaptoTaskAssignmentResponse(task);
       
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
            return ResponseDTOMapper.MaptoTaskAssignmentResponse(newTask);
       
        
    }

    public async Task<TaskAssignmentResponseDTO> UpdateTask(Guid Id,CreateorUpdateTaskAssignmentRequestDTO task)
    {
       
            TaskAssignment? newTask = await _context.TaskAssignments.FindAsync(Id);
            if(newTask == null)
            {
                throw new NotFoundException("Task Assignment",Id);
            }
                
                newTask.Status= task.Status;                
            newTask.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return ResponseDTOMapper.MaptoTaskAssignmentResponse(newTask);

        
    }

    public async Task<bool> DeleteTask (Guid Id)
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






}


using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Context;
using TraineeManagementApi.DTO.LearningTaskDTO;
using TraineeManagementApi.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;
using TraineeManagementApi.utils;

namespace TraineeManagementApi.Services;

public class LearningTaskService: ILearningTaskService 
{
    private readonly ApiContext _context;
    private readonly ILogger<LearningTaskService> _logger;

    public LearningTaskService(ApiContext context, ILogger<LearningTaskService> logger)
    {
        _context =context;
        _logger = logger;
    }

   

    public async Task<List<LearningTaskResponseDTO>> Getall()
    {
       
            List<LearningTask> tasks = await _context.LearningTasks.ToListAsync();
            if(tasks.Count == 0)
            {
                return [];
            }
            List<LearningTaskResponseDTO> result = tasks.Select(ResponseDTOMapper.MapTaskToDTO).ToList();
            return result;
            


       
    }

    public async Task<LearningTaskResponseDTO> GetById(Guid Id)
    {
      
            LearningTask? task = await _context.LearningTasks.FindAsync(Id);
            if(task is null)
            {
                throw new NotFoundException("Learning Task",Id);
            }
            return ResponseDTOMapper.MapTaskToDTO(task);
        
      
    }

    public async Task<LearningTaskResponseDTO> AddTask(CreateorUpdateLearningTaskRequestDTO task)
    {
        
            LearningTask newtask = new LearningTask
            {
                Title = task.Title,
                Descriptiion = task.Descriptiion,
                ExpectedTechStack = task.ExpectedTechStack,
                Status = task.Status,
                DueDate = task.DueDate,
                CreatedDate= DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            await _context.LearningTasks.AddAsync(newtask);
            await _context.SaveChangesAsync();
            return ResponseDTOMapper.MapTaskToDTO(newtask);
        }
       
        
    

    public async Task<LearningTaskResponseDTO> UpdateTask(Guid Id,CreateorUpdateLearningTaskRequestDTO updatedtask)
    {
       
            LearningTask? task = await _context.LearningTasks.FindAsync(Id);
            if(task == null)
            {
                throw new NotFoundException("LearningTask",Id);
            }
            task.Title= updatedtask.Title;
            task.Descriptiion=updatedtask.Descriptiion;
            task.DueDate = updatedtask.DueDate;
            task.ExpectedTechStack = updatedtask.ExpectedTechStack;
            task.Status = updatedtask.Status;
            task.UpdatedDate=DateTime.Now;
            

            await _context.SaveChangesAsync();
            return ResponseDTOMapper.MapTaskToDTO(task);

        
        
    }

    public async Task<bool> DeleteTask (Guid Id)
    {
       
            LearningTask? task = await _context.LearningTasks.FindAsync(Id);
            if(task == null)
            {
                throw new NotFoundException("Learning Task",Id);
            }
            _context.LearningTasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;


        
    }


}


using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Context;
using TraineeManagementApi.DTO.LearningTaskDTO;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;

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

    private LearningTaskResponseDTO MapTaskToDTO (LearningTask task)
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

    public async Task<List<LearningTaskResponseDTO?>> Getall()
    {
        try
        {
            List<LearningTask>? tasks = await _context.LearningTasks.ToListAsync();
            if(tasks.Count == 0)
            {
                return [];
            }
            List<LearningTaskResponseDTO?> result = tasks.Select(MapTaskToDTO).ToList();
            return result;
            


        }
        catch (Exception e)
        {
            _logger.LogWarning("Failed to get the mentors");
            throw new Exception("Error getting the Mentors",e);
        }
    }

    public async Task<LearningTaskResponseDTO?> GetById(Guid Id)
    {
        try
        {
            LearningTask? task = await _context.LearningTasks.FindAsync(Id);
            if(task == null)
            {
                return null;
            }
            return MapTaskToDTO(task);
        }
        catch (Exception e)
        {
            _logger.LogWarning($"failed to retrive learning task with Id: {Id}",Id);
            throw new Exception("Failed to Retrive Mentor",e);
        }
    }

    public async Task<LearningTaskResponseDTO> AddTask(CreateorUpdateLearningTaskRequestDTO task)
    {
        try
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
            return MapTaskToDTO(newtask);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Failed to add Mentor");
            throw new Exception("Failed to add Menotr",e);
        }
        
    }

    public async Task<LearningTaskResponseDTO?> UpdateTask(Guid Id,CreateorUpdateLearningTaskRequestDTO updatedtask)
    {
        try
        {
            LearningTask? task = await _context.LearningTasks.FindAsync(Id);
            if(task == null)
            {
                return null;
            }
            task.Title= updatedtask.Title;
            task.Descriptiion=updatedtask.Descriptiion;
            task.DueDate = updatedtask.DueDate;
            task.ExpectedTechStack = updatedtask.ExpectedTechStack;
            task.Status = updatedtask.Status;
            task.UpdatedDate=DateTime.Now;
            

            await _context.SaveChangesAsync();
            return MapTaskToDTO(task);

        }
        catch (Exception e)
        {
            _logger.LogWarning($"Failed to update Mentor with Id: {Id}",Id);
            
            throw new Exception("Failed to update Mentor",e);
        }
    }

    public async Task<bool?> DeleteTask (Guid Id)
    {
        try
        {
            LearningTask? task = await _context.LearningTasks.FindAsync(Id);
            if(task == null)
            {
                return null;
            }
            _context.LearningTasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;


        }
        catch (Exception)
        {   
            _logger.LogWarning($"Failed to delete mentor with Id: {Id}",Id);
            return false;
           
        }
    }


}
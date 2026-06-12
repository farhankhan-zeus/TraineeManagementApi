
using TraineeManagementApi.DTO.TraineeDTO;
using TraineeManagementApi.Models;
using TraineeManagementApi.Context;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.utils;
using Microsoft.VisualBasic;
using TraineeManagementApi.Services.Interfaces;
namespace TraineeManagementApi.Services;

public class TraineeService : ITraineeService 
{
    private readonly ApiContext _context;
    private readonly ILogger<TraineeService> _logger;
    public TraineeService( ApiContext context, ILogger<TraineeService> logger){
        _context=context;
        _logger =logger;
    }
      private TraineeResponseDTO MapTraineetoDTO (Trainee trainee){
        return new TraineeResponseDTO{
            Id = trainee.Id,
            FirstName=trainee.FirstName,
            LastName=trainee.LastName,
            Email=trainee.Email,
            TechStack=trainee.TechStack,
            Status = trainee.Status,
           

        };
    }
    
    
    // private static int nextId=0;
    // private static List<Trainee> trainees = new List<Trainee>();

    public async Task<PagedResponse<TraineeResponseDTO>> GetAll (QuertFilter filter, CancellationToken cancellationToken) {
        //  return trainees.Select(MapTraineetoDTO).ToList();
        try
        {
       
        
        var query = _context.Trainees.AsQueryable();
            if (filter.Search !=null)
            {
        query = query.ApplySearch(filter.Search);
            }
            if (filter.Status != null)
            {                
        query= query.ApplyStatusFilter(filter.Status);
            }
        var trainees =  query.ApplyPagination(filter.PageNumber,filter.PageSize).Select(MapTraineetoDTO).ToList();
        var totalRecords = await query.CountAsync(cancellationToken);
        return new PagedResponse<TraineeResponseDTO>
        {
            Data= trainees,
            PageNumber=filter.PageNumber,
            PageSize=filter.PageSize,
            TotalRecords=totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double) filter.PageSize)

        };
        }
        catch(Exception e)
        {
            _logger.LogError($"failed to fetch Trainees");
            throw new Exception("Error getting the Trainees",e);
        };
    }

    public async Task<TraineeResponseDTO> GetById ( Guid Id){
        // var result =trainees.FirstOrDefault(p=>p.Id==Id);
        try
        {
             var result =  _context.Trainees.Where(t=>t.Id==Id).FirstOrDefault();
        if (result == null){
            return null;
        }
         return MapTraineetoDTO(result);
        }
        catch(Exception e)
        {
            _logger.LogError($"failed to fetch Trainee Id:{Id}",Id);
            throw new Exception("Error getting the Trainee",e);
        };
    }

    public async Task<TraineeResponseDTO> AddTrainee(CreateorUpdateTraineeRequestDTO traineedto){
        try
        {
  
             var newTrainee = new Trainee {
            // Id=nextId+1,
            FirstName=traineedto.FirstName,
            LastName=traineedto.LastName,
            Email=traineedto.Email,
            TechStack=traineedto.TechStack,
            Status=traineedto.Status,
            CreatedDate=DateTime.Now,
            UpdatedDate=DateTime.Now
        };
        
        await _context.Trainees.AddAsync(newTrainee);
        await _context.SaveChangesAsync();
        return MapTraineetoDTO(newTrainee);
        }
        catch (Exception e)
        {
            _logger.LogWarning($"Adding trainee failed");
            throw new Exception("Error adding the trainee",e);
        };
    }

    public async Task<TraineeResponseDTO> UpdateTrainee(Guid Id, CreateorUpdateTraineeRequestDTO updatedTraineedto){
        try
        {
             var atrainee =   _context.Trainees.Where(t=>t.Id==Id).FirstOrDefault();
        if (atrainee == null) return null;
        
        atrainee.FirstName = updatedTraineedto.FirstName;
        atrainee.LastName = updatedTraineedto.LastName;
        atrainee.Email = updatedTraineedto.Email;
        atrainee.TechStack = updatedTraineedto.TechStack;
        atrainee.Status = updatedTraineedto.Status;
        atrainee.UpdatedDate= DateTime.Now;

       await _context.SaveChangesAsync();
        return MapTraineetoDTO(atrainee);
        }
        catch (Exception e)
        {
            _logger.LogWarning($"Failed to update trainee Id:{Id}");
            throw new Exception("Error updating the trainee",e);
        };
    }

    public async Task<bool> Delete(Guid Id){
        try
        {
             Trainee atrainee = _context.Trainees.Where(t=>t.Id==Id).FirstOrDefault();
        if (atrainee == null) return false;

       _context.Trainees.Remove(atrainee);
       await _context.SaveChangesAsync();
        return true;
        }
        catch (Exception e)
        {
            _logger.LogWarning($"Delete failed for Id: {Id}");
            throw new Exception("Error deleting the trainee",e);
        };
    } 

}   

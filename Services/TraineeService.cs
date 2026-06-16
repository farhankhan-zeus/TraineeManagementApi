
using TraineeManagementApi.DTO.TraineeDTO;
using TraineeManagementApi.Models;
using TraineeManagementApi.Context;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.utils;
using Microsoft.VisualBasic;
using TraineeManagementApi.Services.Interfaces;
using TraineeManagementApi.Exceptions;
namespace TraineeManagementApi.Services;

public class TraineeService : ITraineeService 
{
    private readonly ApiContext _context;
    private readonly ILogger<TraineeService> _logger;
    public TraineeService( ApiContext context, ILogger<TraineeService> logger){
        _context=context;
        _logger =logger;
    }

    
    
    // private static int nextId=0;
    // private static List<Trainee> trainees = new List<Trainee>();

    public async Task<PagedResponse<TraineeResponseDTO>> GetAll (QuertFilter filter, CancellationToken cancellationToken) {
        //  return trainees.Select(MapTraineetoDTO).ToList();
       
       
        
        IQueryable<Trainee> query = _context.Trainees.AsQueryable();
            if (filter.Search !=null)
            {
        query = query.ApplySearch(filter.Search);
            }
            if (filter.Status != null)
            {                
        query= query.ApplyStatusFilter(filter.Status);
            }
        List<TraineeResponseDTO> trainees =  query.ApplyPagination(filter.PageNumber,filter.PageSize).Select(ResponseDTOMapper.MapTraineetoDTO).ToList();
        int totalRecords = await query.CountAsync(cancellationToken);
        return new PagedResponse<TraineeResponseDTO>
        {
            Data= trainees,
            PageNumber=filter.PageNumber,
            PageSize=filter.PageSize,
            TotalRecords=totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double) filter.PageSize)

        };
       
    }

    public async Task<TraineeResponseDTO> GetById ( Guid Id){
        // var result =trainees.FirstOrDefault(p=>p.Id==Id);
       
             Trainee? result =  await _context.Trainees.FindAsync(Id);
        if (result == null){
            throw new NotFoundException("Trainee",Id);
        }
         return ResponseDTOMapper.MapTraineetoDTO(result);
       
    }

    public async Task<TraineeResponseDTO> AddTrainee(CreateorUpdateTraineeRequestDTO traineedto){
       
  
             Trainee newTrainee = new Trainee {
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
        return ResponseDTOMapper.MapTraineetoDTO(newTrainee);
       
    }

    public async Task<TraineeResponseDTO> UpdateTrainee(Guid Id, CreateorUpdateTraineeRequestDTO updatedTraineedto){
       
             Trainee? atrainee =   await _context.Trainees.FindAsync(Id);
        if (atrainee == null)
        {
            throw new NotFoundException("Trainee",Id);
        }
        
        atrainee.FirstName = updatedTraineedto.FirstName;
        atrainee.LastName = updatedTraineedto.LastName;
        atrainee.Email = updatedTraineedto.Email;
        atrainee.TechStack = updatedTraineedto.TechStack;
        atrainee.Status = updatedTraineedto.Status;
        atrainee.UpdatedDate= DateTime.Now;

       await _context.SaveChangesAsync();
        return ResponseDTOMapper.MapTraineetoDTO(atrainee);
       
    }

    public async Task<bool> Delete(Guid Id){
        
             Trainee? atrainee = _context.Trainees.Where(t=>t.Id==Id).FirstOrDefault();
        if (atrainee == null)
        {
            throw new NotFoundException("Trainee",Id);
        }

       _context.Trainees.Remove(atrainee);
       await _context.SaveChangesAsync();
        return true;
       
    } 

}   

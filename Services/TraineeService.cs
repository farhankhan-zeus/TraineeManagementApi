
using TraineeManagementApi.DTO;
using TraineeManagementApi.Models;
using TraineeManagementApi.Context;
using System.Collections.Generic;
namespace TraineeManagementApi.Services;

public class TraineeService : ITraineeService 
{
    private readonly ApiContext _context;
    public TraineeService( ApiContext context){
        _context=context;
    }
      private TraineeResponseDTO MapTraineetoDTO (Trainee trainee){
        return new TraineeResponseDTO{
            Id = trainee.Id,
            FirstName=trainee.FirstName,
            LastName=trainee.LastName,
            Email=trainee.Email,
            TechStack=trainee.TechStack,
            Status=trainee.Status,
           

        };
    }
    // private static int nextId=0;
    // private static List<Trainee> trainees = new List<Trainee>();

    public async Task<List<TraineeResponseDTO>> GetAll (string search) {
        //  return trainees.Select(MapTraineetoDTO).ToList();
        try
        {
        var result = _context.Trainees.Select(MapTraineetoDTO);
        if(search != "")
        {
            var query = result.Where(t=> t.FirstName.Contains(search) || t.LastName.Contains(search) || t.Email.Contains(search) || t.TechStack.Contains(search));
            return query.ToList();
        }
        return result.ToList();
        }
        catch(Exception e)
        {
            throw new Exception("Error getting the Trainees",e);
        };
    }

    public async Task<TraineeResponseDTO> GetById ( int Id){
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
            throw new Exception("Error getting the Trainee",e);
        };
    }

    public async Task<TraineeResponseDTO> AddTrainee(CreateTraineeRequestDTO traineedto){
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
            
            throw new Exception("Error adding the trainee",e);
        };
    }

    public async Task<TraineeResponseDTO> UpdateTrainee(int Id, UpdateTraineeRequestDTO updatedTraineedto){
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
            
            throw new Exception("Error updating the trainee",e);
        };
    }

    public async Task<bool> Delete(int Id){
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
            
            throw new Exception("Error deleting the trainee",e);
        };
    } 

}   

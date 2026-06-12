

using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Context;
using TraineeManagementApi.DTO.MentorDTO;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;

namespace TraineeManagementApi.Services;

public class MentorService: IMentorService 
{
    private readonly ApiContext _context;
    private readonly ILogger<MentorService> _logger;

    public MentorService(ApiContext context, ILogger<MentorService> logger)
    {
        _context =context;
        _logger = logger;
    }

    private MentorResponseDTO MapMentorToDTO (Mentor mentor)
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

    public async Task<List<MentorResponseDTO?>> Getall()
    {
        try
        {
            List<Mentor>? mentors = await _context.Mentors.ToListAsync();
            if(mentors.Count == 0)
            {
                return [];
            }
            List<MentorResponseDTO?> result = mentors.Select(MapMentorToDTO).ToList();
            return result;
            


        }
        catch (Exception e)
        {
            _logger.LogWarning("Failed to get the mentors");
            throw new Exception("Error getting the Mentors",e);
        }
    }

    public async Task<MentorResponseDTO?> GetById(Guid Id)
    {
        try
        {
            Mentor? mentee = await _context.Mentors.FindAsync(Id);
            if(mentee == null)
            {
                return null;
            }
            return MapMentorToDTO(mentee);
        }
        catch (Exception e)
        {
            _logger.LogWarning($"failed to retrive mentor with Id: {Id}",Id);
            throw new Exception("Failed to Retrive Mentor",e);
        }
    }

    public async Task<MentorResponseDTO> AddMentor(CreateorUpdateMentorRequestDTO mentee)
    {
        try
        {
            Mentor newMentee = new Mentor
            {
                FirstName= mentee.FirstName,
                LastName = mentee.LastName,
                Email = mentee.Email,
                Expertise = mentee.Expertise,
                Status = mentee.Status,
                CreatedDate= DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            await _context.Mentors.AddAsync(newMentee);
            await _context.SaveChangesAsync();
            return MapMentorToDTO(newMentee);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Failed to add Mentor");
            throw new Exception("Failed to add Menotr",e);
        }
        
    }

    public async Task<MentorResponseDTO?> UpdateMentor(Guid Id,CreateorUpdateMentorRequestDTO updatedmentee)
    {
        try
        {
            Mentor? mentee = await _context.Mentors.FindAsync(Id);
            if(mentee == null)
            {
                return null;
            }
            mentee.FirstName = updatedmentee.FirstName;
            mentee.LastName = updatedmentee.LastName;
            mentee.Email = updatedmentee.Email;
            mentee.Expertise = updatedmentee.Expertise;
            mentee.Status = updatedmentee.Status;
            mentee.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return MapMentorToDTO(mentee);

        }
        catch (Exception e)
        {
            _logger.LogWarning($"Failed to update Mentor with Id: {Id}",Id);
            
            throw new Exception("Failed to update Mentor",e);
        }
    }

    public async Task<bool?> DeleteMentor (Guid Id)
    {
        try
        {
            Mentor? mentee = await _context.Mentors.FindAsync(Id);
            if(mentee== null)
            {
                return null;
            }
            _context.Mentors.Remove(mentee);
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
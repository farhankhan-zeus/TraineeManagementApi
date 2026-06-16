

using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Context;
using TraineeManagementApi.DTO.MentorDTO;
using TraineeManagementApi.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;
using TraineeManagementApi.utils;

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

    public  MentorResponseDTO MapMentorToDTO (Mentor mentor)
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

    public async Task<List<MentorResponseDTO>> Getall()
    {
      
            List<Mentor> mentors = await _context.Mentors.ToListAsync();
            if(mentors.Count == 0)
            {
                return [];
            }
            List<MentorResponseDTO> result = mentors.Select(ResponseDTOMapper.MapMentorToDTO).ToList();
            return result;
            


       
    }

    public async Task<MentorResponseDTO> GetById(Guid Id)
    {
        
            Mentor? mentee = await _context.Mentors.FindAsync(Id);
            if(mentee == null)
            {
                throw new NotFoundException("Mentor",Id);
            }
            return ResponseDTOMapper.MapMentorToDTO(mentee);
        
    }

    public async Task<MentorResponseDTO> AddMentor(CreateorUpdateMentorRequestDTO mentee)
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
            return ResponseDTOMapper.MapMentorToDTO(newMentee);
       
        
    }

    public async Task<MentorResponseDTO> UpdateMentor(Guid Id,CreateorUpdateMentorRequestDTO updatedmentee)
    {
        
            Mentor? mentee = await _context.Mentors.FindAsync(Id);
            if(mentee == null)
            {
                throw new NotFoundException("Mentor",Id);
            }
            mentee.FirstName = updatedmentee.FirstName;
            mentee.LastName = updatedmentee.LastName;
            mentee.Email = updatedmentee.Email;
            mentee.Expertise = updatedmentee.Expertise;
            mentee.Status = updatedmentee.Status;
            mentee.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return ResponseDTOMapper.MapMentorToDTO(mentee);

        
    }

    public async Task<bool> DeleteMentor (Guid Id)
    {
       
            Mentor? mentee = await _context.Mentors.FindAsync(Id);
            if(mentee== null)
            {
                throw new NotFoundException("Mentor",Id);
            }
            _context.Mentors.Remove(mentee);
            await _context.SaveChangesAsync();
            return true;


        
    }


}
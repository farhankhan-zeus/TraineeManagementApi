using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraineeManagementApi.DTO.MentorDTO;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;

namespace TraineeManagementApi.Controllers;

[ApiController]
[Route("api/[Controller]")]

public class MentorController : ControllerBase
{
    private readonly IMentorService _mentorservice;
    private readonly ILogger<MentorController> _logger;

    public MentorController(IMentorService mentorService,ILogger<MentorController> logger)
    {
        _mentorservice=mentorService;
        _logger=logger;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Getall()
    {
        
            List<MentorResponseDTO> response = await _mentorservice.Getall();
            _logger.LogInformation("Mentor fetched successfully");
            return Ok( new ApiResponse<List<MentorResponseDTO?>>
            {
                success=true,
                message="Data Fetched successfully",
                Data = response
            });
            
       
    }
    [Authorize]
    [HttpGet("{Id:guid}")]    
    public async Task<IActionResult> GetById (Guid Id)
    {
      
        var result = await _mentorservice.GetById(Id);
        
        
        _logger.LogInformation($"Mentor with Id:{Id} fetched successfully",Id);
        return Ok( new ApiResponse<MentorResponseDTO>
            {
                success=true,
                message="Data Fetched successfully",
                Data = result
            });


    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> addMentor( CreateorUpdateMentorRequestDTO mentee)
    {
        
            MentorResponseDTO response = await _mentorservice.AddMentor(mentee);
            return Ok( new ApiResponse<MentorResponseDTO>
            {
                success=true,
                message="Mentor added successfully",
                Data=response
            });
       
    }

    [Authorize]
    [HttpPut("{Id:guid}")]
    public async Task<IActionResult> updateMentor (Guid Id,CreateorUpdateMentorRequestDTO mentee)
    {
        MentorResponseDTO response = await _mentorservice.UpdateMentor(Id, mentee);
       
            
        
        _logger.LogInformation($"Mentor with Id:{Id} updated successfully",Id);
        return Ok(new ApiResponse<MentorResponseDTO>
        {
            success=true,
            message="Mentor updated successfully",
            Data=response
        });
    
    }
    [Authorize]
    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> deleteMentor (Guid Id)
    {
        bool response = await  _mentorservice.DeleteMentor(Id);
         if(response) _logger.LogInformation($"Mentor with Id: {Id} deleted",Id);
    
        return NoContent() ;
    }


}


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
        try
        {
            List<MentorResponseDTO?> response = await _mentorservice.Getall();
            _logger.LogInformation("Mentor fetched successfully");
            return Ok( new ApiResponse<List<MentorResponseDTO?>>
            {
                success=true,
                message="Data Fetched successfully",
                Data = response
            });
            
        }
        catch (Exception)
        {
            return StatusCode(500,new ApiResponse<object>
            {
                success=false,
                message="Internal Server Error",
                Data={}
            });
        }
    }
    [Authorize]
    [HttpGet("{Id:guid}")]    
    public async Task<IActionResult> GetById (Guid Id)
    {
        try{
        var result = await _mentorservice.GetById(Id);
        if(result == null)
        {
            return NotFound( new ApiResponse<object>
            {
                success=true,
                message="Data Fetched successfully",
                Data = {}
            });
        }
        _logger.LogInformation($"Mentor with Id:{Id} fetched successfully",Id);
        return Ok( new ApiResponse<MentorResponseDTO>
            {
                success=true,
                message="Data Fetched successfully",
                Data = result
            });
        }
        catch( Exception )
        {
            return StatusCode(500,new ApiResponse<object>
            {
                success=false,
                message="Internal Server Error",
                Data={}
            });
        }

    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> addMentor( CreateorUpdateMentorRequestDTO mentee)
    {
        try
        {
            MentorResponseDTO response = await _mentorservice.AddMentor(mentee);
            return Ok( new ApiResponse<MentorResponseDTO>
            {
                success=true,
                message="Mentor added successfully",
                Data=response
            });
        }
        catch (Exception)
        {
            
            return StatusCode(500,new ApiResponse<object>
            {
                success=false,
                message="Internal Server Error",
                Data={}
            });
        }
    }

    [Authorize]
    [HttpPut("{Id:guid}")]
    public async Task<IActionResult> updateMentor (Guid Id,CreateorUpdateMentorRequestDTO mentee)
    {
        MentorResponseDTO? response = await _mentorservice.UpdateMentor(Id, mentee);
        if(response == null)
        {
            return NotFound(new ApiResponse<object>
        {
            success=false,
            message="Mentor doesn't exist",
            Data={}
        });
            
        }
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
        bool? response = await  _mentorservice.DeleteMentor(Id);
        if(response == null)
        {
             return NotFound(new ApiResponse<object>
        {
            success=false,
            message="Mentor doesn't exist",
            Data={}
        });
        }
        return NoContent();
    }


}


using Microsoft.AspNetCore.Mvc;
using TraineeManagementApi.Models;
using TraineeManagementApi.DTO.TraineeDTO;
using TraineeManagementApi.Services;
using TraineeManagementApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace TraineeManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TraineeListController : ControllerBase
{   
    private readonly ITraineeService _traineeservice;
    // private readonly ILogger<TraineeListController> _logger;

    public TraineeListController(ITraineeService traineeservice){
        _traineeservice = traineeservice;
        // _logger = logger;
    }
    

  

    
    [Authorize]
    [HttpGet]
     public async Task<IActionResult> GetAll([FromQuery] QuertFilter filter ,CancellationToken cancellationToken)
    {
        // return trainees.Select(MapTraineetoDTO).ToList();
        try
        {
              var result= await _traineeservice.GetAll(filter,cancellationToken);
       
        return Ok(new ApiResponse<PagedResponse<TraineeResponseDTO>>
        {
            success=true,
            message="Successfull",
            Data= result,
        });
        }
        catch(Exception  e)
        {
           
            return StatusCode(500,new ApiResponse<bool>
        {
            success=true,
            message=e.Message +"Internal server Error",
            Data= false,
        });
        }
    }

    [Authorize]
    [HttpGet("{Id:guid}")]    
    public async  Task<IActionResult> GetById(Guid Id)
    {
        // return MapTraineetoDTO(trainees.FirstOrDefault(p=>p.Id==Id));
        try
        {
             var result = await _traineeservice.GetById(Id);
        if (result == null){
            return NotFound( new ApiResponse<TraineeResponseDTO>{ success=false,message="Trainee Not found",Data=result} ); }
        return Ok(new ApiResponse<TraineeResponseDTO>
        {
            success=true,
            message="Trainee fetched successfully",
            Data= result,
        });
        }
        catch
        {
            
            return StatusCode(500,new ApiResponse<bool>
        {
            success=true,
            message="Trainee Not found",
            Data= false,
        });
        }
    }


        [Authorize]
        [HttpPost]
    public async  Task<IActionResult> AddTrainee(CreateorUpdateTraineeRequestDTO traineedto)
    {
        // var newTrainee = new Trainee {
        //     Id=nextId+1,
        //     FirstName=traineedto.FirstName,
        //     LastName=traineedto.LastName,
        //     Email=traineedto.Email,
        //     TechStack=traineedto.TechStack,
        //     Status=traineedto.Status,
        //     CreatedDate=DateTime.Now
        // };

        // trainees.Add(newTrainee);
        // nextId+=1;
        // return MapTraineetoDTO(newTrainee);
        try
        {
             var addedTrainee= await _traineeservice.AddTrainee(traineedto);
     return Created("/api/trainee",new ApiResponse<TraineeResponseDTO>
        {
            success=true,
            message="Trainee added successfully",
            Data= addedTrainee,
        });
        }
        catch
        {
            return StatusCode(500,new ApiResponse<bool>
        {
            success=true,
            message="Something went wrong",
            Data= false,
        });
        }
    }  


    [Authorize]
    [HttpPut("{Id:guid}")]
    public async  Task<IActionResult> UpdateTrainee(Guid Id, CreateorUpdateTraineeRequestDTO updatedTraineedto)
    {
        //     var atrainee = trainees.FirstOrDefault(p => p.Id == Id);
        //     if (atrainee == null) return null;

        //     atrainee.FirstName = updatedTraineedto.FirstName;
        //     atrainee.LastName = updatedTraineedto.LastName;
        //     atrainee.Email = updatedTraineedto.Email;
        //     atrainee.TechStack = updatedTraineedto.TechStack;
        //     atrainee.Status = updatedTraineedto.Status;
        //     atrainee.UpdatedDate= DateTime.Now;


        //     return MapTraineetoDTO(atrainee);
        try
        {
             var updated= await _traineeservice.UpdateTrainee(Id,updatedTraineedto);
            if (updated == null){
                return NotFound(new ApiResponse<TraineeResponseDTO>
        {
            success=true,
            message="Trainee not found",
            Data= updated,
        });
            }
            return Ok(new ApiResponse<TraineeResponseDTO>
        {
            success=true,
            message="Trainee updated successfully",
            Data= updated,
        });
        }
            catch
        {
            return StatusCode(500,new ApiResponse<bool>
        {
            success=true,
            message="Trainee Not found",
            Data= false,
        });
        }
    }

    [Authorize]
    [HttpDelete("{Id:guid}")]
    public async  Task<IActionResult> Delete(Guid Id)
    {
        //     var atrainee = trainees.FirstOrDefault(p => p.Id == Id);
        //     if (atrainee == null) return false;

        //     trainees.Remove(atrainee);
        //     return true;
        try
        {
            var isDeleted =await  _traineeservice.Delete(Id);
           if (isDeleted){
            return NoContent();
           }
           return NotFound(new ApiResponse<bool>
        {
            success=true,
            message="Trainee Not found",
            Data= false,
        }); 
        }
        catch
        {
            return StatusCode(500,new ApiResponse<bool>
        {
            success=true,
            message="Trainee Not found",
            Data= false,
        });
        }
           
               }





};


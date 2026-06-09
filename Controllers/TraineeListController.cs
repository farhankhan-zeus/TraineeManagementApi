using Microsoft.AspNetCore.Mvc;
using TraineeManagementApi.Models;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Services;
namespace TraineeManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TraineeListController : ControllerBase
{   
    private readonly ITraineeService _traineeservice;

    public TraineeListController(ITraineeService traineeservice){
        _traineeservice = traineeservice;
    }
    

  

    
    [HttpGet]
     public async Task<IActionResult> GetAll(string search="")
    {
        // return trainees.Select(MapTraineetoDTO).ToList();
        try
        {
              var result= await _traineeservice.GetAll(search);
       
        return Ok(new ApiResponse<List<TraineeResponseDTO>>
        {
            success=true,
            message="Successfull",
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

    [HttpGet("{Id:int}")]
    public async  Task<IActionResult> GetById(int Id)
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


    [HttpPost]
    public async  Task<IActionResult> AddTrainee(CreateTraineeRequestDTO traineedto)
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


    [HttpPut("{Id:int}")]
    public async  Task<IActionResult> UpdateTrainee(int Id, UpdateTraineeRequestDTO updatedTraineedto)
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

    [HttpDelete("{Id:int}")]
    public async  Task<IActionResult> Delete(int Id)
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


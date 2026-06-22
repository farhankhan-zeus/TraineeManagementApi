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
    private readonly ILogger<TraineeListController> _logger;

    public TraineeListController(ITraineeService traineeservice, ILogger<TraineeListController> logger){
        _traineeservice = traineeservice;
        _logger = logger;
    }
    

  

    
    [Authorize]
    [HttpGet]
     public async Task<IActionResult> GetAll([FromQuery] QuertFilter filter ,CancellationToken cancellationToken)
    {
        // return trainees.Select(MapTraineetoDTO).ToList();
        
              PagedResponse<TraineeResponseDTO> result= await _traineeservice.GetAll(filter,cancellationToken);
       
        return Ok(new ApiResponse<PagedResponse<TraineeResponseDTO>>
        {
            success=true,
            message="Successfull",
            Data= result,
        });
       
    }

    [Authorize]
    [HttpGet("{Id:guid}")]    
    public async  Task<IActionResult> GetById(Guid Id,CancellationToken cancellationToken=default)
    {
        // return MapTraineetoDTO(trainees.FirstOrDefault(p=>p.Id==Id));
        
             TraineeResponseDTO result = await _traineeservice.GetById(Id,cancellationToken);
        return Ok(new ApiResponse<TraineeResponseDTO>
        {
            success=true,
            message="Trainee fetched successfully",
            Data= result,
        });
       
    }


        [Authorize]
        [HttpPost]
    public async  Task<IActionResult> AddTrainee(CreateorUpdateTraineeRequestDTO traineedto,CancellationToken cancellationToken=default)
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
       
             TraineeResponseDTO addedTrainee= await _traineeservice.AddTrainee(traineedto,cancellationToken);
             
     return Created("/api/trainee",new ApiResponse<TraineeResponseDTO>
        {
            success=true,
            message="Trainee added successfully",
            Data= addedTrainee,
        });
       
    }  


    [Authorize]
    [HttpPut("{Id:guid}")]
    public async  Task<IActionResult> UpdateTrainee(Guid Id, CreateorUpdateTraineeRequestDTO updatedTraineedto,CancellationToken cancellationToken=default)
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
       
             TraineeResponseDTO updated= await _traineeservice.UpdateTrainee(Id,updatedTraineedto,cancellationToken);
           
            return Ok(new ApiResponse<TraineeResponseDTO>
        {
            success=true,
            message="Trainee updated successfully",
            Data= updated,
        });
       
    }

    [Authorize]
    [HttpDelete("{Id:guid}")]
    public async  Task<IActionResult> Delete(Guid Id,CancellationToken cancellationToken=default)
    {
        //     var atrainee = trainees.FirstOrDefault(p => p.Id == Id);
        //     if (atrainee == null) return false;

        //     trainees.Remove(atrainee);
        //     return true;
       
            bool isDeleted =await  _traineeservice.Delete(Id,cancellationToken);
            if(isDeleted) _logger.LogInformation($"Trainee with Id: {Id} deleted",Id);
            return NoContent();
    }
           
        
       
           
               





}


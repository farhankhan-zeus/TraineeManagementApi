using TraineeManagementApi.Exceptions;

namespace TraineeManagementApi.utils;
public static class GuidValidation
{
    public static void ValidateGuid(Guid Id)
    {
        if (string.IsNullOrWhiteSpace(Id.ToString())) 
            {
                throw new BadRequestException("Valid ID is required");
            }

            if (!Guid.TryParse(Id.ToString(), out Guid validId)) 
            {
                throw new BadRequestException("Invalid ID");
            }
    }
    public static void ValidateGuid(string stringId)
    {
        
        if (string.IsNullOrWhiteSpace(stringId)) 
            {
                throw new BadRequestException("Valid ID is required");
            }

            if (!Guid.TryParse(stringId, out Guid validId)) 
            {
                throw new BadRequestException("Invalid ID");
            }
}
    }


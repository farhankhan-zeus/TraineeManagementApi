using BCrypt.Net;
namespace TraineeManagementApi.utils;

class PasswordHasher
{
    public  static string Hashpassword(string mypassword)
    {
     string hashedpassword = BCrypt.Net.BCrypt.EnhancedHashPassword(mypassword);   
     return hashedpassword;
    }

    public static bool VerifyPassword (string mypassword,string hashedpassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(mypassword,hashedpassword);
    }
}

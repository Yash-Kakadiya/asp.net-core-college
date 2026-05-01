using PractiseProject.Models;

namespace PractiseProject.Interfaces
{
    public interface IAuthService
    {
        Task<object> LoginAsync(string username, string password);
        string GenerateJwtToken(User user);
    }
}

namespace TicketManagementSystem.DTOs.User.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RoleResponse Role { get; set; } = null;
        public DateTime CreatedAt { get; set; }
    }
    public class RoleResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

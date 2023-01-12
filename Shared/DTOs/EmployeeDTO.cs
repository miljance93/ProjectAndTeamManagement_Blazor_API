namespace Shared.DTOs
{
    public class EmployeeDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public int? ProjectId { get; set; }
        public int? TeamId { get; set; }
    }
}

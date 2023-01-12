namespace Shared.DTOs
{
    public class ProjectDTO
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Path { get; set; }
        public int? TeamId { get; set; }
        public int? ProjectId { get; set; }
        public string? ProjectLeadId { get; set; }
        public string? ProjectStatusId { get; set; }
    }
}

namespace Shared.DTOs
{
    public class RequestDTO
    {
        public int RequestId { get; set; }
        public string? LeadId { get; set; }
        public string? DepartmentLeadId { get; set; }
        public string? ProjectLeadId { get; set; }
        public string? TeamLeadId { get; set; }
        public string EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? RequestStatusId { get; set; }
    }
}

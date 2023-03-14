namespace WebApi.Controllers.Members.Current
{
    public class UpdateMemberInfoRequest
    {
        public string? BriefDescription { get; set; }

        public string? LookingFor { get; set; }

        public string? Interests { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }
    }
}

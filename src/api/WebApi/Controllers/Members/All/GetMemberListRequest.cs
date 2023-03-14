using Application.Members.Queries.GetList;
using Application.Users.Registration.Models;

namespace WebApi.Controllers.Members.All
{
    public class GetMemberListRequest
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public GenderDto? Gender { get; set; }

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }

        public SortableField? OrderBy { get; set; }
    }
}

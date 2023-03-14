using System.Net;

namespace Application.Common.Exceptions
{
    public class ResourceNotFoundException : ApiException
    {
        public ResourceNotFoundException()
            : base((int)HttpStatusCode.NotFound, "Requested resource was not found")
        {
        }
    }
}

using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Persistence.Photos
{
    public interface IPhotoStorage
    {
        Task<AddPhotoResponse> Add(AddPhotoRequest request, CancellationToken cancellationToken = default);

        Task Delete(DeletePhotoRequest request, CancellationToken cancellationToken = default);
    }
}

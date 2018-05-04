using System.Collections.Generic;
using System.Threading.Tasks;
using Cubo.Core.DTO;

namespace Cubo.Core.Services
{
    public interface IBucketService {
        Task<BucketDTO> GetAsync (string name);
        Task<IEnumerable<string>> GetNameAsync ();
        Task RemoveAsync (string name);
        Task AddAsync (string name);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cubo.Core.Domain;

namespace Cubo.Core.Repositories
{
    public class InMemoryBucketRepository : IBucketRepository
    {
        private readonly ISet<Bucket> _buckets = new HashSet<Bucket>();

        public async Task AddAsync(Bucket bucket)
        {
            _buckets.Add(bucket);
            await Task.CompletedTask;
        }

        public async Task<Bucket> GetAsync(string name)
        {
            var bucket = _buckets.SingleOrDefault(x=> x.Name == name);
            return await Task.FromResult(bucket);
        }

        public async Task<IEnumerable<string>> GetNameAsync()
        => await Task.FromResult(_buckets.Select(x => x.Name));

        public async Task RemoveAsync(string name)
        {
            _buckets.Remove(await GetAsync(name));
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Bucket bucket)
       => await Task.CompletedTask;
    }
}
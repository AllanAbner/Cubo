using AutoMapper;
using Cubo.Core.Domain;
using Cubo.Core.DTO;
using Cubo.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cubo.Core.Services
{
    public class BucketService : IBucketService
    {
        private readonly IBucketRepository _bucketRepository;
        private readonly IMapper _mapper;

        public BucketService(IBucketRepository bucketRepository, IMapper mapper)
        {
            _bucketRepository = bucketRepository;
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddAsync(string name)
        {
            var bucket = await _bucketRepository.GetAsync(name);
            if (bucket != null)
            {
                throw new CuboException("bucket_already_exists",$"Bucket: '{name}' already exist ");

            }
            bucket = new Bucket(Guid.NewGuid(), name);
            await _bucketRepository.AddAsync(bucket);

        }

        public async Task<BucketDTO> GetAsync(string name)
        {
            var bucket = await _bucketRepository.GetOrFailAsync(name);

            return _mapper.Map<BucketDTO>(bucket);

        }

        public async Task<IEnumerable<string>> GetNameAsync() => await _bucketRepository.GetNameAsync();

        public async Task RemoveAsync(string name) => await _bucketRepository.RemoveAsync(name);

    }
}
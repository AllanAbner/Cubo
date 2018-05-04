using System;
using System.Threading.Tasks;
using Cubo.Core.Domain;
using Cubo.Core.Repositories;
using Xunit;

namespace Cubo.Tests.Repositories {
    public class BucketRepositoryTests {
        [Fact]
        public async Task add_async_shoud_create_new_bucket () {
            
            //Given
            var repository = new InMemoryBucketRepository ();
            var name = "test-bucket";
            var bucket = new Bucket(Guid.NewGuid(),name);

            //When
            await repository.AddAsync(bucket);

            //Then

            var createdBucket = await repository.GetAsync(bucket.Name);
            Assert.Same(bucket,createdBucket);
        }
    }
}
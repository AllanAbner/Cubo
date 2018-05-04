using System.Linq;
using AutoMapper;
using Cubo.Core.Domain;
using Cubo.Core.DTO;

namespace Cubo.Core.Mappers {
    public static class AutoMapperConfig {
        public static IMapper GetMapper () {
            var config = new MapperConfiguration (cfg => {

                cfg.CreateMap<Bucket, BucketDTO> ()
                    .ForMember (m => m.Items, m =>
                        m.MapFrom (p => p.Items.Select (i => i.Key).OrderBy (o => o)));

                cfg.CreateMap<Item, ItemDTO> ();

            });

            return config.CreateMapper ();
        }
    }
}
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Aucxis.Eprw.Reporting.Dataservice.Entities.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.TestEntity, Models.TestModel>();
            CreateMap<Models.TestModel, Entities.TestEntity>()
                .ForMember(_ => _.Id, _ => _.Ignore());
        }
    }
}


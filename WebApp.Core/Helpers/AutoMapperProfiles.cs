using AutoMapper;
using WebApp.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Domain.Entities;

namespace WebApp.Core.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Member, MemberResponseDTO>();
            CreateMap<MemberResponseDTO, Member>();
        }
    }
}

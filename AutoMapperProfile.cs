using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_7.DTOs.Fight;
using net_7.DTOs.Skill;
using net_7.DTOs.Weapon;

namespace net_7
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<GetCharacterDto, Character>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<AddWeaponDto, Weapon>();
            CreateMap<Skill, GetSkillDto>();
            CreateMap<Character, HighScoreDto>();
        }
    }
}
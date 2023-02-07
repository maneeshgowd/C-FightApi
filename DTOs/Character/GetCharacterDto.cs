using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_7.DTOs.Skill;
using net_7.DTOs.Weapon;

namespace net_7.DTOs.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public GetWeaponDto? Weapon { get; set; }
        public List<GetSkillDto>? Skills { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}
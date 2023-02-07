using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_7.DTOs.Weapon;

namespace net_7.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}
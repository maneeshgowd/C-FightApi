using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using net_7.DTOs.Weapon;

namespace net_7.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            _context = context;

        }
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var response = new ServiceResponse<GetCharacterDto>();


            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.User!.Id == int.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!));

                if (character is null)
                {
                    throw new Exception("Character not found!");
                }

                var weapon = mapper.Map<Weapon>(newWeapon);

                _context.Weapons.Add(weapon);

                await _context.SaveChangesAsync();
                response.Data = mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

using System.Security.Claims;

namespace net_7.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CharacterService(IMapper mapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var newCharacter = _mapper.Map<Character>(character);
            newCharacter.User = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            _dataContext.Characters.Add(newCharacter);
            await _dataContext.SaveChangesAsync();

            serviceResponse.Data = await _dataContext.Characters.Where(c => c.User!.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            serviceResponse.Data = await _dataContext.Characters.Include(c => c.Weapon).Include(c => c.Skills).Where(c => c.User!.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterByid(int id)
        {

            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _dataContext.Characters.Include(c => c.Weapon).Include(c => c.Skills).FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
                if (character is null)
                    throw new Exception($"Character with Id '{id}' Not Found!");

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(GetCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var updatedCharacter = await _dataContext.Characters.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == character.Id);

                if (updatedCharacter is null || updatedCharacter.User!.Id != GetUserId())
                    throw new Exception($"Character with Id '{character.Id}' Not Found!");

                _dataContext.Characters.Update(_mapper.Map<Character>(character));
                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(updatedCharacter);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var character = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());

                if (character is null)
                    throw new Exception($"Character with Id '{id}' Not Found!");

                _dataContext.Characters.Remove(character);
                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = await _dataContext.Characters
                .Where(c => c.User!.Id == GetUserId())
                .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _dataContext.Characters.Include(c => c.Weapon).Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.User!.Id == GetUserId());

                if (character is null)
                {
                    throw new Exception("Character not found.");
                }

                var skill = await _dataContext.Skill.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);

                if (skill is null)
                {
                    throw new Exception("Skill not found.");

                }

                character.Skills!.Add(skill);
                await _dataContext.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(character);

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
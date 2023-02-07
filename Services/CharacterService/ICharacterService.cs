
namespace net_7.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterByid(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(GetCharacterDto character);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);
    }
}
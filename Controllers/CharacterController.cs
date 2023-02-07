
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace net_7.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetCharacter(int id)
        {
            var response = await _characterService.GetCharacterByid(id);

            if (response is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto character)
        {
            return Ok(await _characterService.AddCharacter(character));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(GetCharacterDto character)
        {
            var response = await _characterService.UpdateCharacter(character);

            if (response is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);

            if (response is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            return Ok(await _characterService.AddCharacterSkill(newCharacterSkill));
        }
    }
}
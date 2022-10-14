using AutoMapper;
using dotnet_rpg.DTO.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private readonly IMapper _mapper;
    public CharacterService(IMapper mapper)
    {
      _mapper = mapper;
    }

    private static List<Character> characters = new List<Character> {
        new Character(),
        new Character { Id = 1, Name = "Sam" }
    };
    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {

      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

      Character character = _mapper.Map<Character>(newCharacter);
      character.Id = characters.Max(c => c.Id) + 1;

      characters.Add(character);

      serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
      var serviceResponse = new ServiceResponse<GetCharacterDto>();
      // serviceResponse.Data = characters.FirstOrDefault(c => c.Id == id);
      var character = characters.FirstOrDefault(c => c.Id == id);
      serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
      ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
      try
      {
        Character character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

        _mapper.Map(updatedCharacter, character); // automapper updates our character object with the updatedCharacter object
        // character.Name = updatedCharacter.Name;
        // character.Class = updatedCharacter.Class;
        // character.Defense = updatedCharacter.Defense;
        // character.HitPoints = updatedCharacter.HitPoints;
        // character.Intelligence = updatedCharacter.Intelligence;
        // character.Strength = updatedCharacter.Strength;

        response.Data = _mapper.Map<GetCharacterDto>(character);
      }
      catch (Exception ex)
      {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
      ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();
      try
      {
        Character character = characters.First(c => c.Id == id);
        characters.Remove(character);
        response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
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
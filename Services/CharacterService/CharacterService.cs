using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.DTO.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {

    private static List<Character> characters = new List<Character> {
        new Character(),
        new Character { Id = 1, Name = "Sam" }
    };
    public async Task<ServiceResponse<List<AddCharacterDto>>> AddCharacter(Character newCharacter)
    {

      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

      characters.Add(newCharacter);
      serviceResponse.Data = characters;
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
      var serviceResponse = new ServiceResponse<List<Character>>();
      serviceResponse.Data = characters;
      return serviceResponse;
    }

    public async Task<ServiceResponse<Character>> GetCharacterById(int id)
    {
      var serviceResponse = new ServiceResponse<Character>();
      serviceResponse.Data = characters.FirstOrDefault(c => c.Id == id);
      return serviceResponse;
    }
  }
}
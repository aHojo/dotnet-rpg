using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.DTO.Character;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private readonly IMapper _mapper;
    private readonly DataContext _dbContext;

    public CharacterService(IMapper mapper, DataContext dbContext)
    {
      _mapper = mapper;
      _dbContext = dbContext;
    }
    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {

      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

      Character character = _mapper.Map<Character>(newCharacter);
      // character.Id = characters.Max(c => c.Id) + 1;
      // characters.Add(character);
      _dbContext.Add(character);
      await _dbContext.SaveChangesAsync();

      serviceResponse.Data = await  _dbContext.Characters
        .Select(c => _mapper.Map<GetCharacterDto>(c))
        .ToListAsync();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      var dbCharacters =  await  _dbContext.Characters.ToListAsync();
      serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
      var serviceResponse = new ServiceResponse<GetCharacterDto>();
      // serviceResponse.Data = characters.FirstOrDefault(c => c.Id == id);
      // var character = characters.FirstOrDefault(c => c.Id == id);
      var dbCharacter = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == id); 
      serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
      ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
      try
      {
        var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

       // _mapper.Map(updatedCharacter, character); // automapper updates our character object with the updatedCharacter object
        character.Name = updatedCharacter.Name;
        character.Class = updatedCharacter.Class;
        character.Defense = updatedCharacter.Defense;
        character.HitPoints = updatedCharacter.HitPoints;
        character.Intelligence = updatedCharacter.Intelligence;
        character.Strength = updatedCharacter.Strength;
        await _dbContext.SaveChangesAsync();
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
        Character character = await _dbContext.Characters.FirstAsync(c => c.Id == id);
        _dbContext.Characters.Remove(character);
        await _dbContext.SaveChangesAsync();

        response.Data = await _dbContext.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
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
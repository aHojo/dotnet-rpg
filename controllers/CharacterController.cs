using System.Runtime.CompilerServices;
using dotnet_rpg.DTO.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
  private readonly ICharacterService _characterService;

  public CharacterController(ICharacterService characterService)
  {
    _characterService = characterService;
  }
  [HttpGet()]
  public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
  {
    var characters = _characterService.GetAllCharacters();
    return Ok(characters);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
  {
    return Ok(_characterService.GetCharacterById(id));
  }

  [HttpPost]
  public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
  {
    var characters = _characterService.AddCharacter(newCharacter);
    return Ok(characters);
  }


  [HttpPut]
  public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
  {
    var response = await _characterService.UpdateCharacter(updatedCharacter);
    if (response.Data == null)
    {
      return NotFound(response);
    }
    return Ok(response);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id)
  {
    var response = await _characterService.DeleteCharacter(id);
    if (response.Data == null)
    {
      return NotFound(response);
    }
    return Ok(response);
  }
}

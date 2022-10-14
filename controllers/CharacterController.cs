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
  public async Task<ActionResult<ServiceResponse<List<AddCharacterDto>>>> AddCharacter(Character newCharacter)
  {
    var characters = _characterService.AddCharacter(newCharacter);
    return Ok(characters);
  }
}

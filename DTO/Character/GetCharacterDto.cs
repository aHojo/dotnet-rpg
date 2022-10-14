using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.DTO.Character
{
  public class GetCharacterDto
  {
    public int Id { get; set; }
    public string? Name { get; set; }
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;

    public RpgClass Class { get; set; } = RpgClass.Mage;
  }
}
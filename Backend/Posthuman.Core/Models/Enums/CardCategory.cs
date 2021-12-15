using System;

namespace Posthuman.Core.Models.Enums
{
    [Flags]
    public enum CardCategory
    {
        None = 0,

        Technology = 1,
        Person = 2,
        Megastructure = 4, 
        EnergySource = 8,
        Habitat = 16,
        Cosmos = 32,
        Computers = 64,
        Bionics = 128,
        Genetics = 256
    }
}

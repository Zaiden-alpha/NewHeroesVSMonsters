using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesVSMonsters.Classes.Heroes
{
    /// <summary>Nain : +2 en Endurance.</summary>
    public class Nain : Heros 
    {
        protected override int BonusEndurance => 2;

        public Nain(string nom, int x, int y) : base(nom, x, y)
        {
        }
    }
}

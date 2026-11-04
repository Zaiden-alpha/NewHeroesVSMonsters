using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesVSMonsters.Classes.Heroes
{
    /// <summary>Humain : +1 en Force et +1 en Endurance.</summary>
    public class Humain : Heros
    {
        protected override int BonusForce => 1;
        protected override int BonusEndurance => 1;

        public Humain(string nom, int x , int y) : base(nom, x, y)
        {

        }
    }
}
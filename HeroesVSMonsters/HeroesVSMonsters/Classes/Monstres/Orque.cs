using HeroesVSMonsters.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static HeroesVSMonsters.Classes.De;

namespace HeroesVSMonsters.Classes.Monstres
{
    /// <summary>Orque : +1 en Force, possède de l'or (d6 à la création).</summary>
    public class Orque : Monstre, IRiche
    {
        public int Or { get; }
        public override char Symbole => 'O';
        protected override int BonusForce => 1;

        public Orque(int x, int y) : base("Orque", x, y)
        {
            Or = Des.D6.Lancer();
        }
    }
    
}

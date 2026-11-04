using HeroesVSMonsters.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static HeroesVSMonsters.Classes.De;

namespace HeroesVSMonsters.Classes.Monstres
{
    /// <summary>Dragonnet : +1 en Endurance, possède de l'or ET peut être dépecé.</summary>
    public class Dragonnet : Monstre, IRiche, IDepecable
    {
        public int Or { get; }
        public int Cuir { get; }
        public override char Symbole => 'D';
        protected override int BonusEndurance => 1;


        public Dragonnet(int x, int y) : base("Dragonnet", x, y)
        {
            Or = Des.D6.Lancer();
            Cuir = Des.D4.Lancer();
        }
    }
}

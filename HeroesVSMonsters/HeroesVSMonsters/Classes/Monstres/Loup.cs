using HeroesVSMonsters.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static HeroesVSMonsters.Classes.De;

namespace HeroesVSMonsters.Classes.Monstres
{
    /// <summary>Loup : peut être dépecé (cuir calculé avec un d4 à la création).</summary>
    public class Loup : Monstre, IDepecable
    {
        public int Cuir { get; }
        public override char Symbole => 'L';

        public Loup(int x, int y) : base("Loup", x, y)
        {
            Cuir = Des.D4.Lancer();
        }
    }
    
}

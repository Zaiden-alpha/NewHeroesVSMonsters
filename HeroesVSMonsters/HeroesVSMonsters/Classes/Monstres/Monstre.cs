using HeroesVSMonsters.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesVSMonsters.Classes.Monstres
{
    public abstract class Monstre : Personnage
    {
        // Un monstre est caché tant que le combat n'a pas commencé
        public bool Revele { get; set; } = false;

        protected Monstre(string nom, int x, int y) : base(nom, x, y)
        {
        }
    }
}

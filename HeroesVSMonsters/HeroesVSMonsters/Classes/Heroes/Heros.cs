using HeroesVSMonsters.Classes;
using HeroesVSMonsters.Classes.Monstres;
using HeroesVSMonsters.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesVSMonsters.Classes.Heroes
{
    public abstract class Heros : Personnage
    {
        // Richesses stockées sans limite
        public int Or { get; private set; }
        public int Cuir { get; private set; }

        public override char Symbole => 'H';

        protected Heros(string nom, int x, int y) : base(nom, x, y)
        {

        }


        /// <summary>
        /// Dépouille un monstre mort : on regarde ce qu'il possède grâce aux interfaces.
        /// </summary>

        public void Depouiller (Monstre monstre)
        {
            if (monstre is IRiche riche) Or += riche.Or;
            if (monstre is IDepecable depecable) Cuir += depecable.Cuir;
        }


        /// <summary>Repos après un combat : PV restaurés.</summary>
        public void Reposer()
        {
            RestaurerPV();
        }
       
    }
    
}
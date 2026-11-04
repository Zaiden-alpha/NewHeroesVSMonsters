using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesVSMonsters.Classes
{
    /// <summary>
    /// La classe Dé : deux propriétés en lecture seule (Minimum, Maximum)
    /// et une méthode Lance qui retourne un entier aléatoire.
    /// </summary>

    public class De
    {
        public int Minimum { get; }
        public int Maximum { get; }

        public De(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public int Lancer()
        {
            return Alea.Instance.Next(Minimum, Maximum + 1);
        }

        /// <summary>
        /// Les dés utilisés dans le jeu, créés une seule fois.
        /// </summary>

        public static class Des
        {
            public static readonly De D4 = new De(1, 4);
            public static readonly De D6 = new De(1, 6);
        }

    }
}

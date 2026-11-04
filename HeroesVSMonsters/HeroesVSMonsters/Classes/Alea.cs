using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesVSMonsters.Classes
{
    /// <summary>
    /// Un seul objet Random pour tout le programme.
    /// Si on créait un "new Random()" à chaque lancer, on pourrait obtenir
    /// les mêmes valeurs plusieurs fois de suite (même graine).
    /// </summary>

    public static class Alea
    {
        public static readonly Random Instance = new Random();
    }
}

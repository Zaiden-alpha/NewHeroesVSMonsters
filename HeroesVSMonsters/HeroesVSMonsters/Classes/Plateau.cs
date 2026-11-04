using HeroesVSMonsters.Classes.Monstres;
using System;
using HeroesVSMonsters.Classes.Heroes;
using System.Collections.Generic;
using System.Text;

namespace HeroesVSMonsters.Classes
{
    public class Plateau
    {
        public const int Taille = 15;


        // Distance minimale entre deux monstres (et entre le héros et un monstre
        // au départ). 3 = deux cases vides entre eux. Tu peux mettre 2 si tu
        // interprètes l'énoncé autrement.

        private const int Espacement = 3;

        public List<Monstre> Monstres { get; } = new List<Monstre>();


        /// <summary>
        /// Distance "en carré" : le plus grand écart entre horizontal et vertical.
        /// </summary>

        private static int Distance(int x1, int y1, int x2, int y2)
        {
            return Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2));
        }


        /// <summary>Vérifie qu'une case est assez loin du héros et des autres monstres.</summary>
        private bool PositionValide(int x, int y, Heros heros)
        {
            if (Distance(x, y, heros.X, heros.Y) < Espacement) return false;
            return Monstres.All(m => Distance(x, y, m.X, m.Y) >= Espacement);
        }


        /// <summary>Crée un monstre au hasard (loup, orque ou dragonnet).</summary>
        private static Monstre CreerMonstreAleatoire(int x, int y)
        {
            switch (Alea.Instance.Next(3))
            {
                case 0: return new Loup(x, y);
                case 1: return new Orque(x, y);
                default: return new Dragonnet(x, y);
            }
        }


        /// <summary>
        /// Place des monstres au hasard. Limite d'essais pour ne pas boucler à l'infini
        /// si on ne trouve plus de place.
        /// </summary>

        public void GenererMonstres(int nombre, Heros heros)
        {
            int essais = 0;
            while (Monstres.Count < nombre && essais < 10000)
            {
                essais++;
                int x = Alea.Instance.Next(Taille);
                int y = Alea.Instance.Next(Taille);

                if (PositionValide(x, y, heros))

                    Monstres.Add(CreerMonstreAleatoire(x, y));

            }
        }


        /// <summary>
        /// Cherche un monstre juste à côté du héros (haut, bas, gauche, droite).
        /// Retourne null s'il n'y en a pas.
        /// </summary>

        public Monstre MonstreVoisin(Heros heros)
        {
            return Monstres.FirstOrDefault(m => Math.Abs(m.X - heros.X) + Math.Abs(m.Y - heros.Y) == 1);
        }


        /// <summary>Dessine le plateau : H pour le héros, monstres seulement s'ils sont révélés.</summary>
        public void Afficher(Heros heros)
        {
            for (int y = 0; y < Taille; y++)
            {
                for (int x = 0; x < Taille; x++)
                {
                    char c = '.';

                    if (heros.X == x && heros.Y == y)
                    {
                        c = heros.Symbole;
                    }
                    else
                    {
                        Monstre m = Monstres.FirstOrDefault(mo => mo.Revele && mo.X == x && mo.Y == y);
                        if (m != null) c = m.Symbole;

                    }
                    Console.Write(c + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static HeroesVSMonsters.Classes.De;   // gardé tel quel : c'est lui qui te donne accès à "Des"

namespace HeroesVSMonsters.Classes
{
    public abstract class Personnage
    {
        // =====================================================
        // CARACTÉRISTIQUES DE BASE
        // =====================================================

        // Valeurs de base, tirées à la création. Elles ne changent JAMAIS :
        // les bonus des classes s'ajoutent par-dessus sans les modifier.
        private readonly int _forceBase;
        private readonly int _enduranceBase;

        public string Nom { get; }

        // Position sur le plateau (modifiable seulement via Deplacer)
        public int X { get; private set; }
        public int Y { get; private set; }

        // Bonus propres à chaque race : 0 par défaut,
        // les classes filles les redéfinissent (override) si besoin.
        protected virtual int BonusForce => 0;
        protected virtual int BonusEndurance => 0;

        // Force et Endurance : lecture seule (pas de set).
        // Valeur finale = valeur de base + bonus de la race.
        public int Force => _forceBase + BonusForce;
        public int Endurance => _enduranceBase + BonusEndurance;



        // =====================================================
        // POINTS DE VIE
        // =====================================================

        // PV : totalement privé (ni lecture ni écriture depuis l'extérieur).
        // Les autres classes sont prévenues des changements grâce aux events.
        private int PV { get; set; }

        // PV maximum = endurance + modificateur basé sur l'endurance
        public int PVMax => Endurance + Modificateur(Endurance);

        // Un personnage est vivant tant que ses PV sont > 0
        public bool EstVivant => PV > 0;




        // =====================================================
        // EVENTS : le personnage "annonce" ce qui se passe
        // =====================================================

        // Déclenché à chaque changement de PV : (qui, PV actuels)
        public event Action<Personnage, int> PVChange;

        // Déclenché quand le personnage meurt
        public event Action<Personnage> Mort;

        // Lettre affichée sur le plateau (H, L, O, D)
        public abstract char Symbole { get; }




        // =====================================================
        // CONSTRUCTEUR
        // =====================================================

        protected Personnage(string nom, int x, int y)
        {
            Nom = nom;
            X = x;
            Y = y;

            // Force et endurance : 4 dés à 6 faces, on garde les 3 meilleurs
            _forceBase = Lancer4D6Garder3();
            _enduranceBase = Lancer4D6Garder3();

            // Les PV de départ sont les PV max.
            // (Les bonus sont des constantes dans les classes filles,
            // donc on peut les utiliser sans souci dans ce constructeur.)
            PV = PVMax;
        }



        // =====================================================
        // MÉTHODES
        // =====================================================

        /// <summary>
        /// Lance 4 dés, retire le plus petit, additionne les 3 autres.
        /// </summary>
        private static int Lancer4D6Garder3()
        {
            int[] jets = { Des.D6.Lancer(), Des.D6.Lancer(), Des.D6.Lancer(), Des.D6.Lancer() };
            return jets.Sum() - jets.Min();
        }

        /// <summary>
        /// Modificateur selon le score :
        /// &lt; 5 => -1 ; &lt; 10 => 0 ; &lt; 15 => +1 ; sinon +2
        /// </summary>
        public static int Modificateur(int caracteristique)
        {
            if (caracteristique < 5) return -1;
            else if (caracteristique < 10) return 0;
            else if (caracteristique < 15) return 1;
            else return 2;
        }

        /// <summary>
        /// Action "Frappe" : dégâts = 1d4 + modificateur de Force.
        /// Les dégâts sont retirés des PV de la cible. Retourne les dégâts infligés.
        /// </summary>
        public int Frappe(Personnage cible)
        {
            int degats = Des.D4.Lancer() + Modificateur(Force);
            if (degats < 0) degats = 0;   // pas de dégâts négatifs (ça soignerait la cible)

            cible.RecevoirDegats(degats); // on passe par une méthode au lieu de toucher PV
            return degats;
        }

        // private : appelable seulement dans Personnage (même sur un autre objet Personnage)
        private void RecevoirDegats(int degats)
        {
            PV -= degats;

            // On annonce le nouveau nombre de PV (jamais en dessous de 0)
            PVChange?.Invoke(this, Math.Max(0, PV));

            // Si on est mort, on l'annonce aussi
            if (!EstVivant) Mort?.Invoke(this);
        }

        /// <summary>Restaure tous les PV (utilisé par les héros après un combat).</summary>
        protected void RestaurerPV()
        {
            PV = PVMax;
            PVChange?.Invoke(this, PV);
        }

        /// <summary>Déplace le personnage d'un décalage (dx, dy).</summary>
        public void Deplacer(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }
}
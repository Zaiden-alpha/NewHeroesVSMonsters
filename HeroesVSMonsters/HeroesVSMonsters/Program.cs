using HeroesVSMonsters.Classes;
using HeroesVSMonsters.Classes.Heroes;
using HeroesVSMonsters.Classes.Monstres;

// =====================================================
// CRÉATION DU HÉROS
// =====================================================

Console.WriteLine("BIENVENUE DANS LE JEU !  SOYEZ PRETS  ! ");

Console.WriteLine("Nom de ton héros ? ");
string nomHeros = Console.ReadLine();
if (string.IsNullOrWhiteSpace(nomHeros))
{
    nomHeros = "Héros";
}

Console.WriteLine("Humain (1) ou Nain (2)");
string choix = Console.ReadLine();

// Le héros démarre au centre du plateau
int depart = Plateau.Taille / 2;
Heros heros = choix == "2" ? new Nain(nomHeros, depart, depart) : new Humain(nomHeros, depart, depart);

// PV est privé : on garde le dernier chiffre annoncé par l'event PVChange
int pvHeros = heros.PVMax;
heros.PVChange += (p, pv) => pvHeros = pv;

Console.WriteLine($"\n{heros.Nom} : Force {heros.Force}, Endurance {heros.Endurance}, PV {pvHeros}");
Console.WriteLine("Appuie sur une touche pour entrer dans la forêt de Shorewood...");
Console.ReadKey(true);

// =====================================================
// CRÉATION DU PLATEAU
// =====================================================

Plateau plateau = new Plateau();
plateau.GenererMonstres(10, heros);

// =====================================================
// BOUCLE DE JEU
// =====================================================

while (heros.EstVivant && plateau.Monstres.Count > 0)
{
    Console.Clear();
    Console.WriteLine($"{heros.Nom} | PV {pvHeros}/{heros.PVMax} | Or {heros.Or} | Cuir {heros.Cuir} | Monstres restants : {plateau.Monstres.Count}");
    Console.WriteLine("Déplacement : flèches du clavier\n");
    plateau.Afficher(heros);

    // Lecture de la touche pour déplacer le héros
    ConsoleKey touche = Console.ReadKey(true).Key;
    int dx = 0, dy = 0;
    switch (touche)
    {
        case ConsoleKey.UpArrow: dy = -1; break;
        case ConsoleKey.DownArrow: dy = 1; break;
        case ConsoleKey.LeftArrow: dx = -1; break;
        case ConsoleKey.RightArrow: dx = 1; break;
    }

    // On vérifie qu'on reste dans le plateau avant de bouger
    int nx = heros.X + dx;
    int ny = heros.Y + dy;
    if (nx >= 0 && nx < Plateau.Taille && ny >= 0 && ny < Plateau.Taille)
        heros.Deplacer(dx, dy);


    // Un monstre est à côté ? Le combat démarre automatiquement
    Monstre voisin = plateau.MonstreVoisin(heros);
    if (voisin != null)
        Combat(voisin);
}   // <-- la boucle se ferme ICI

// =====================================================
// FIN DE PARTIE
// =====================================================

Console.Clear();

if (heros.EstVivant)
    Console.WriteLine($"Bravo {heros.Nom}, tu as survécu à la forêt de Shorewood ! Butin : {heros.Or} pièces d'or et {heros.Cuir} morceaux de cuir.");
else
    Console.WriteLine($"Dommage {heros.Nom}, tu es mort dans la forêt de Shorewood... Butin : {heros.Or} pièces d'or et {heros.Cuir} morceaux de cuir.");


// =====================================================
// MÉTHODE COMBAT
// (pas de static : elle utilise heros, plateau et pvHeros définis plus haut)
// =====================================================

void Combat(Monstre monstre)
{
    // Le monstre apparaît à l'écran
    monstre.Revele = true;

    // Même principe que pour le héros : on suit les PV du monstre via l'event
    int pvMonstre = monstre.PVMax;
    monstre.PVChange += (p, pv) => pvMonstre = pv;

    // Quand le monstre meurt, c'est LUI qui prévient, et on réagit ici
    monstre.Mort += _ =>
    {
        heros.Depouiller(monstre);
        plateau.Monstres.Remove(monstre);
        heros.Reposer();
    };

    Console.Clear();
    plateau.Afficher(heros);
    Console.WriteLine($"\nUn {monstre.Nom} apparaît ! (Force {monstre.Force}, Endurance {monstre.Endurance}, PV {pvMonstre})\n");

    // Combat au tour par tour jusqu'à la mort de l'un des deux
    while (heros.EstVivant && monstre.EstVivant)
    {
        int degats = heros.Frappe(monstre);
        Console.WriteLine($"{heros.Nom} frappe le {monstre.Nom} : -{degats} PV (reste {pvMonstre})");
        if (!monstre.EstVivant) break;   // <-- le break est de retour : le monstre mort ne frappe pas

        degats = monstre.Frappe(heros);
        Console.WriteLine($"Le {monstre.Nom} frappe {heros.Nom} : -{degats} PV (reste {pvHeros})");
    }

    // Message de victoire APRÈS le dernier coup
    if (!monstre.EstVivant)
        Console.WriteLine($"\n{monstre.Nom} vaincu ! Le héros se repose et récupère tous ses PV.");

    Console.WriteLine("\nAppuie sur une touche pour continuer...");
    Console.ReadKey(true);
}
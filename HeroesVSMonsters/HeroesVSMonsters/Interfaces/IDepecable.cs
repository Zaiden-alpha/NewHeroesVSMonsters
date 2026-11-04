using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesVSMonsters.Interfaces
{
    /// <summary>Un monstre qui peut être dépecé (donne du cuir).</summary>
    public interface IDepecable
    {
        int Cuir { get; }
    }
}

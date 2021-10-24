namespace Uppgift3_TheGame.POCO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    static public class Equipment
    {
        
        static public Weapon Fists = new Weapon
        {
            Name = "Fists",
            Damage = 0,
            Price = 0,
            FlavourTexts = new string[4]
            {
                "punch for",
                "hit the enemy where it really hurts for",
                "tickle the enemy for",
                "punch a big hole in the air for"
            }
        };
        static public Weapon Stick = new Weapon
        {
            Name = "Stick",
            Damage = 5,
            Price = 5,
            FlavourTexts = new string[4]
            {
                "thwack the enemy with your stick for",
                "poke the enemy in the eye with your stick, hurtfully doing",
                "poke the enemy ineffectually with you stick, scratching it for",
                "you hit the ground beside the enemy with your stick for a total of"
            }
        };
        static public Weapon Dagger = new Weapon
        {
            Name = "Dagger",
            Damage = 10,
            Price = 50,
            FlavourTexts = new string[4]
            {
                "stick the enemy with your dagger for",
                "stab the enemy in its vunerables with your dagger, doing ",
                "scratch the enemy with your dagger for",
                "drop your dagger to the ground, doing"
            }
        };
        static public Weapon Sword = new Weapon
        {
            Name = "Sword",
            Damage = 15,
            Price = 100,
            FlavourTexts = new string[4]
            {
                "swing your sword and hit for",
                "strike a flurry of blows with your sword, inflicting",
                "grimace as your sword glances off the enemy, doing",
                "try to juggle your sword, managing to do"
            }
        };
        static public Weapon GreatAxe = new Weapon
        {
            Name = "Great Axe",
            Damage = 20,
            Price = 200,
            FlavourTexts = new string[4]
            {
                "swing your great axe and hit for",
                "make a mighty swing with your great axe and brutally hit for",
                "barely manage to connect with your great axe, doing",
                "manage to miss your toe with your great axe, inflicting"
            }
        };
        static public Weapon MagicSword = new Weapon
        {
            Name = "Magic Sword of Doom",
            Damage = 40,
            Price = 500,
            FlavourTexts = new string[4]
            {
                "swing your sparkling sword and hit, doing",
                "strike a migthy blow, your sword crackling with energy, and hit, inflicting",
                "swing but the light in your sword fades and you hit weakly for",
                "realize that your sword might have a mind of it's own as it twists in your hand and misses, doing"
            }
        };
        static public List<Weapon> WeaponsList = new() { Stick, Dagger, Sword, GreatAxe, MagicSword };
    }
}

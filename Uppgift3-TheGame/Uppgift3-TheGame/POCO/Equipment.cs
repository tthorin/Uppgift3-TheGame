namespace Uppgift3_TheGame.POCO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Interface;

    static internal class Equipment
    {
        #region Weapons
        static internal Weapon Fists = new Weapon
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
        static internal Weapon Stick = new Weapon
        {
            Name = "Stick",
            Damage = 5,
            Price = 5,
            FlavourTexts = new string[4]
            {
                "thwack the enemy with your stick for",
                "poke the enemy in the eye with your stick, painfully doing",
                "poke the enemy ineffectually with you stick, scratching it for",
                "hit the ground beside the enemy with your stick for a total of"
            }
        };
        static internal Weapon Dagger = new Weapon
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
        static internal Weapon Sword = new Weapon
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
        static internal Weapon GreatAxe = new Weapon
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
        static internal Weapon MagicSword = new Weapon
        {
            Name = "Magic Sword of Doom",
            Damage = 40,
            Price = 500,
            FlavourTexts = new string[4]
            {
                "swing your glowing sword and hit, doing",
                "strike a migthy blow, your sword crackling with energy, and hit, inflicting",
                "swing but the light in your sword fades and you hit weakly for",
                "realize that your sword might have a mind of it's own as it twists in your hand and misses, doing"
            },
        };

        static internal List<Weapon> WeaponsList = new() { Stick, Dagger, Sword, GreatAxe, MagicSword };

        #endregion Weapons
        #region Armor
        static internal Armor BirthdaySuit = new Armor
        {
            Name = "Birthday suit",
            Protection = 0,
            Price = 0,
            FlavourTexts = new string[4]
            {
                "avoid the worst of the hit and dodge",
                "nimbly dodge out of the way and avoid",
                "stumble but manage to block",
                "fall on your face and block"
            }
        };
        static internal Armor Cloth = new Armor
        {
            Name = "Cloth coverings",
            Protection = 5,
            Price = 5,
            FlavourTexts = new string[4]
            {
                "move out of the way and avoid",
                "tangle the enemys attack with your cloth and blunts its force with",
                "stumble on a loose piece of cloth but manage to avoid",
                "realize you shouldn't have wrapped yourself as a mummy when you block"
            }
        };
        static internal Armor Leather = new Armor
        {
            Name = "Leather armor",
            Protection = 10,
            Price = 50,
            FlavourTexts = new string[4]
            {
                "dodge of the way and avoid",
                "sigh in relief as your enemys attack scrape along your leathers and you block",
                "misjudge your enemys speed but manage to avoid",
                "fall straight into the incoming attack and block"
            }
        };
        static internal Armor Chainmail = new Armor
        {
            Name = "Chainmail armor",
            Protection = 15,
            Price = 100,
            FlavourTexts = new string[4]
            {
                 "skillfully block",
                 "critically block",
                 "stumble but manage to block",
                 "trip over your own feet and block"
            }
        };
        static internal Armor Plate = new Armor
        {
            Name = "Plate armor",
            Protection = 20,
            Price = 200,
            FlavourTexts = new string[4]
            {
                 "utilize your plate armor and let it absorb",
                 "expertly twist out of the way, your plate armor blocking",
                 "are hindered by the weight of your plate armor but it still blocks",
                 "are blinded by your helmet, maybe try putting it on the other way? You avoid"
            }
        };
        static internal Armor PowerArmor = new Armor
        {
            Name = "Power armor",
            Protection = 40,
            Price = 500,
            FlavourTexts = new string[4]
            {
                 "grin as the mystical energies imbued in your armor block",
                 "laugh at your enemy as a mystic force reaches out and almost halts the incoming attack, blocking",
                 "stumble but still absorb",
                 "feel like you want to cry when the mystical energies in your armor winks out and absorbs"
            }
        };
        static internal List<Armor> ArmorList = new() { Cloth,Leather,Chainmail,Plate,PowerArmor };
        #endregion Armor
        static internal List<ISellable> EquipmentList = new() { Stick, Dagger, Sword, GreatAxe, MagicSword, Cloth, Leather, Chainmail, Plate, PowerArmor };

    }
}

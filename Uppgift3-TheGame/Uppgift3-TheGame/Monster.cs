namespace Uppgift3_TheGame
{
    public class Monster : Character
    {
        public int XpValue { get; set; }

        public Monster(int powerUp)
        {   
            Offense += powerUp /2;
            Defense += powerUp /2;
            Damage += powerUp * 2;
            Toughness += powerUp * 2;
            MaxHealth = powerUp * 30;
            CurrentHealth = MaxHealth;
            Gold = rng.Next(1, 11) * powerUp;
            XpValue = powerUp;
        }
    }
}
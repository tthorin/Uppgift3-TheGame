namespace Uppgift3_TheGame.POCO
{
    
    public class Weapon
    {
        public string Name { get; set; } = "";
        public int Damage { get; set; } = 0;
        public int Price { get; set; } = 0;
        public string[] FlavourTexts { get; set; } = new string[4];
    }
}

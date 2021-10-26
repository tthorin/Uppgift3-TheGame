namespace Uppgift3_TheGame.POCO
{
    using Interface;
    internal class Armor:ISellable
    {
        public string Name { get; set; } = "";
        public int Protection { get; set; } = 0;
        public int Price { get; set; } = 0;
        public string[] FlavourTexts { get; set; } = new string[4];
    }
}

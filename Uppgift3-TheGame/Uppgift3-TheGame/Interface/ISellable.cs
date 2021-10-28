namespace Uppgift3_TheGame.Interface
{
    //todo: ta bort?
    internal interface ISellable
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int EffectiveValue { get; }
    }
}

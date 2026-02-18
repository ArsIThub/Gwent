namespace Gwent.Cards.Interfaces.PropertyInterfaces
{
    public interface IRareCard
    {
        bool IsRare { get; }
        string RarityDescription { get; }
    }
}
namespace Gwent.Cards.Interfaces.AbilityInterfaces
{
    public interface IConnection
    {
        int StrengthBonusPerConnection { get; }

        void ExecuteConnectionAbility();
    }
}
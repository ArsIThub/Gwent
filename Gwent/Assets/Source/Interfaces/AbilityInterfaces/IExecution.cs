namespace Gwent.Cards.Interfaces.AbilityInterfaces
{
    public interface IExecution
    {
        int TargetsCount { get; }
        bool TargetEnemyOnly { get; }

        void ExecuteExecutionAbility();
    }
}
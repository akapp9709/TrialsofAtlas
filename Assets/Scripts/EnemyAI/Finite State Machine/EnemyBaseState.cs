public abstract class EnemyBaseState
{
    protected EnemyStateController Enemy;
    public abstract void EnterState(EnemyStateController enemy);

    public abstract void UpdateState(EnemyStateController enemy);

    public EnemyStateController Controller => Enemy;

}

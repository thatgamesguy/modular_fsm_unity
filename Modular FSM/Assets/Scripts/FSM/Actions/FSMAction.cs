public abstract class FSMAction
{
    public abstract void PerformAction();
    protected abstract bool OkToAct();
    public abstract void Enter();
    public abstract void Exit();

}


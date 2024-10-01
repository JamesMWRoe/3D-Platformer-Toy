
public abstract class BaseState
{
    public string name;
    
    public abstract void OnStart();

    public abstract void OnUpdate();
    
    public abstract void OnEnd();

    protected abstract void CheckForStateTransition();

}

using UnityEngine;

public class StateMachine : MonoBehaviour
{
  protected BaseState currentState;

  protected void Awake()
  {
    TransitionToState(GetInitialState());
  }

  // Update is called once per frame
  protected void Update()
  {
    if (currentState != null)
    {  currentState.OnUpdate();  }
  }

  public void TransitionToState(BaseState newState)
  {
    
    if (newState == null)
    {  return;  }

    print("New State Entered: " + newState.name);
    
    if (currentState != null)
    {   currentState.OnEnd();   }

    currentState = newState;

    currentState.OnStart();
  }

  protected virtual BaseState GetInitialState()
  { return null; }

  

}

using UnityEngine;
public class PlayerGroundedState : PlayerBaseState
{
  public PlayerGroundedState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  public override void OnStart()
  {}

  public override void OnEnd()
  {}

  public override void OnUpdate()
  {
    CheckForStateTransition();
  }

  protected override void CheckForStateTransition()
  {
    if (stateMachine.jumpAction.WasPressedThisFrame())
    {  
      stateMachine.TransitionToState(states.Jump());
      return;
    }
  }

}

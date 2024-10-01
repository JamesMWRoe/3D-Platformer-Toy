using UnityEngine;

public class PlayerAerialState : PlayerBaseState
{
  public PlayerAerialState (string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  public override void OnStart()
  {}

  public override void OnEnd()
  {}

  public override void OnUpdate()
  {
    stateMachine.zVelocity += stateMachine.gravity * Time.deltaTime;

    CheckForStateTransition();
  }

  protected override void CheckForStateTransition()
  {
    if (stateMachine.controller.isGrounded)
    {
      stateMachine.TransitionToState(states.Idle());
      return;
    }
  }
}

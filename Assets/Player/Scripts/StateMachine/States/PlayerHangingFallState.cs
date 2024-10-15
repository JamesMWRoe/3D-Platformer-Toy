using UnityEngine;

public class PlayerHangingFallState : PlayerBaseState
{
  public PlayerHangingFallState (string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }


  public override void OnStart()
  {}

  public override void OnEnd()
  {
    stateMachine.zVelocity = 0;
  }

  public override void OnUpdate()
  {
    stateMachine.zVelocity += stateMachine.gravity * Time.deltaTime;

    stateMachine.Move();


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
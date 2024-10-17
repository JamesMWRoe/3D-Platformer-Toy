using UnityEngine;
public class PlayerMoveState : PlayerGroundedState
{


  public PlayerMoveState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  public override void OnStart()
  {}

  public override void OnEnd()
  {}

  public override void OnUpdate()
  {
    stateMachine.animator.SetFloat("moveSpeed", stateMachine.moveInput.magnitude);

    stateMachine.xyVelocity = stateMachine.moveDirection * stateMachine.speed;

    stateMachine.Move();
    stateMachine.UpdateRotation();

    base.OnUpdate();
  }

  protected override void CheckForStateTransition()
  {
    if (stateMachine.moveInput.magnitude < Mathf.Epsilon)
    {
      stateMachine.TransitionToState(states.Idle());
      return;
    }

    if (stateMachine.crouchAction.WasPressedThisFrame())
    {
      stateMachine.TransitionToState(states.Slide());
    }
    
    base.CheckForStateTransition();
  }
}

using UnityEngine;
public class PlayerMoveState : PlayerGroundedState
{
  Vector2 moveInput;

  public PlayerMoveState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  public override void OnStart()
  {}

  public override void OnEnd()
  {}

  public override void OnUpdate()
  {
    moveInput = stateMachine.moveAction.ReadValue<Vector2>();

    Vector2 forwardDir = new Vector2 (stateMachine.cameraTransform.forward.x, stateMachine.cameraTransform.forward.z).normalized;
    Vector2 rightDir = new Vector2 (stateMachine.cameraTransform.right.x, stateMachine.cameraTransform.right.z).normalized;

    Vector2 moveDirection = moveInput.x*rightDir + moveInput.y*forwardDir;
    stateMachine.xyVelocity = moveDirection * stateMachine.speed;

    base.OnUpdate();
  }

  protected override void CheckForStateTransition()
  {
    if (moveInput.magnitude < Mathf.Epsilon)
    {
      stateMachine.TransitionToState(states.Idle());
      return;
    }
    
    base.CheckForStateTransition();
  }
}

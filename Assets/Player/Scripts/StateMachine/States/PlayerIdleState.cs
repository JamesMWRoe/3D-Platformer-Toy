using UnityEngine;
public class PlayerIdleState : PlayerGroundedState
{
  public PlayerIdleState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  public override void OnStart()
  {
    stateMachine.xyVelocity = Vector2.zero;
  }

  public override void OnEnd()
  {}

  public override void OnUpdate()
  {
    base.OnUpdate();
  }

  protected override void CheckForStateTransition()
  {
    Vector2 moveInput = stateMachine.moveAction.ReadValue<Vector2>();
    if (moveInput.magnitude > 0)
    {
      stateMachine.TransitionToState(states.Move());
      return;
    }

    base.CheckForStateTransition();
  }

}

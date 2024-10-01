using UnityEngine;

public class PlayerJumpState : PlayerAerialState
{
  public PlayerJumpState (string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  public override void OnStart()
  {
    stateMachine.zVelocity += stateMachine.jumpVelocity;
  }

  public override void OnEnd()
  {}

  public override void OnUpdate()
  {
    if (stateMachine.jumpAction.WasReleasedThisFrame())
    {  stateMachine.zVelocity /= 3;  }

    base.OnUpdate();
  }

  protected override void CheckForStateTransition()
  {
    if (stateMachine.zVelocity <= 0)
    {  stateMachine.TransitionToState(states.Fall());  }
    base.CheckForStateTransition();
  }
}

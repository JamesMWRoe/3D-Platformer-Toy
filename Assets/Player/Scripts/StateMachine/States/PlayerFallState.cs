using UnityEngine;

public class PlayerFallState : PlayerAerialState
{
  public PlayerFallState (string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  public override void OnStart()
  {}

  public override void OnEnd()
  {
    stateMachine.velocity.y = 0;
  }

  public override void OnUpdate()
  { 
    base.OnUpdate();
  }

  protected override void CheckForStateTransition()
  {
    base.CheckForStateTransition();
  }
}

using UnityEngine;
public class PlayerGroundedState : PlayerBaseState
{
  public PlayerGroundedState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  protected int groundLayerMask = 1 << 6;

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

    if (!IsGrounded())
    {
      stateMachine.TransitionToState(states.Fall());
      return;
    }
  }

  protected bool IsGrounded()
  {
    bool groundCheck = Physics.Raycast(stateMachine.transform.position - (0.01f * Vector3.down), Vector3.down, 0.1f, groundLayerMask);

    return groundCheck;
  }

}

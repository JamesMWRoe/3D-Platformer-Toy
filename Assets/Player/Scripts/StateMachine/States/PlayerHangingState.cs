using UnityEngine;
public class PlayerHangingState : PlayerBaseState
{
  public PlayerHangingState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  int groundLayerMask = 1 << 6;

  protected Vector3 shimmyDirection;

  public override void OnStart()
  {
    stateMachine.animator.SetFloat("moveSpeed", 0);
    stateMachine.animator.SetBool("isHanging", true);

    shimmyDirection = new Vector3(stateMachine.transform.right.x, 0, stateMachine.transform.right.z);

  }

  public override void OnEnd()
  {
    stateMachine.animator.SetBool("isHanging", false);
  }

  public override void OnUpdate()
  {
    CheckForStateTransition();
  }

  protected override void CheckForStateTransition()
  {
    if(stateMachine.moveInput.y < 0)
    {
      stateMachine.TransitionToState(states.HangingFall());
      return;
    }

    if(stateMachine.jumpAction.WasPressedThisFrame())
    {
      stateMachine.TransitionToState(states.HangingClimb());
      return;
    }
  }

  protected bool IsAtLedgeEdge()
  {
    Vector3 pointToCheck = stateMachine.ledgeCheckPoint.position + 0.5f*stateMachine.moveInput.x*new Vector3(shimmyDirection.x, 0, shimmyDirection.y);
    bool isOnLedge = Physics.Raycast(pointToCheck, Vector3.down, 0.5f, 1 << 6);

    return !isOnLedge;
  }
}

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
    stateMachine.velocity.y += stateMachine.gravity * Time.deltaTime;

    stateMachine.Move();

    stateMachine.UpdateRotation();

    CheckForStateTransition();
  }

  protected override void CheckForStateTransition()
  {
    if (stateMachine.controller.isGrounded)
    {
      stateMachine.TransitionToState(states.Idle());
      return;
    }

    if (IsLedgeInFront())
    {
      stateMachine.TransitionToState(states.HangingIdle());
      return;
    }
  }

  bool IsLedgeInFront()
  {
    RaycastHit ledgeHitInfo;

    bool isLedge = Physics.Raycast(stateMachine.ledgeCheckPoint.position, Vector3.down, out ledgeHitInfo, 0.15f, 1<<6);
    if (!isLedge) return false;

    Vector3 pointToCastFrom = new Vector3(stateMachine.transform.position.x, ledgeHitInfo.point.y - 0.1f, stateMachine.transform.position.z);

    bool isInRange = Physics.Raycast(pointToCastFrom, stateMachine.transform.forward, 1.0f, 1<<6);
    return isInRange;
  }
}

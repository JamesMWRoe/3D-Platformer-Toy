using UnityEngine;
public class PlayerIdleState : PlayerGroundedState
{
  public PlayerIdleState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  public override void OnStart()
  {
    stateMachine.velocity = Vector3.zero;
    stateMachine.animator.SetFloat("moveSpeed", 0);
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

    if(stateMachine.crouchAction.WasPerformedThisFrame() && OnSlope())
    {
      stateMachine.TransitionToState(states.Slide());
      return;
    }

    base.CheckForStateTransition();
  }


  bool OnSlope()
  {
    RaycastHit groundInfo;
    Physics.Raycast(stateMachine.transform.position, Vector3.down, out groundInfo, 0.2f, groundLayerMask);

    Vector3 comparisonVector = Vector3.ProjectOnPlane(Vector3.down, groundInfo.normal);

    if (comparisonVector.magnitude < Mathf.Epsilon)
    {  return false;  }

    return true;
  }

}

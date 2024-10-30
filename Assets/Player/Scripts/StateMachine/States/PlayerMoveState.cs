using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerMoveState : PlayerGroundedState
{
  public PlayerMoveState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  Vector3 groundNormal;

  public override void OnStart()
  {}

  public override void OnEnd()
  {}

  public override void OnUpdate()
  {
    UpdateGroundNormal();
    Vector3 moveDirection3D = new Vector3 (stateMachine.moveDirection.x, 0, stateMachine.moveDirection.y);
    Vector3 moveDirection = Vector3.ProjectOnPlane(moveDirection3D, groundNormal).normalized;

    stateMachine.animator.SetFloat("moveSpeed", stateMachine.moveInput.magnitude);

    stateMachine.velocity = moveDirection * stateMachine.speed;

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

  protected void UpdateGroundNormal()
  {
    RaycastHit hitInfo;
    Physics.Raycast(stateMachine.transform.position, Vector3.down, out hitInfo, 0.51f, groundLayerMask);

    groundNormal = hitInfo.normal;
  }
}

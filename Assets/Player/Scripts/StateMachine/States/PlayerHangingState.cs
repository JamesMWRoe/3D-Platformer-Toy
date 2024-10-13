using UnityEngine;
public class PlayerHangingState : PlayerBaseState
{
  public PlayerHangingState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  int groundLayerMask = 1 << 6;

  Vector3 ledgeOffset = new Vector3(0, -1.81f, 0.43f);

  public override void OnStart()
  {
    stateMachine.animator.SetFloat("moveSpeed", 0);
    stateMachine.animator.SetBool("isHanging", true);

  }

  public override void OnEnd()
  {
    stateMachine.animator.SetBool("isHanging", false);
  }

  public override void OnUpdate()
  {
    stateMachine.animator.SetFloat("shimmySpeed", stateMachine.moveInput.x);
    CheckForStateTransition();
  }

  protected override void CheckForStateTransition()
  {

  }

}

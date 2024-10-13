using UnityEngine;
public class PlayerHangingMoveState : PlayerHangingState
{
  public PlayerHangingMoveState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  float shimmySpeed = -0.5f;
  Vector2 shimmyDirection;

  public override void OnStart()
  {
    base.OnStart();
    shimmyDirection = new Vector2(stateMachine.transform.right.x, stateMachine.transform.right.z);
  }

  public override void OnEnd()
  {
    base.OnEnd();
  }

  public override void OnUpdate()
  {
    stateMachine.animator.SetFloat("shimmySpeed", stateMachine.moveInput.x);

    Debug.Log("right direction: "+ stateMachine.transform.right);


    stateMachine.xyVelocity = -stateMachine.moveInput.x * shimmyDirection * shimmySpeed;
    stateMachine.Move();

    CheckForStateTransition();
  }

  protected override void CheckForStateTransition()
  {

  }

}

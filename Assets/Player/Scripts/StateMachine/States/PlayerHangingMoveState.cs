using UnityEngine;
public class PlayerHangingMoveState : PlayerHangingState
{
  public PlayerHangingMoveState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  float shimmySpeed = -0.5f;
  

  public override void OnStart()
  {
    base.OnStart();
    
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
    
    base.OnUpdate();
    
  }

  protected override void CheckForStateTransition()
  {
    if (stateMachine.moveInput.x*stateMachine.moveInput.x < Mathf.Epsilon)
    {
      stateMachine.TransitionToState(states.HangingIdle());
      return;
    }

    if (IsAtLedgeEdge())
    {
      stateMachine.TransitionToState(states.HangingIdle());
      return;
    }

    base.CheckForStateTransition();
  }
}

using UnityEngine;
public class PlayerHangingClimbState : PlayerHangingState
{
  public PlayerHangingClimbState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  Vector3 endPosition = new Vector3(0, 3f, 1f);

  public override void OnStart()
  {
    base.OnStart();
    stateMachine.animator.SetFloat("yVelocity", 1);
    stateMachine.animator.SetFloat("shimmySpeed", 0);
    stateMachine.animator.SetBool("isHanging", false);
    

    stateMachine.xyVelocity = Vector2.zero;
    stateMachine.zVelocity = 0;

  }

  public override void OnEnd()
  {
    stateMachine.transform.position = stateMachine.climbEndPoint.position;
    Physics.SyncTransforms();

    Debug.Log("moved to position: " + stateMachine.transform.position);
    stateMachine.animationEventHandler.ClimbFinished();
  }

  public override void OnUpdate()
  {
    base.OnUpdate();
  }

  protected override void CheckForStateTransition()
  {

    if (stateMachine.animationEventHandler.HasClimbed())
    {
      stateMachine.TransitionToState(states.Idle());
      return;
    }

    base.CheckForStateTransition();
  }

}
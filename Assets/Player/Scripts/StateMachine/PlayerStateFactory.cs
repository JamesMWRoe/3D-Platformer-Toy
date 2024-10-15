
public class PlayerStateFactory
{
  PlayerStateMachine stateMachine;

  public PlayerStateFactory(PlayerStateMachine currentStateMachine)
  {
    this.stateMachine = currentStateMachine;
  }

  public PlayerBaseState Idle()
  {  return new PlayerIdleState("idle", stateMachine, this); }

  public PlayerBaseState Move()
  {  return new PlayerMoveState("move", stateMachine, this);  }

  public PlayerBaseState Jump()
  {  return new PlayerJumpState("jump", stateMachine, this);  }

  public PlayerBaseState Fall()
  {  return new PlayerFallState("fall", stateMachine, this);  }

  public PlayerBaseState HangingIdle()
  {  return new PlayerHangingIdleState("hangingIdle", stateMachine, this);  }

  public PlayerBaseState HangingMove()
  {  return new PlayerHangingMoveState("hangingMove", stateMachine, this);  }

  public PlayerBaseState HangingFall()
  {  return new PlayerHangingFallState("hangingFall",stateMachine, this);  }

  public PlayerBaseState HangingClimb()
  {  return new PlayerHangingClimbState("hangingClimb", stateMachine, this);  }
}

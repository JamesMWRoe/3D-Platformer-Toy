
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
}

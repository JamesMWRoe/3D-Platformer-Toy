using UnityEngine;
public class PlayerSlideState : PlayerBaseState
{
  public PlayerSlideState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  int groundLayerMask = 1 << 6;
  Vector3 currentGroundNormal;
  float slideSpeed = 5;
  float currentMaxSpeed;
  float maxSpeedFactor;

  public override void OnStart()
  {
    maxSpeedFactor = 10*stateMachine.speed;
    currentMaxSpeed = stateMachine.velocity.magnitude;

    Debug.Log("initial max speed: " + currentMaxSpeed);
  }

  public override void OnEnd()
  {}

  public override void OnUpdate()
  {
    UpdateGroundNormal();

    Vector3 inputAsVector3 = new Vector3(stateMachine.moveDirection.x, 0, stateMachine.moveDirection.y);
    Vector3 inputDirectionAlongSurface = Vector3.ProjectOnPlane(inputAsVector3, currentGroundNormal).normalized;
    Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.down, currentGroundNormal).normalized;

    Vector3 slideDirection = (slopeDirection + 0.5f*inputDirectionAlongSurface).normalized;

    Vector3 newVelocity = stateMachine.velocity + slideDirection*slideSpeed;

    Debug.Log("slope direction: " + slopeDirection);
    Debug.Log("slide direction: " + slideDirection);

    Debug.Log("new velocity before clamping: " + newVelocity);

    if (slopeDirection.sqrMagnitude < Mathf.Epsilon)
    {
      Debug.Log("new max speed: " + currentMaxSpeed);
      currentMaxSpeed *= 0.99f;
    }
    else
    {
      Debug.Log("slopedirection is non zero");
      currentMaxSpeed = -slopeDirection.y*maxSpeedFactor;
      Debug.Log("new max speed: " + currentMaxSpeed);
    }

    newVelocity = Vector3.ClampMagnitude(newVelocity, currentMaxSpeed);

    Debug.Log("max velocity after clamping: " + newVelocity);
    Debug.Log("\n");

    stateMachine.velocity = newVelocity;

    stateMachine.Move();

    CheckForStateTransition();
  }

  protected override void CheckForStateTransition()
  {
    if (currentMaxSpeed < 0.5)
    {
      stateMachine.TransitionToState(states.Idle());
    }
  }

  protected void UpdateGroundNormal()
  {
    RaycastHit groundInfo;
    Physics.Raycast(stateMachine.transform.position, Vector3.down, out groundInfo, 0.2f, groundLayerMask);

    currentGroundNormal = groundInfo.normal;
  }

}

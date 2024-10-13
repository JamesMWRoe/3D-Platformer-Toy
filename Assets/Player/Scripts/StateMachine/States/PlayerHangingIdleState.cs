using UnityEngine;
public class PlayerHangingIdleState : PlayerHangingState
{
  public PlayerHangingIdleState(string name, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory) : base(name, playerStateMachine, playerStateFactory) { }

  int groundLayerMask = 1 << 6;

  Vector3 ledgeOffset = new Vector3(0, -1.81f, 0.43f);
  float ledgeYOffset = -1.81f;
  float ledgeXZOffset = 0.43f;

  public override void OnStart()
  {
    base.OnStart();

    stateMachine.animator.SetFloat("shimmySpeed", 0);

    stateMachine.xyVelocity = Vector2.zero;
    stateMachine.zVelocity = 0;

    HangSetupInfo setupInfo = GetHangSetupInfo();
    
    stateMachine.transform.rotation = setupInfo.hangRotation;
    stateMachine.transform.position = setupInfo.hangPosition;

    Debug.Log("Player position: " + stateMachine.transform.position);
    Debug.Log("Player Rotation: " + stateMachine.transform.eulerAngles);
  }

  public override void OnEnd()
  {
    base.OnEnd();
  }

  public override void OnUpdate()
  {
    stateMachine.animator.SetFloat("shimmySpeed", stateMachine.moveInput.x);
    CheckForStateTransition();
  }

  protected override void CheckForStateTransition()
  {
    if (stateMachine.moveInput.x*stateMachine.moveInput.x > 0)
    {
      stateMachine.TransitionToState(states.HangingMove());
      return;
    }

    base.CheckForStateTransition();
  }

  HangSetupInfo GetHangSetupInfo()
  {
    RaycastHit ledgeHitInfo;
    RaycastHit forwardHitInfo;

    HangSetupInfo hangSetupInfo;

    bool isLedge = Physics.Raycast(stateMachine.ledgeCheckPoint.position, Vector3.down, out ledgeHitInfo, 0.15f, groundLayerMask);
    Vector3 pointToCastFrom = new Vector3(stateMachine.transform.position.x, ledgeHitInfo.point.y - 0.1f, stateMachine.transform.position.z);

    bool isInRange = Physics.Raycast(pointToCastFrom, stateMachine.transform.forward, out forwardHitInfo, 2.0f, groundLayerMask);

    float positionToHangY = forwardHitInfo.point.y + ledgeYOffset;
    Vector2 positionToHangXZ = new Vector2(forwardHitInfo.point.x + ledgeXZOffset*forwardHitInfo.normal.x, forwardHitInfo.point.z + ledgeXZOffset*forwardHitInfo.normal.z); 

    Vector3 positionToHang = new Vector3(positionToHangXZ.x, positionToHangY, positionToHangXZ.y);

    Vector3 directionToFace = -forwardHitInfo.normal;
    float eulerAngleToFace = (Mathf.Atan2(directionToFace.x, directionToFace.z) * Mathf.Rad2Deg);
    Quaternion angleToFace = Quaternion.Euler(0, eulerAngleToFace, 0);

    hangSetupInfo.hangPosition = positionToHang;
    hangSetupInfo.hangRotation = angleToFace;

    Debug.Log("rotation to hang: " + eulerAngleToFace);

    return hangSetupInfo;
  }

}

struct HangSetupInfo
{
  public Vector3 hangPosition;
  public Quaternion hangRotation;
}

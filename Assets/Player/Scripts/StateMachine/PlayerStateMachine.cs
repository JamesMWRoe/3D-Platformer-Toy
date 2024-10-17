using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine
{ 

  PlayerActions actions;
  PlayerStateFactory states;

  public PlayerAnimationHandler animationEventHandler;
  public CharacterController controller;
  public Transform cameraTransform;
  public Transform ledgeCheckPoint;
  public Transform climbEndPoint;
  public Animator animator;

  float jumpHeight = 3;
  float jumpDistance = 4;
  
  public float speed = 7;
  public float gravity;
  public float jumpVelocity;

  public Vector2 moveInput;
  public Vector2 xyVelocity;
  public float zVelocity;

  public Vector2 moveDirection;

  public InputAction moveAction;
  public InputAction jumpAction;
  public InputAction crouchAction;

  protected override BaseState GetInitialState()
  {  return states.Idle(); }


  new void Awake()
  {
    jumpVelocity = 2 * jumpHeight * speed / jumpDistance;
    gravity = -2 * jumpHeight * (speed*speed) / (jumpDistance*jumpDistance);


    if (actions == null)
    {   actions = new PlayerActions();   }
    moveAction = actions.Movement.Move;
    jumpAction = actions.Movement.Jump;
    crouchAction = actions.Movement.Crouch;
    actions.Enable();

    states = new PlayerStateFactory(this);
    controller = GetComponent<CharacterController>();
    
    base.Awake();
  }

  new void Update()
  {
    base.Update();

    if (this.currentState.name == "hanging")
    {  print("Current Position: " + transform.position);}

    UpdateMoveDirection();
  }

  protected void UpdateMoveDirection()
  {
    moveInput = moveAction.ReadValue<Vector2>();

    Vector2 forwardDir = new Vector2 (cameraTransform.forward.x, cameraTransform.forward.z).normalized;
    Vector2 rightDir = new Vector2 (cameraTransform.right.x, cameraTransform.right.z).normalized;

    moveDirection = moveInput.x*rightDir + moveInput.y*forwardDir;
  }
  
  public void UpdateRotation()
  {
    if (moveInput.sqrMagnitude < Mathf.Epsilon) return;

    float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
    Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
  }

  public void Move()
  {
    Vector3 velocity = new Vector3(xyVelocity.x, zVelocity, xyVelocity.y);
    controller.Move(Time.deltaTime * velocity);
  }
}

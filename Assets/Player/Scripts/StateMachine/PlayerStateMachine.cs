
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine
{ 

  PlayerActions actions;
  PlayerStateFactory states;

  public CharacterController controller;
  public Transform cameraTransform;

  float jumpHeight = 3;
  float jumpDistance = 4;
  
  public float speed = 10;
  public float gravity;
  public float jumpVelocity;

  public Vector2 xyVelocity;
  public float zVelocity;

  public InputAction moveAction;
  public InputAction jumpAction;

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
    actions.Enable();

    states = new PlayerStateFactory(this);
    controller = GetComponent<CharacterController>();
    
    base.Awake();
  }

  new void Update()
  {
    base.Update();

    if (jumpAction.WasPressedThisFrame())
    {  
      print("jump pressed this frame");
      print ("z velocity: " + zVelocity);
    }

    if (jumpAction.WasReleasedThisFrame())
    {  print("jump press was released this frame");  }

    Vector3 velocity = new Vector3(xyVelocity.x, zVelocity, xyVelocity.y);
    controller.Move(Time.deltaTime * velocity);
  }
}

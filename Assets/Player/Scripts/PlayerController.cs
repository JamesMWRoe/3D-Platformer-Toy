using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerActions actions;
    CharacterController controller;
    [SerializeField] Transform cameraTransform;

    float gravity = -40f;
    float speed = 10;
    float jumpVelocity = 15;


    Vector2 xyVelocity;
    float zVelocity;

    InputAction moveAction;
    InputAction jumpAction;



    void Start()
    {
        if (actions == null)
        {   actions = new PlayerActions();   }

        controller = GetComponent<CharacterController>();

        moveAction = actions.Movement.Move;
        jumpAction = actions.Movement.Jump;

        actions.Enable();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            zVelocity = -1;
        }

        if (!controller.isGrounded)
        {
            zVelocity += gravity * Time.deltaTime;
        }

        if (jumpAction.WasPressedThisFrame() && controller.isGrounded)
        {
            print("jump occured");
            zVelocity += jumpVelocity;
        }


        Vector2 forwardDir = new Vector2 (cameraTransform.forward.x, cameraTransform.forward.z).normalized;
        Vector2 rightDir = new Vector2 (cameraTransform.right.x, cameraTransform.right.z).normalized;
        

        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector2 moveDirection = moveInput.x*rightDir + moveInput.y*forwardDir;

        xyVelocity = moveDirection * speed;

        Vector3 velocity = new Vector3(xyVelocity.x, zVelocity, xyVelocity.y);
        controller.Move(Time.deltaTime * velocity);
    }

}

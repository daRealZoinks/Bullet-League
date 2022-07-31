using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private float moveSpeedMultiplier;
    public float groundSpeedMultiplier;
    public float airSpeedMultiplier;
    public float groundDrag;
    public float airDrag;
    public LayerMask whatIsGround;
    private Vector2 direction;
    [Space]
    [HideInInspector]
    public Rigidbody rb;
    [Space]
    public float jumpForce;
    public CapsuleCollider capsuleCollider;
    private bool isGrounded;
    [Header("Looking around")]
    public float sensitivity;
    [HideInInspector]
    public Vector2 lookingDirection;
    public Camera cam;
    [Header("Wallrun")]
    public float wallDistance;
    [Space]
    public float wallRunGravityCancelForce;
    private bool wallLeft;
    private bool wallRight;
    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    #region Movement
    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            }
            else
            {
                if (wallLeft || wallRight)
                {
                    Vector3 wallRunJumpDirection = Vector3.zero;

                    if (wallLeft)
                    {
                        wallRunJumpDirection = transform.up + leftWallHit.normal;
                        wallLeft = false;
                    }

                    if (wallRight)
                    {
                        wallRunJumpDirection = transform.up + rightWallHit.normal;
                        wallRight = false;
                    }

                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    rb.AddForce(wallRunJumpDirection * jumpForce, ForceMode.VelocityChange);
                }
            }
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        WallRun();

        isGrounded = Physics.Raycast(transform.position + capsuleCollider.center, Vector3.down, capsuleCollider.height / 2 + 0.3f, whatIsGround);

        ControlDrag();
    }
    public void WallRun()
    {
        wallLeft = Physics.Raycast(transform.position + capsuleCollider.center, -transform.right, out leftWallHit, wallDistance, whatIsGround);
        wallRight = Physics.Raycast(transform.position + capsuleCollider.center, transform.right, out rightWallHit, wallDistance, whatIsGround);

        if (!isGrounded && (wallRight || wallLeft))
        {
            if (rb.velocity.y < 0)
            {
                rb.AddForce(Time.fixedDeltaTime * wallRunGravityCancelForce * transform.up, ForceMode.Force);
            }

            rb.AddForce(Time.deltaTime * groundSpeedMultiplier * transform.forward, ForceMode.Acceleration);

            if (wallRight)
            {
                rb.AddForce(2 * Time.deltaTime * groundSpeedMultiplier * -rightWallHit.normal, ForceMode.Acceleration);
            }

            if (wallLeft)
            {
                rb.AddForce(2 * Time.deltaTime * groundSpeedMultiplier * -leftWallHit.normal, ForceMode.Acceleration);
            }
        }

        if (isGrounded)
        {
            wallRight = false;
            wallLeft = false;
        }
    }
    private void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = Mathf.Lerp(rb.drag, groundDrag, Time.deltaTime * 10);
            moveSpeedMultiplier = Mathf.Lerp(moveSpeedMultiplier, groundSpeedMultiplier, 20);
        }
        else
        {
            rb.drag = airDrag;
            moveSpeedMultiplier = Mathf.Lerp(moveSpeedMultiplier, airSpeedMultiplier, 20);
        }
    }
    private void FixedUpdate()
    {
        if (!(wallRight || wallLeft))
        {
            Vector3 moveDirection = transform.forward * direction.y + transform.right * direction.x;

            rb.AddForce(moveSpeed * moveSpeedMultiplier * moveDirection, ForceMode.Acceleration);
        }
    }
    #endregion
    #region Looking around
    public void Looking(InputAction.CallbackContext context)
    {
        lookingDirection = context.ReadValue<Vector2>();
    }
    private void LateUpdate()
    {
        transform.Rotate(sensitivity * Time.deltaTime * new Vector3(0, lookingDirection.x, 0));
        cam.transform.Rotate(sensitivity * Time.deltaTime * new Vector3(-lookingDirection.y, 0, 0));

        if (!isGrounded)
        {
            if (wallRight)
            {
                cam.transform.Rotate(new Vector3(0, 0, 0.5f - 3 * cam.transform.localRotation.z));
            }
            if (wallLeft)
            {
                cam.transform.Rotate(new Vector3(0, 0, -0.5f - 3 * cam.transform.localRotation.z));
            }
        }
        if (!wallRight && !wallLeft)
        {
            cam.transform.Rotate(new Vector3(0, 0, -3 * cam.transform.localRotation.z));
        }

        float newFieldOfView;

        if (!isGrounded)
        {
            newFieldOfView = 90 + rb.velocity.magnitude;
        }
        else
        {
            newFieldOfView = 90;
        }

        newFieldOfView = Mathf.Clamp(newFieldOfView, 90, 150);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newFieldOfView, Time.deltaTime * 5);
    }
    #endregion
}
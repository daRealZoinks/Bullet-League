using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] public float moveSpeed = 6.0f;
    private float _moveSpeedMultiplier;
    public float groundSpeedMultiplier = 15.0f;
    public float airSpeedMultiplier = 0.4f;
    public float groundDrag = 6.0f;
    public float airDrag;
    public LayerMask whatIsGround;
    private Vector2 _direction;

    [Space][HideInInspector] public Rigidbody rb;

    [Space] public float jumpForce = 8.0f;
    public CapsuleCollider capsuleCollider;
    private bool _isGrounded;

    [Header("Looking around")] public float sensitivity = 6;

    [HideInInspector] public Vector2 lookingDirection;
    public Camera cam;

    [Header("Wall run")] public float wallDistance = .56f;

    [Space] public float wallRunGravityCancelForce = 300.0f;
    private bool _wallLeft;
    private bool _wallRight;
    private RaycastHit _leftWallHit;
    private RaycastHit _rightWallHit;

    #region Movement

    public void Move(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if (_isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
        else
        {
            if (!_wallLeft && !_wallRight) return;

            var wallRunJumpDirection = Vector3.zero;

            if (_wallLeft)
            {
                wallRunJumpDirection = transform.up + _leftWallHit.normal;
                _wallLeft = false;
            }

            if (_wallRight)
            {
                wallRunJumpDirection = transform.up + _rightWallHit.normal;
                _wallRight = false;
            }

            var velocity = rb.velocity;
            velocity = new Vector3(velocity.x, 0, velocity.z);
            rb.velocity = velocity;
            rb.AddForce(wallRunJumpDirection * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        _isGrounded = Physics.Raycast(transform.position + capsuleCollider.center, Vector3.down,
            capsuleCollider.height / 2 + 0.3f, whatIsGround);

        ControlDrag();
    }

    private void WallRun()
    {
        var playerTransform = transform;
        var position = playerTransform.position;
        var right = playerTransform.right;

        _wallLeft = Physics.Raycast(position + capsuleCollider.center, -right, out _leftWallHit, wallDistance,
            whatIsGround);
        _wallRight = Physics.Raycast(position + capsuleCollider.center, right, out _rightWallHit, wallDistance,
            whatIsGround);

        switch (_isGrounded)
        {
            case false when _wallRight || _wallLeft:
                {
                    if (rb.velocity.y < 0)
                    {
                        rb.AddForce(Time.fixedDeltaTime * wallRunGravityCancelForce * transform.up, ForceMode.Force);
                    }

                    rb.AddForce(Time.deltaTime * groundSpeedMultiplier * transform.forward, ForceMode.Acceleration);

                    if (_wallRight)
                    {
                        rb.AddForce(2 * Time.deltaTime * groundSpeedMultiplier * -_rightWallHit.normal,
                            ForceMode.Acceleration);
                    }

                    if (_wallLeft)
                    {
                        rb.AddForce(2 * Time.deltaTime * groundSpeedMultiplier * -_leftWallHit.normal,
                            ForceMode.Acceleration);
                    }

                    break;
                }
            case true:
                _wallRight = false;
                _wallLeft = false;
                break;
        }
    }

    private void ControlDrag()
    {
        if (_isGrounded)
        {
            rb.drag = Mathf.Lerp(rb.drag, groundDrag, Time.deltaTime * 10);
            _moveSpeedMultiplier = Mathf.Lerp(_moveSpeedMultiplier, groundSpeedMultiplier, 20);
        }
        else
        {
            rb.drag = airDrag;
            _moveSpeedMultiplier = Mathf.Lerp(_moveSpeedMultiplier, airSpeedMultiplier, 20);
        }
    }

    private void FixedUpdate()
    {

        WallRun();

        if (!_wallLeft && !_wallRight)
        {
            var playerTransform = transform;
            var moveDirection = playerTransform.forward * _direction.y + playerTransform.right * _direction.x;

            rb.AddForce(moveSpeed * _moveSpeedMultiplier * moveDirection, ForceMode.Acceleration);
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
        transform.Rotate(sensitivity * new Vector3(0, lookingDirection.x, 0));
        cam.transform.Rotate(sensitivity * new Vector3(-lookingDirection.y, 0, 0));

        if (!_isGrounded)
        {
            if (_wallRight)
            {
                cam.transform.Rotate(new Vector3(0, 0, 0.5f - 3 * cam.transform.localRotation.z));
            }

            if (_wallLeft)
            {
                cam.transform.Rotate(new Vector3(0, 0, -0.5f - 3 * cam.transform.localRotation.z));
            }
        }

        if (!_wallRight && !_wallLeft)
        {
            cam.transform.Rotate(new Vector3(0, 0, -3 * cam.transform.localRotation.z));
        }

        float newFieldOfView;

        if (!_isGrounded)
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
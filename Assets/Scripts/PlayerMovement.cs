using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Horizontal Speed")]
    [SerializeField] private float moveSpeed;
    [Tooltip("Rate of change for movespeed")]
    [SerializeField] private float acceleration;
    [Tooltip("Deceleration rate when no input is provided")]
    [SerializeField] private float deceleration;

    [Header("Controls")]
    [Tooltip("Use keys to move")]
    [SerializeField] private KeyCode forwardKey = KeyCode.W; // = KeyCode.W can be replaced in Inspector. Make sure code is attached to player.
    [SerializeField] private KeyCode backwardKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;

    [Header("Collectible")]
    [SerializeField] LayerMask Collectible;

    private Vector3 inputVector;
    private float currentSpeed;
    private CharacterController characterController;
    private float initialYPosition;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        initialYPosition = transform.position.y;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        Move(inputVector);
    }

    private void HandleInput()
    {
        float xInput = 0;
        float zInput = 0;

        if (Input.GetKey(forwardKey))
        {
            zInput++;
        }
        else if (Input.GetKey(backwardKey))
        {
            zInput--;
        }
        else if (Input.GetKey(leftKey))
        {
            xInput--;
        }
        else if (Input.GetKey(rightKey))
        {
            xInput++;
        }
        inputVector = new Vector3(xInput, 0, zInput);
    }

    private void Move(Vector3 inputVector)
    {
        if (inputVector == Vector3.zero)
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= deceleration * Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0);
            }
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, Time.deltaTime * acceleration);
        }

        Vector3 movement = inputVector.normalized * currentSpeed * Time.deltaTime;
        characterController.Move(movement);
        transform.position = new Vector3(transform.position.x, initialYPosition, transform.position.z);
    }
}
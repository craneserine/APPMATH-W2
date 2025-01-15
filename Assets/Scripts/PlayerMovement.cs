using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    [Header("Controls")]
    [SerializeField] private KeyCode forwardKey = KeyCode.W;
    [SerializeField] private KeyCode backwardKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;

    [Header("Rocket Firing")]
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private float rocketCooldown = 5f;
    private float rocketCooldownTimer;
    private int rocketsToFire = 4; // Initial rockets (max 8)

    [Header("Collectibles")]
    [SerializeField] private LayerMask collectibleLayer;

    private Vector3 inputVector;
    private float currentSpeed;
    private CharacterController characterController;
    private float initialYPosition;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        initialYPosition = transform.position.y;
    }

    void Start()
    {
        rocketCooldownTimer = rocketCooldown;
    }

    void Update()
    {
        HandleInput();
        Move(inputVector);

        // Handle rocket cooldown
        rocketCooldownTimer -= Time.deltaTime;
        if (rocketCooldownTimer <= 0f)
        {
            FireRockets();
            rocketCooldownTimer = rocketCooldown; // Reset cooldown
        }
    }

    private void HandleInput()
    {
        float xInput = 0;
        float zInput = 0;

        if (Input.GetKey(forwardKey)) zInput++;
        else if (Input.GetKey(backwardKey)) zInput--;
        if (Input.GetKey(leftKey)) xInput--;
        else if (Input.GetKey(rightKey)) xInput++;

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

    // Fire rockets in a circular pattern
    private void FireRockets()
    {
        float angleIncrement = 360f / rocketsToFire;
        for (int i = 0; i < rocketsToFire; i++)
        {
            float angle = angleIncrement * i;
            Vector3 direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad * angle));
            Instantiate(rocketPrefab, transform.position + Vector3.up * 0.8f, Quaternion.LookRotation(direction));
        }
    }

    // Collect power-ups to increase the number of rockets fired
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has the "Collectible" tag
        if (other.CompareTag("Collectible"))
        {
            // Increase the number of rockets if it's below the max
            if (rocketsToFire < 8)
            {
                rocketsToFire++;
                Debug.Log("Rockets increased to: " + rocketsToFire); // Log when rockets increase
            }
            else
            {
                // Log that the max rocket count is reached
                Debug.Log("Max rockets reached! Cannot collect more power-ups.");
            }
            
            Destroy(other.gameObject); // Destroy the collectible after use
        }
    }
}

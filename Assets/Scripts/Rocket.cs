using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector3 direction;

    void Start()
    {
        // Direction is determined by the rotation from PlayerMovement
        direction = transform.forward;
    }

    void Update()
    {
        // Move the rocket forward
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}

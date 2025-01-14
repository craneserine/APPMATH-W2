using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float cooldown = 5;
    private float cooldownTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireRocket();
    }

    void FireRocket()
    {
        Vector3 rocketOffset = new Vector3(transform.position.x, transform.position.y + 0.8f, 0);
        Instantiate(_rocketPrefab, rocketOffset, Quaternion.identity);

    }
}

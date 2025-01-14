using UnityEngine;

public class Orbit : MonoBehaviour
{
    float xAngleValue;
    float yAngleValue;

    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    [SerializeField] private float xorbitRadius = 4;
    [SerializeField] private float yorbitRadius = 4;


    // Update is called once per frame
    void Update()
    {
        xAngleValue += Time.deltaTime * xSpeed;
        yAngleValue += Time.deltaTime * ySpeed;
        this.transform.position = new Vector3 (Mathf.Cos(xAngleValue) * xorbitRadius, this.transform.position.y, Mathf.Sin (xAngleValue) * xorbitRadius);
    }
}

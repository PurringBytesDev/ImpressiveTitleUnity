using UnityEngine;

public class SlowRotation : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 10f, 0f); // degrees per second

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
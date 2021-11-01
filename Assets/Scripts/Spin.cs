using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float rotationPerSec;

    const float degreesPerRotation = 360;
    
    void Update()
    {
        transform.Rotate(Vector3.up * degreesPerRotation * rotationPerSec * Time.deltaTime);
    }
}

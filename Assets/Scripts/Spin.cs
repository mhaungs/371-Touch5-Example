using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float _rotationPerSec;

    const float _degreesPerRotation = 360;
    
    void Update()
    {
        transform.Rotate(Vector3.up * _degreesPerRotation * _rotationPerSec * Time.deltaTime);
    }
}

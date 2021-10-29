using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 2f;
    
    Vector3 _ySpin = new Vector3(0, 1, 0);

    void Start()
    {
        _ySpin *= speed;
    }

    void Update()
    {
        transform.Rotate(_ySpin);
    }
}

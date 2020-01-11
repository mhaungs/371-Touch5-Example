using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 ySpin = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        ySpin *= speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(ySpin);
    }
}

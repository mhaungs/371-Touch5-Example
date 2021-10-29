using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    Vector3 _translation;
    Rigidbody rgbd;

    void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        _translation = new Vector3(horizontalInput, 0, verticalInput);
        _translation = _translation.normalized;  // so moving diagonally is not faster
    }

    void FixedUpdate()
    {
        rgbd.MovePosition(transform.position + _translation  * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}

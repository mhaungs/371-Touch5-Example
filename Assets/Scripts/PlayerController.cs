using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    // Components
    Rigidbody _rgbd;
    
    // Input
    InputAction _moveAction;
    Vector2 _moveInput;

    Vector3 _translation;

    void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Movement");
        _rgbd = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        _translation = new Vector3(_moveInput.x, 0, _moveInput.y);
    }

    void FixedUpdate()
    {
        _rgbd.MovePosition(transform.position + Time.deltaTime * speed * _translation);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}

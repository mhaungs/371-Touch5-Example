using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    // Input
    PlayerInput _playerInput;
    InputAction _moveAction;

    Vector3 _translation;
    Rigidbody rgbd;

    void Awake()
    {
        PlayerInput _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Movement"];

        rgbd = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 moveInput = _moveAction.ReadValue<Vector2>();
        _translation = new Vector3(moveInput.x, 0, moveInput.y);
    }

    void FixedUpdate()
    {
        rgbd.MovePosition(transform.position + _translation * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}

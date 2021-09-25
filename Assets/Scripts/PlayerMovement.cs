using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    // General attributes
    private Transform _transform;
    private Rigidbody _rigidbody;
    private Camera _mainCamera;

    // Movement
    private Vector3 _movementInput;

    [Header("Speed")]
    [Range(0, 20)] [SerializeField] private float horizontalSpeed = 10.0f;
    [Range(0, 20)] [SerializeField] private float verticalSpeed = 10.0f;

    private Vector3 _up, _right;
    
    // Constants
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();

        _movementInput = new Vector3(0, 0, 0);

        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            print("Main camera not set (ensure that camera has MainCamera tag).");
        }
        else
        {
            _up = _mainCamera.transform.forward;
            _up.y = 0;
            _up = Vector3.Normalize(_up);
            _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _up;
        }
    }

    private void Update()
    {
        _movementInput.x = Input.GetAxisRaw("Horizontal");
        _movementInput.z = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        var movementHorizontal = _right * horizontalSpeed * Time.deltaTime * _movementInput.x;
        var movementVertical = _up * verticalSpeed * Time.deltaTime * _movementInput.z;

        var movement = movementHorizontal + movementVertical;
        if (movement.Equals(Vector3.zero))
        {
            return;
        }
        
        _rigidbody.MovePosition(_transform.position + movement);
        _transform.forward = Vector3.Normalize(movement);
    }
}

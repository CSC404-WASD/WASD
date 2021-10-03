using System;
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
    private bool _jumpInput = false;
    private bool _onGround = false;
    

    [Header("Movement")]
    [Range(0, 1)] [SerializeField] private float horizontalRelativeSpeed = 1.0f;
    [Range(0, 1)] [SerializeField] private float verticalRelativeSpeed = 1.0f;
    [Range(0, 100)] [SerializeField] private float moveSpeed = 0.5f;
    [Range(0, 100)] [SerializeField] private float maxGroundDistanceForJump = 5f;
    [Range(0, 100)] [SerializeField] private float jumpHeight = 0.5f;

    private Vector3 _up, _right;

    private PlayerStats _stats;
    
    // Constants
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();

        _movementInput = new Vector3(0, 0, 0);

        _mainCamera = Camera.main;
        _stats = PlayerStats.instance;
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
        _jumpInput = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        // If stunned, make movement 0.
        if (_stats.isStunned())
        {
            var movement = Vector3(0,0,0);
        }
        else
        {
            // Calculate x and z movement from input
            var movementHorizontal = _right * horizontalRelativeSpeed * Time.deltaTime * _movementInput.x;
            var movementVertical = _up * verticalRelativeSpeed * Time.deltaTime * _movementInput.z;
            var movement = Vector3.Normalize(movementHorizontal + movementVertical) * moveSpeed;
        }

        // Rotate character in the right direction. Make sure to do this before adding y movement.
        if (!movement.Equals(Vector3.zero))
        {
            _transform.forward = movement;
        }

        // Factor in y movement from jump and physics engine gravity.
        movement.y = _rigidbody.velocity.y;

        // Set rigidbody velocity
        _rigidbody.velocity = movement;

        // Jump code
        RaycastHit groundRaycastHit;
        _onGround = Physics.Raycast(_rigidbody.position, Vector3.down, out groundRaycastHit, maxGroundDistanceForJump);
        Debug.DrawRay(_rigidbody.position, Vector3.down * maxGroundDistanceForJump, Color.green);
        if (_onGround && _jumpInput && !_stats.isStunned())
        {
            _rigidbody.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
        }
    }
}

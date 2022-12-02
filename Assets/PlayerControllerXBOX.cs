using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerXBOX : MonoBehaviour
{
    public float MoveSpeed = 1.0f;

    private Rigidbody _rigidbody;
    private Vector3 _moveDir = Vector3.zero;
    private Camera _mainCamera;

    private bool _isWalking;

    [SerializeField]
    private float _turnSpeed = 10.0f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;

        _isWalking = false;
    }

    void Update()
    {
        _moveDir = Vector3.zero;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _moveDir = new Vector3(horizontal, 0, vertical);

        if(_moveDir != Vector3.zero)
        {
            // Are we already walking?
            if(!_isWalking)
            {
                // If not, trigger animation change and face same direction as camera.
                _isWalking = true;
                transform.forward = _mainCamera.transform.forward;
            }
        }
        // If _movDir is zero and we are walking, we need to stop
        else if(_isWalking)
        {
            _isWalking = false;
        }

        // Transform our movement direction into the camera's local coordinates.
        _moveDir = _mainCamera.transform.TransformDirection(_moveDir);
    }

    private void FixedUpdate()
    {
        if(_moveDir.magnitude > 0)
        {
            // Rotate to face direction of movement
            Quaternion newDir = Quaternion.LookRotation(_moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                newDir, Time.deltaTime * _turnSpeed);

            _rigidbody.MovePosition(_rigidbody.position + _moveDir * MoveSpeed * Time.fixedDeltaTime);
        }
    }
}
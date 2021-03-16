using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private bool isGrounded;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            isGrounded = false;
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Road road))
        {
            isGrounded = true;
        }
    }

    public void ChangeJumpForce(float value)
    {
        _jumpForce *= value;
    }
}

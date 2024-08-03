using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _speedPlayer = 5f;
    [SerializeField] private DynamicJoystick _joystick;
    private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    private bool _canMove = true;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void CanMove(bool canMove)
    {
        _canMove = canMove;
    }

    private void Update()
    {
        if(_canMove)
        {
            _rigidbody.velocity = new Vector3(_joystick.Horizontal * _speedPlayer, _rigidbody.velocity.y, _joystick.Vertical * _speedPlayer);
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                _animator.SetBool("IsRun", true);
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            }
            else
            {
                _animator.SetBool("IsRun", false);
            }
        }  
    }

}


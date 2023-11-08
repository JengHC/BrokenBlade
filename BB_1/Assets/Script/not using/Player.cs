using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private Vector3 moveDirection;
    private float moveSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool hasControl = (moveDirection != Vector3.zero);
        if(hasControl)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
    }

    #region SEND_MESSAGE
    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        if(input != null)
        {
            moveDirection = new Vector3(input.x, 0f, input.y);
            _animator.SetFloat("moveSpeed", input.magnitude);
            Debug.Log($"UNITY_EVENTS : {input.magnitude}");
        }
    }

    void OnJump()
    {

    }
    #endregion

    #region SEND_MESSAGE
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input != null)
        {
            moveDirection = new Vector3(input.x, 0f, input.y);
            _animator.SetFloat("moveSpeed", input.magnitude);
            Debug.Log($"UNITY_EVENTS : {input.magnitude}");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("Unity Event Jump");
        }
    }
    #endregion
}

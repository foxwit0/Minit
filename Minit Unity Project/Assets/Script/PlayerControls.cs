using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[SelectionBase]

public class PlayerControls : MonoBehaviour
{
    //Controls
    ControlsMaster controls;
    Vector2 move;
    //Components
    Rigidbody2D rb;
    Animator animator;
    
    //Parameters
    public float moveSpeed = 5f;
    //private Vector3 velocity =  Vector3.zero;

    void Awake ()
    {
        //Controls
        controls = new ControlsMaster();
        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;

        //Components init
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update ()
    {
        //Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        //transform.Translate(m, Space.World);
        
        //Animator
        animator.SetFloat("Horizontal", move.x);
        animator.SetFloat("Vertical", move.y);
        animator.SetFloat("Speed", move.sqrMagnitude);

        if (move.sqrMagnitude > 0.01f)
        {
            animator.SetFloat("LastHorizontal", move.x);
            animator.SetFloat("LastVertical", move.y);
        }
    }

    void FixedUpdate()
    {
        //Vector3 targetVelocity = new Vector2(move.x, move.y) * moveSpeed * Time.fixedDeltaTime;
        //rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        //Movement
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);

    }

    void Jump ()
    {
        Debug.Log("Jump !!");
    }

    //Controls Enable/Disable
    private void OnEnable ()
    {
        controls.Player.Enable();
    }
    private void OnDisable ()
    {
        controls.Player.Disable();
    }
}

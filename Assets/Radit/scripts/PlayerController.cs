using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator anim;
    public float moveSpeed;

    private Vector2 input;
    private bool moving;

    private Rigidbody2D rb;

    private float x;
    private float y;

     private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (anim == null) // If Animator is missing, show a warning
        {
        Debug.LogWarning("Animator component is missing on " + gameObject.name);
        }
         else
        {
        Debug.Log("Animator found on " + gameObject.name);
        }
    }

    private void Update()
    {
        GetInput();
        Animate();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(x * moveSpeed, y * moveSpeed);
    }

    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        input = new Vector2(x, y);
        input.Normalize(); // Normalize to ensure magnitude is 1 when moving

    }  

    private void Animate()
    {
        moving = input.magnitude > 0.1f; // Check if player is moving

        anim.SetBool("moving", moving); // Use the correct parameter name

        if (moving) 
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
        }
        else
        {
            // Reset X and Y to 0 when idle
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", 0);
        }
    }    
    
}

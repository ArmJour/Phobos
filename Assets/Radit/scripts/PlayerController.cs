using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator anim;
    public float moveSpeed;
    public GameObject GFX; 
    private Vector2 input;
    public bool moving;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float x;
    private float y;

     private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
         if (GFX != null)
            spriteRenderer = GFX.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetInput();
        RaditAnimate();
        
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(x * moveSpeed, y * moveSpeed);
    }

    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        input = new Vector2(x, y);
        input.Normalize(); // Normalize to ensure magnitude is 1 when moving

    }  

    private void RaditAnimate()
    {
        moving = input.magnitude > 0.1f; // Check if player is moving

        // Debug.Log($"Moving: {moving}, X: {x}, Y: {y}");

        anim.SetBool("moving", moving); // Use the correct parameter name

        if (moving) 
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);

            // 🔥 Flip sprite based on movement direction 🔥
            if (x > 0) 
                spriteRenderer.flipX = true;  // Face right ✅
            else if (x < 0) 
                spriteRenderer.flipX = false;
        }
        else
        {
            // Reset X and Y to 0 when idle
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", 0);
        }    
    }
}

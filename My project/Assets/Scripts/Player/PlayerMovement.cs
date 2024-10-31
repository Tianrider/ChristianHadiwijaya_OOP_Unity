using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed = new Vector2(5f, 5f); // Default value
    [SerializeField] Vector2 timeToFullSpeed = new Vector2(0.5f, 0.5f); // Default value
    [SerializeField] Vector2 timeToStop = new Vector2(0.3f, 0.3f); // Default value
    [SerializeField] Vector2 stopClamp = new Vector2(0.1f, 0.1f); // Default value

    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;

    // Start is called before the first frame update
void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component missing from player!");
            return;
        }

        // Configure Rigidbody2D
        rb.gravityScale = 0f; // Disable gravity for top-down movement
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Prevent rotation

        // Initial calculations for movement variables
        moveVelocity = maxSpeed / timeToFullSpeed;
        moveFriction = maxSpeed / timeToStop;
        stopFriction = stopClamp / timeToStop;

        Debug.Log($"Movement initialized with maxSpeed: {maxSpeed}, moveVelocity: {moveVelocity}");
    }

    public void move()
    {
        // Getting player input
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        Debug.Log($"Input: {moveDirection}, Current Velocity: {rb.velocity}");

        if (moveDirection != Vector2.zero)
        {
            // Normalize the movement direction for consistent speed in all directions
            moveDirection.Normalize();

            // Calculate target velocity
            Vector2 targetVelocity = new Vector2(
                moveDirection.x * maxSpeed.x,
                moveDirection.y * maxSpeed.y
            );

            // Smoothly move towards target velocity
            rb.velocity = Vector2.MoveTowards(
                rb.velocity,
                targetVelocity,
                moveVelocity.magnitude * Time.fixedDeltaTime
            );
        }
        else
        {
            // Stop more quickly when no input
            rb.velocity = Vector2.MoveTowards(
                rb.velocity,
                Vector2.zero,
                stopFriction.magnitude * Time.fixedDeltaTime
            );
        }
    }

    public bool isMoving()
    {
        return rb.velocity.sqrMagnitude > 0.01f;
    }    
    
    public Vector2 GetFriction() 
    {
        // Choose appropriate friction based on movement
        return (moveDirection != Vector2.zero) ? moveFriction : stopFriction;
    }

    public void MoveBound()
    {
        // Leave empty for now as instructed
    }
}

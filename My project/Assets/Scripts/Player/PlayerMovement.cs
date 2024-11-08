using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject cameraBoundsObject;
    private BoxCollider2D cameraBoundsCollider;
    [SerializeField] Vector2 maxSpeed = new Vector2(5f, 5f);
    [SerializeField] Vector2 timeToFullSpeed = new Vector2(0.5f, 0.5f);
    [SerializeField] Vector2 timeToStop = new Vector2(0.3f, 0.3f);
    [SerializeField] Vector2 stopClamp = new Vector2(0.1f, 0.1f);
    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;

    [SerializeField] GameObject portal;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component missing from player!");
            return;
        }

        cameraBoundsCollider = cameraBoundsObject.GetComponent<BoxCollider2D>();
        if (cameraBoundsCollider == null)
        {
            Debug.LogError("CameraBounds object does not have a BoxCollider2D component!");
            return;
        }

        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / timeToStop * timeToStop;
        stopFriction = -2 * maxSpeed / timeToStop * timeToStop;

        Debug.Log($"Movement initialized with maxSpeed: {maxSpeed}, moveVelocity: {moveVelocity}");

        portal.SetActive(false);
    }

    public void move()
    {
        // Clamp input values between -1 and 1
        float horizontalInput = Mathf.Clamp(Input.GetAxisRaw("Horizontal"), -1f, 1f);
        float verticalInput = Mathf.Clamp(Input.GetAxisRaw("Vertical"), -1f, 1f);
        moveDirection = new Vector2(horizontalInput, verticalInput);

        // Debug.Log($"Input: {moveDirection}, Current Velocity: {rb.velocity}");

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            moveDirection.Normalize();

            Vector2 targetVelocity = new Vector2(
                moveDirection.x * maxSpeed.x,
                moveDirection.y * maxSpeed.y
            );

            rb.velocity = Vector2.MoveTowards(
                rb.velocity,
                targetVelocity,
                GetFriction().magnitude * Time.fixedDeltaTime
            );
        }
        else
        {
            rb.velocity = Vector2.MoveTowards(
                rb.velocity,
                Vector2.zero,
                GetFriction().magnitude * Time.fixedDeltaTime
            );

            if (rb.velocity.magnitude < stopClamp.magnitude)
            {
                rb.velocity = Vector2.zero;
            }
        }

        MoveBound();
    }

    public bool isMoving()
    {
        return rb.velocity.sqrMagnitude > 0.01f;
    }

    public Vector2 GetFriction()
    {
        return (moveDirection != Vector2.zero) ? moveFriction : stopFriction;
    }

    public void MoveBound()
    {
        // applied small offset
        float minX = cameraBoundsCollider.bounds.min.x + (float)0.6;
        float maxX = cameraBoundsCollider.bounds.max.x - (float)0.6;
        float minY = cameraBoundsCollider.bounds.min.y;
        float maxY = cameraBoundsCollider.bounds.max.y - (float)1.5;

        // Clamp the player's position within the camera bounds
        rb.position = new Vector2(
            Mathf.Clamp(rb.position.x, minX, maxX),
            Mathf.Clamp(rb.position.y, minY, maxY)
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WeaponPickup"))
        {
            Debug.Log("Player entered weapon pickup");
            portal.SetActive(true);
        }
    }
}
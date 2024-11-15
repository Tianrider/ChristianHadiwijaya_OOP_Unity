using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;
    private Camera mainCamera;

    public IObjectPool<Bullet> objectPool;

    void Awake()
    {
        Debug.Log("Bullet awake");
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    // This will be called every time the object is activated
    void OnEnable()
    {
        // Reset velocity and apply new speed
        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed;
        }
    }

    void Update()
    {
        // Check if bullet is outside the screen bounds
        if (IsOffScreen())
        {
            ReturnToPool();
        }
    }

    private bool IsOffScreen()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        return screenPoint.x < 0 || screenPoint.x > 1 ||
               screenPoint.y < 0 || screenPoint.y > 1;
    }

    private void ReturnToPool()
    {
        if (objectPool != null)
        {
            objectPool.Release(this);
        }
    }
}
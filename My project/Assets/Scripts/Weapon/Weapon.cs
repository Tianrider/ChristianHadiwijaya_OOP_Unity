using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    public float nextShootTime = 0f;
    public Transform parentTransform;

    void Start()
    {
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseOnPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
        Debug.Log("Weapon started");
        Debug.Log("objectPool: " + objectPool);
    }
    private void FixedUpdate()
    {
        if (Time.time >= nextShootTime)
        {
            Bullet bulletObject = objectPool.Get();

            if (bulletObject != null)
            {
                // Just set position and rotation
                bulletObject.transform.position = bulletSpawnPoint.position;
                // bulletObject.transform.rotation = bulletSpawnPoint.rotation;

                // Update the next shoot time
                nextShootTime = Time.time + shootIntervalInSeconds;
            }
        }
    }

    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletInstance.objectPool = objectPool;
        return bulletInstance;
    }

    private void OnGetFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        // The OnEnable method will automatically handle velocity initialization
    }

    private void OnReleaseOnPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void Shoot()
    {
        Debug.Log("Shoot");
        if (Time.time >= nextShootTime)
        {
            Debug.Log("Shoot1");
            if (objectPool == null)
            {
                Debug.Log("Object Pool is null");
            }

            Bullet bulletObject = objectPool.Get();

            if (bulletObject != null)
            {
                // Set bullet position and rotation
                bulletObject.transform.position = bulletSpawnPoint.position;
                bulletObject.transform.rotation = bulletSpawnPoint.rotation;

                // Update the next shoot time
                nextShootTime = Time.time + shootIntervalInSeconds;
            }
            else
            {
                // Handle the case where the object pool is empty
                Debug.LogWarning("Failed to get a bullet from the object pool.");
            }
        }
    }
}
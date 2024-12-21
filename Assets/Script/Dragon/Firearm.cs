using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firearm : Item
{
    public GameObject bulletPrefab; // Bullet prefab
    public Transform firePoint; // Where bullets spawn
    public float fireRate = 0.5f; // Fire rate in seconds
    public float bulletSpeed = 20f;

    private float nextFireTime = 0f;

    public override GameObject Use()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }

        return null; // Guns don't drop items like food
    }

    private void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogWarning("No firePoint assigned for the gun.");
            return;
        }

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }
    }
}
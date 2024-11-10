using UnityEngine;

public class Gun : Equipment
{
    [SerializeField] private float fireRate;
    private float lastFireTime;

    public override void Use()
    {
        if (Time.time - lastFireTime >= fireRate)
        {
            Shoot();
            lastFireTime = Time.time;
        }
    }

    private void Shoot()
    {
        Debug.Log($"{equipmentName} fired.");
        // Implement shooting logic, like spawning bullets or raycasting here
    }
}
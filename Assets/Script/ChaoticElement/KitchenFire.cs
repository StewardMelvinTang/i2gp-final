using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenFire : MonoBehaviour
{
    [SerializeField] private GameObject firePrefab; // The fire particle prefab
    [SerializeField] private Transform spawnPoint; // The position where the fire will spawn
    [SerializeField] private float spawnInterval = 30f; // Time interval between spawn checks
    [SerializeField] private float fireChance = 0.2f; // 20% chance to spawn fire

    private Coroutine fireSpawner;

    public bool canFireBeShutoff = false;

    // Start is called before the first frame update
    void Start()
    {
        // Start the fire spawning coroutine
        fireSpawner = StartCoroutine(SpawnFireRoutine());
    }

    // Coroutine to handle fire spawning
    private IEnumerator SpawnFireRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Roll for a chance to spawn the fire
            if (Random.value <= fireChance) // Random.value generates a number between 0 and 1
            {
                SpawnFire();
            }
        }
    }
    
    private void SpawnFire()
    {
        if (firePrefab != null && spawnPoint != null)
        {
            Instantiate(firePrefab, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Fire prefab or spawn point not assigned!");
        }
    }
}
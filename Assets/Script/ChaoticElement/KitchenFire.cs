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
    private GameObject activeFire;

    public bool isOnFire = false;

    private PanTable panTableRef;

    // Start is called before the first frame update
    void Start()
    {
        // Start the fire spawning coroutine
        fireSpawner = StartCoroutine(SpawnFireRoutine());
        panTableRef = GetComponentInParent<PanTable>();

        if (panTableRef) {
            panTableRef.OnItemPlaced += HandleItemPlaced;
        }
    }

    private void HandleItemPlaced(GameObject item) {
        // Debug.Log("Item Placed: " + item.name);
        Item itemRef;
        if (item.TryGetComponent<Item>(out itemRef) && isOnFire) {
            if (itemRef.itemName == "Fire Estinguisher") {
                Debug.Log("Shutting Down Fire");
                isOnFire = false;
                DestroyImmediate(activeFire);
            }
        }
    }

    // Coroutine to handle fire spawning
    private IEnumerator SpawnFireRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Roll for a chance to spawn the fire
            if (Random.value <= fireChance && !isOnFire && panTableRef.isCooking) // Random.value generates a number between 0 and 1
            {
                SpawnFire();
            }
        }
    }
    
    private void SpawnFire()
    {
        if (firePrefab != null && spawnPoint != null) {
            activeFire =  Instantiate(firePrefab, spawnPoint.position, Quaternion.identity);
            isOnFire = true;
            
            Debug.Log("GET A FIRE ESTINGUISHER TO SHUTDOWN THE FIRE");
        }
        else
        {
            Debug.LogWarning("Fire prefab or spawn point not assigned!");
        }
    }
}
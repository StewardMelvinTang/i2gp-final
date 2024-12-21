using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float eventTime = 360f;
    public GameObject dragonUI; 
    public Transform spawnLocation;
    public GameObject dragonPrefab;

    private bool eventTriggered = false;
    void Start()
    {
        dragonUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!eventTriggered)
        {
            eventTime -= Time.deltaTime;

            if (eventTime <= 0)
            {
                TriggerDragonEvent();
            }
        }
    }

    void TriggerDragonEvent()
    {
        eventTriggered = true;
        dragonUI.SetActive(true); // Show the "Dragon Detected!" UI
        StartCoroutine(WaitAndSpawnDragon());
    }

    System.Collections.IEnumerator WaitAndSpawnDragon()
    {
        yield return new WaitForSeconds(3); // Wait a few seconds for the player to notice
        dragonUI.SetActive(false); // Hide UI
        Instantiate(dragonPrefab, spawnLocation.position, spawnLocation.rotation); // Spawn the dragon
    }
}

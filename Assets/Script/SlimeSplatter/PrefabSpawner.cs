using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab; // The prefab to spawn
    public BoxCollider[] boundingBoxes; // Array of BoxColliders to define the spawning areas
    public float spawnInterval = 15f; // Interval between spawns

    private void Start()
    {
        // Start the spawning process
        InvokeRepeating("SpawnPrefab", 0f, spawnInterval);
    }

    void SpawnPrefab()
    {
        // Choose a random bounding box (BoxCollider) from the list
        BoxCollider randomBox = null;
        if (boundingBoxes.Length == 1)
        {
            randomBox = boundingBoxes[0];
        }
        else
        {
            randomBox = boundingBoxes[Random.Range(0, boundingBoxes.Length)];
        }

        // Get the center and world size of the selected BoxCollider
        Vector3 boxCenter = randomBox.transform.position + randomBox.center;
        Vector3 boxSize = Vector3.Scale(randomBox.size, randomBox.transform.lossyScale);

        // Calculate random position within the bounding box's area
        Vector3 spawnPosition = new Vector3(
            Random.Range(boxCenter.x - boxSize.x / 2f, boxCenter.x + boxSize.x / 2f), // Random x within box bounds
            randomBox.transform.position.y, // y axis based on the BoxCollider position
            Random.Range(boxCenter.z - boxSize.z / 2f, boxCenter.z + boxSize.z / 2f) // Random z within box bounds
        );

        // Instantiate the prefab at the calculated position
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
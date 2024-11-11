using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint; // Single spawn point for all customers
    public Transform[] targetPositions; // Array of target positions at the counter
    private List<GameObject> activeCustomers = new List<GameObject>();
    private bool[] positionOccupied; // Track occupied target positions

    void Start()
    {
        positionOccupied = new bool[targetPositions.Length]; // Initialize the occupied tracker
        StartCoroutine(SpawnCustomers());
    }

    private IEnumerator SpawnCustomers()
    {
        while (true)
        {
            if (activeCustomers.Count < 5) // Maximum of 5 customers at a time
            {
                SpawnCustomer();
            }
            // Wait for a random interval between 2 and 10 seconds before spawning the next customer
            float randomInterval = Random.Range(2f, 10f);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    private void SpawnCustomer()
    {
        int freePositionIndex = GetFreePositionIndex();

        if (freePositionIndex != -1) // Ensure there’s a free position
        {
            // Instantiate customer at spawn point
            GameObject customer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
            activeCustomers.Add(customer);

            // Move customer to the free target position
            Vector3 targetPosition = targetPositions[freePositionIndex].position;
            MoveCustomerToCounter(customer, targetPosition);

            // Mark this position as occupied
            positionOccupied[freePositionIndex] = true;

            // Track the target position within the customer for cleanup later
            customer.GetComponent<Customer>().AssignedPositionIndex = freePositionIndex;
        }
    }

    private int GetFreePositionIndex()
    {
        for (int i = 0; i < positionOccupied.Length; i++)
        {
            if (!positionOccupied[i]) // Check for unoccupied position
            {
                return i;
            }
        }
        return -1; // Return -1 if no position is free
    }

    private void MoveCustomerToCounter(GameObject customer, Vector3 targetPosition)
    {
        customer.GetComponent<Customer>().MoveTo(new Vector3(targetPosition.x, customer.transform.position.y, targetPosition.z));
    }

    public void RemoveCustomer(GameObject customer)
    {
        int positionIndex = customer.GetComponent<Customer>().AssignedPositionIndex;
        if (positionIndex >= 0 && positionIndex < positionOccupied.Length)
        {
            positionOccupied[positionIndex] = false; // Mark the position as free
        }
        activeCustomers.Remove(customer);
        Destroy(customer);
    }
}
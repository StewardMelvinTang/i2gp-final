using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint; // Single spawn point for all customers
    public Transform[] targetPositions; // Array of target positions at the counter
    private List<GameObject> activeCustomers = new List<GameObject>();
    private List<Recipe> activeRecipes = new List<Recipe>();

    private bool[] positionOccupied; // Track occupied target positions

    private float customerDestroyDelay = 5f;

    // ugly code
    public OrderUiManager orderUiManager; 

    private GameManager gameManager;
    
    private float initialTime = 45f; // Starting leave time
    private float minTime = 15f;     // Minimum leave time
    private float decayRate = 0.01f; // Adjust this to control how fast the time decreases


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        positionOccupied = new bool[targetPositions.Length]; // Initialize the occupied tracker
    }

    public void startSpawningCustomers(RecipeManager recipeManager) {
        StartCoroutine(SpawnCustomers(recipeManager));
    }

    private IEnumerator SpawnCustomers(RecipeManager recipeManager)
    {
        while (true)
        {
            if (activeCustomers.Count < 6) // Maximum of 5 customers at a time
            {
                Recipe randomRecipe = recipeManager.GetRandomRecipe();  
                
                SpawnCustomer(randomRecipe);
                
            }
            // Wait for a random interval between 2 and 10 seconds before spawning the next customer
            float timeTaken = gameManager.GetRealTime();
            // ugly code
            
            float randomInterval = Random.Range(8f, 17f);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    private void SpawnCustomer(Recipe randomRecipe)
    {
        int freePositionIndex = GetFreePositionIndex();

        if (freePositionIndex != -1) // Ensure there’s a free position
        {
            // Instantiate customer at spawn point
            GameObject customer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
            float timeTaken = gameManager.GetRealTime();
            
            // Exponential decay to shrink the leave time
            float leaveTime = Mathf.Lerp(minTime, initialTime, Mathf.Exp(-decayRate * timeTaken));

            customer.GetComponent<Customer>().SetPatienceTime(Mathf.Max(leaveTime, minTime)); // Ensure it doesn't go below the minimum


            activeCustomers.Add(customer);
            activeRecipes.Add(randomRecipe);

            // Move customer to the free target position
            Vector3 targetPosition = targetPositions[freePositionIndex].position;

            MoveCustomerToCounter(customer, targetPosition, randomRecipe);
        
            // Mark this position as occupied
            positionOccupied[freePositionIndex] = true;

            // Track the target position within the customer for cleanup later
            customer.GetComponent<Customer>().AssignedPositionIndex = freePositionIndex;
            customer.GetComponent<Customer>().exitPoint = spawnPoint;
            // customer.GetComponent<Customer>().customerManager = this;
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

    private void MoveCustomerToCounter(GameObject customer, Vector3 targetPosition, Recipe randomRecipe)
    {
        customer.GetComponent<Customer>().OrderFood(new Vector3(targetPosition.x, customer.transform.position.y, targetPosition.z), randomRecipe, OnCustomerTimerEnd);
    }

    // callback function
    void OnCustomerTimerEnd(bool hasLeft) {
        StartCoroutine(RemoveCustomerAfterDelay(customerDestroyDelay, 0));
        orderUiManager.RemoveOrderFromListByIndex(0);
    }

    // public void FulfilOrderByIndex(int idx) {
    //     // breaks for some reason 
    //     activeCustomers[idx].GetComponent<Customer>().FulfillOrder();
    //     Debug.Log(idx);
    //     StartCoroutine(RemoveCustomerAfterDelay(customerDestroyDelay, idx));
    //     orderUiManager.RemoveOrderFromListByIndex(idx);
    // }

    IEnumerator RemoveCustomerAfterDelay(float delay, int idx) {
        yield return new WaitForSeconds(delay);

        RemoveCustomerFromListByIndex(idx);
    }

    public void RemoveCustomerFromListByIndex(int idx) {
        GameObject customer = activeCustomers[idx];
        int positionIndex = customer.GetComponent<Customer>().AssignedPositionIndex;
        if (positionIndex >= 0 && positionIndex < positionOccupied.Length)
        {
            positionOccupied[positionIndex] = false; // Mark the position as free
        }
        // remove recipes
        activeRecipes.RemoveAt(idx);
        // remove customers
        activeCustomers.RemoveAt(idx);
        Destroy(customer);
    }

    public List<GameObject> GetCustomerList() {
        return activeCustomers;
    } 

    public List<Recipe> GetRecipesList() {
        return activeRecipes;
    }

    // public void RemoveCustomer(GameObject customer)
    // {
    //     int positionIndex = customer.GetComponent<Customer>().AssignedPositionIndex;
    //     if (positionIndex >= 0 && positionIndex < positionOccupied.Length)
    //     {
    //         positionOccupied[positionIndex] = false; // Mark the position as free
    //     }
    //     activeCustomers.Remove(customer);
    //     Destroy(customer);
    // }
}
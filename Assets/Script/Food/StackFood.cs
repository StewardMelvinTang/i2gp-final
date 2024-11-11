using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StackFood : MonoBehaviour
{   
    [SerializeField] private float stackDistance = 0.2f;
    public Stack<GameObject> foodStack;

    void Awake()
    {
        foodStack = new Stack<GameObject>();
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public Stack<GameObject> GetFoodStack()
    {
        return foodStack;
    }

    public void InsertFood(GameObject gameObject){

        // foreach (GameObject food in foodStack) {
        //     if (food.name == gameObject.name) {
        //         return;
        //     }
        // }
        
        gameObject.transform.SetParent(transform);
        gameObject.transform.localPosition = new Vector3(
            0, 
            foodStack.Count * stackDistance,
            0
        );
        foodStack.Push(gameObject);
    }

    // this will return all items (gameobject) in the food stack
    public List<GameObject> GetAllItems() {
        return new List<GameObject>(foodStack);
    }

    // use this to check if this has the order of requierd items
    // example usage: List<string> order = new List<string> { "Burger", "Fries" };
    // bool isOrderComplete = stackFoodObject.HasOrder(order);

    public bool HasOrder(List<string> requiredItems)
    {
        List<GameObject> itemsOnPlate = GetAllItems();
        HashSet<string> itemNames = new HashSet<string>();

        foreach (GameObject item in itemsOnPlate)
        {
            itemNames.Add(item.name); // Assuming each food item has a unique name
        }

        // Check if all required items are in the set of items on the plate
        foreach (string requiredItem in requiredItems)
        {
            if (!itemNames.Contains(requiredItem))
            {
                return false; // An item from the order is missing
            }
        }

        return true; // All items are present
    }
    
    public bool CanJoinFood(StackFood other)
    {
        Stack<GameObject> otherStack = other.GetFoodStack();
        List<GameObject> tempItems = new List<GameObject>(otherStack);

        // Check if each item in the other stack already exists in the current stack
        foreach (GameObject foodItem in tempItems)
        {
            foreach (GameObject existingItem in foodStack)
            {
                if (existingItem.name == foodItem.name) // Using name as type identifier
                {
                    Debug.Log($"Cannot join because '{foodItem.name}' already exists in the stack.");
                    return false;
                }
            }
        }

        // No duplicates found, joining is possible
        return true;
    }

    public void JoinFood(StackFood other) {
        // if (CanJoinFood(other) == false) return;
        Stack<GameObject> otherStack = other.GetFoodStack();

        List<GameObject> tempItems = new List<GameObject>();

        // Temporarily hold the other stack's items in a list
        while (otherStack.Count > 0) tempItems.Add(otherStack.Pop());

        Debug.Log("Iterating through items to join stacks");

        // Iterate over each item to add to the current stack
        for (int i = tempItems.Count-1; i >= 0; i--)
        {
            GameObject foodItem = tempItems[i];

            // Check if an item of the same type already exists in foodStack
            // bool itemExists = false;
            // foreach (GameObject existingItem in foodStack)
            // {
            //     if (existingItem.name == foodItem.name) // Using name as type identifier
            //     {
            //         itemExists = true;
            //         Debug.Log($"Item '{foodItem.name}' already exists in the stack, skipping...");
            //         break;
            //     }
            // }

            // // If item of the same type doesn't exist, add it to the stack
            // if (!itemExists)
            // {
                foodItem.transform.SetParent(transform, false);
                foodItem.transform.localPosition = new Vector3(
                    0,
                    foodStack.Count * stackDistance,
                    0
                );

                foodStack.Push(foodItem);
            // }
        }
    }

    public GameObject Pop(){
        if(foodStack.Count > 0){
            return foodStack.Pop();
        }
        else{
            return null;
        }
    }

}

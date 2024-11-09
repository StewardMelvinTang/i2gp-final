using System.Collections;
using System.Collections.Generic;
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
        gameObject.transform.SetParent(transform);
        gameObject.transform.localPosition = new Vector3(
            0, 
            foodStack.Count * stackDistance,
            0
        );
        foodStack.Push(gameObject);
    }

    public void JoinFood(StackFood other)
    {
        Stack<GameObject> otherStack = other.GetFoodStack();
        int currentSize = foodStack.Count;

        List<GameObject> tempItems = new List<GameObject>();

        while (otherStack.Count > 0) tempItems.Add(otherStack.Pop());
        Debug.Log("Iterating");
        for (int i = 0; i < tempItems.Count; i++)
        {
            GameObject foodItem = tempItems[i];
            foodItem.transform.SetParent(transform, false);
           
            Debug.Log("Parent of " + foodItem.name + " after SetParent: " + foodItem.transform.parent.name);
            foodItem.transform.localPosition = new Vector3(
                0,
                (currentSize + i) * stackDistance,
                0
            );

            foodStack.Push(foodItem);
        }
    }
}

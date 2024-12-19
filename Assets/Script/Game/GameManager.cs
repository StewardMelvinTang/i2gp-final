using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RecipeManager recipeManager;
    public OrderUiManager orderUiManager;
    // Start is called before the first frame update
    void Start()
    {
        // first order 
        Recipe randomRecipe = recipeManager.GetRandomRecipe();

        orderUiManager.AddOrder(randomRecipe);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

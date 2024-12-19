using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public RecipeDatabase recipeDatabase;    
    // Start is called before the first frame update
    void Start()
    {
        // debug print
        foreach (var recipe in recipeDatabase.recipes)
        {
            Debug.Log("Recipe: " + recipe.recipeName);
            Debug.Log("Ingredients: " + string.Join(", ", recipe.ingredients));
        }
    }

    public Recipe GetRecipeByIndex(int index) {
        if (index >= recipeDatabase.recipes.Count) return null;
        return recipeDatabase.recipes[index];
    }

    public Recipe GetRandomRecipe() {
        int randomIndex = Random.Range(0, recipeDatabase.recipes.Count);
        return recipeDatabase.recipes[randomIndex];
    }
}

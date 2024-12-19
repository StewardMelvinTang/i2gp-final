using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUiManager : MonoBehaviour
{
    public GameObject orderPrefab;
    public Transform orderPanel;
    public Dictionary<string, Sprite> ingredientIcons;
    

    public void AddOrder(Recipe recipe)
    {
        string recipeName = recipe.recipeName;
        Sprite recipeIcon = recipe.recipeIcon;
        List<Ingredient> ingredients = recipe.ingredients;
        
        
        // string recipeName, List<string> ingredients, Sprite recipeIcon
        // Instantiate a new order
        GameObject newOrder = Instantiate(orderPrefab, orderPanel);

        // Set the recipe icon
        Image recipeImage = newOrder.transform.Find("RecipeIcon").GetComponent<Image>();
        recipeImage.sprite = recipeIcon;

        // Add ingredients
        Transform ingredientsPanel = newOrder.transform.Find("IngredientsPanel");
        foreach (Ingredient ingredient in ingredients)
        {
            // GameObject ingredientIcon = new GameObject("IngredientIcon", typeof(Image));
            // ingredientIcon.transform.SetParent(ingredientsPanel);

            // // Set the ingredient icon
            // Image iconImage = ingredientIcon.GetComponent<Image>();
            // if (ingredientIcons.TryGetValue(ingredient, out Sprite icon))
            // {
            //     iconImage.sprite = icon;
            // }
            // else
            // {
            //     Debug.LogWarning($"No icon found for ingredient: {ingredient}");
            // }

            // // Resize and position the ingredient icon
            // RectTransform rect = ingredientIcon.GetComponent<RectTransform>();
            // rect.sizeDelta = new Vector2(32, 32); // Example size
        }
    }
    
    public void RemoveOrder(GameObject order)
    {
        Destroy(order);
    }
}

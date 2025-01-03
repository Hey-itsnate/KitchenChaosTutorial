using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerDuration = 4f;
    private int waitingRecipesMax = 4;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            //Recipte Timer Finisehd
            spawnRecipeTimer = spawnRecipeTimerDuration;

            //Add New Recipe to waitingRecipeSO List
            RecipeSO waitingRecipe = recipeListSO.RecipteSOList[Random.Range(0, recipeListSO.RecipteSOList.Count)];
            Debug.Log("New Recipe: " + waitingRecipe.name);
            waitingRecipeSOList.Add(waitingRecipe);
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) 
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if (waitingRecipeSO.Ingredients.Count == plateKitchenObject.GetKitchenObjectSOs().Count) 
            {
                bool plateMathcesRecipe = true;
                //Plate Has the same number of ingredients as one of the waitingRecipes
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.Ingredients) 
                {
                    bool found = false;
                    //cycle through all ingredients in recipe
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOs()) 
                    {
                        //Cycke through all ingredients on plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //Ingredients Match.
                            found = true;
                            break;
                        }
                    }
                    if (!found) 
                    {
                        //Ingredients don't match
                        plateMathcesRecipe = false;
                    }
                }

                if (plateMathcesRecipe) 
                {
                    //Player delievered valid recipe
                    Debug.Log("Player delivered the correct Recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }

        //Player didn't deliver right recipe
        Debug.Log("Invalid recipe!");
    }

}

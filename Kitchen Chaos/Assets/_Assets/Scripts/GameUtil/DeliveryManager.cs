using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerDuration = 4f;
    private int waitingRecipesMax = 4;
    private int successfulRecipesAmount;

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

            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                //Add New Recipe to waitingRecipeSO List
                RecipeSO waitingRecipe = recipeListSO.RecipteSOList[UnityEngine.Random.Range(0, recipeListSO.RecipteSOList.Count)];
                //Debug.Log("New Recipe: " + waitingRecipe.name);
                waitingRecipeSOList.Add(waitingRecipe);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
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
                    //Debug.Log("Player delivered the correct Recipe");
                    successfulRecipesAmount++;
                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        //Player didn't deliver right recipe
        Debug.Log("Invalid recipe!");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetRecipeListSOs() { return waitingRecipeSOList; }

    public int GetSuccefulRecipesAmount() { return successfulRecipesAmount; }
}

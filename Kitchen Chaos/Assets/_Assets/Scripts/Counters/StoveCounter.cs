using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    #region Fields
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs 
    {
        public StoveState state;
    }

    public enum StoveState {Idle,Frying,Fried,Burned }
    private StoveState state;
    [SerializeField] FryingRecipeSO[] FryingRecipes;
    [SerializeField] BurningRecipeSO[] BurningRecipes;
    private float fryingTimer, burningTimer;
    FryingRecipeSO currentFryingRecipeSO;
    BurningRecipeSO currentBurningRecipeSO;

    public bool Testing;
    #endregion

    private void Start()
    {
        state = StoveState.Idle;
    }

    private void Update()
    {
        if (HasKitchObject())
        {
            //There is a kitchen object on stove
            switch (state)
            {
                case StoveState.Idle:
                    if (Testing) Debug.Log("Idle");
                    break;
                    
                case StoveState.Frying:
                    //Frying kitchen Object
                    if (Testing) Debug.Log("Current State: Frying || Fry Time: " + fryingTimer);
                    fryingTimer += Time.deltaTime;
                    if (fryingTimer > currentFryingRecipeSO.fyingTime)
                    {
                        //Done Frying || Destroy current kitchen object and spawn frying recipe output.
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(currentFryingRecipeSO.output, this);

                        //Setup Burn State
                        currentBurningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        burningTimer = 0f;
                        state = StoveState.Fried;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = this.state }); ;
                        
                    }
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = fryingTimer / currentFryingRecipeSO.fyingTime });

                    break;
                case StoveState.Fried:
                    //Burning kitchen Object
                    if (Testing) Debug.Log("Current State: Fried || Burn Timer: " + burningTimer);
                    burningTimer += Time.deltaTime;
                    if (burningTimer > currentBurningRecipeSO.burnTime)
                    {
                        //Done Frying || Destroy current kitchen object and spawn frying recipe output.
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(currentBurningRecipeSO.output, this);

                        //Set Burn State
                        burningTimer = 0f;
                        state = StoveState.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = this.state }); ;
                    }
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = burningTimer / currentBurningRecipeSO.burnTime });
                    break;
                case StoveState.Burned:
                    if (Testing) Debug.Log("Burned");
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchObject())
        {
            //No Kitchen Object on Counter
            if (player.HasKitchObject())
            {
                //Player Is carrying something
                if (HasFryingRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Player is holding a fryable kitchen object
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    //Get The cutting recipe for the kitchen object and invoke the OnCuttingProgressChanged Event w/ cuttingProgress = 0.
                     currentFryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = StoveState.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = this.state }); ;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = fryingTimer / currentFryingRecipeSO.fyingTime });
                }
            }
        }
        else
        {
            //Has a Kitchen Object on Counter.
            if (player.HasKitchObject())
            {
                //Player is carrying Something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Add ingredient to plate IF the plate doesn't already have the ingredient.
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        GetKitchenObject().DestroySelf();

                    //Hande State Change
                    state = StoveState.Idle;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = this.state });
                    OnProgressChanged?.Invoke(player, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
                }
            }
            else
            {
                //Player is not carrying something || Give the player the kitchenObject
                GetKitchenObject().SetKitchenObjectParent(player);
                state = StoveState.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = this.state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f});
            }
        }
    }

    private bool HasFryingRecipe(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipe = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipe != null)
        {
            return fryingRecipe.output;
        }
        else
            return null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipe in FryingRecipes)
        {
            if (fryingRecipe.input == inputKitchenObjectSO)
            {
                return fryingRecipe;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipe in BurningRecipes)
        {
            if (burningRecipe.input == inputKitchenObjectSO)
            {
                return burningRecipe;
            }
        }
        return null;
    }

    public bool IsFried() 
    {
        return state == StoveState.Fried;
    }
}

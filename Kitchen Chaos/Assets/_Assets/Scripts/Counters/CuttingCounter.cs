using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class CuttingCounter : BaseCounter, IHasProgress
{
    #region Fields
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipesSO;
    private int cuttingProgress = 0;

    #endregion

    #region Methods
    public override void Interact(Player player)
    {

        if (!HasKitchObject())
        {
            //No Kitchen Object on Counter
            if (player.HasKitchObject())
            {
                //Player Is carrying something
                if (HasCuttingRecipe(player.GetKitchenObject().GetKitchenObjectSO())) 
                {
                    //Player is holding a cuttable kitchen object
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    //Get The cutting recipe for the kitchen object and invoke the OnCuttingProgressChanged Event w/ cuttingProgress = 0.
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized =  (float) cuttingProgress/cuttingRecipeSO.cuttingProgressMax}) ;
                }
                
            }
        }
        else
        {
            //Has a Kitchen Object on Counter.
            if (player.HasKitchObject())
            {
                //Player is carrying Something
            }
            else
            {
                //Player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchObject() && HasCuttingRecipe(GetKitchenObject().GetKitchenObjectSO())) 
        {
            //There is a kitcheObect Here AND it can be cut.
            cuttingProgress++;

            //Get the cuttingRecipeSO of the current kitchenObject of the counter
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                //Spawn cuttingRecipeOutput when cuttingProgress is complete
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);

                cuttingProgress = 0;
            }

            //Invoke Cutting Progress Event.
            OnCut.Invoke(this, EventArgs.Empty);
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax });
        }
    }

    private bool HasCuttingRecipe(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null) 
        {
            return cuttingRecipeSO.output;
        }
        else
        return null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) 
    {
        foreach (CuttingRecipeSO cuttingRecipe in cuttingRecipesSO)
        {
            if (cuttingRecipe.input == inputKitchenObjectSO)
            {
                return cuttingRecipe;
            }
        }
        return null;
    }

    #endregion
}

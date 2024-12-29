using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class CuttingCounter : BaseCounter
{
    #region Fields
    [SerializeField] private CuttingRecipeSO[] cuttingRecipesSO;

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
                //Player is not carrying Sooething
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    private bool HasCuttingRecipe(KitchenObjectSO inputKitchenObjectSO) 
    {
        foreach (CuttingRecipeSO cuttingRecipe in cuttingRecipesSO)
        {
            if (cuttingRecipe.input == inputKitchenObjectSO)
            {
                return true;
            }
        }
        return false;
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchObject() && HasCuttingRecipe(GetKitchenObject().GetKitchenObjectSO())) 
        {
            //There is a kitcheObect Here AND it can be cut.
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipe in cuttingRecipesSO) 
        {
            if (cuttingRecipe.input == inputKitchenObjectSO) 
            {
                return cuttingRecipe.output;
            }
        }
        return null;
    }

    #endregion
}

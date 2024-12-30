using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) 
    {
        if (!HasKitchObject())
        {
            //No Kitchen Object on Counter
            if (player.HasKitchObject()) 
            {
                //Player Is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this); 
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
                    //Player is holding a Plate
                    //Add ingredient to plate IF the plate doesn't already have the ingredient.
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        GetKitchenObject().DestroySelf();
                }
                else 
                {
                    //player is holding something that isn't a plate
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject1)) 
                    {
                        //There is a plate on clearCounter
                        if (plateKitchenObject1.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) 
                        {
                            //Succefully added ingredient to plate
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                } 
            }
            else 
            {
                //Player is not carrying Sooething
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}

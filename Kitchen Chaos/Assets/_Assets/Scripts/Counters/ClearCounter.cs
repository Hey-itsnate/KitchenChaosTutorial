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
            }
            else 
            {
                //Player is not carrying Sooething
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}

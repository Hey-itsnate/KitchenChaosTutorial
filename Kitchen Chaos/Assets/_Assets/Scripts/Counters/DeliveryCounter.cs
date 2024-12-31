using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchObject()) 
        {
            //Player has a kitchen object
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) 
            {
                //player has a plate
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}

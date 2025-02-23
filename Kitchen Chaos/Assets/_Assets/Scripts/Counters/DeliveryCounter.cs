using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance;
    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchObject()) 
        {
            //Player has a kitchen object
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) 
            {
                //player has a plate
                //Deliver Plate
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);       
                player.GetKitchenObject().DestroySelf();
                
            }
        }
    }
}

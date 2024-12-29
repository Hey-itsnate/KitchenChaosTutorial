using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class CuttingCounter : BaseCounter
{
    #region Fields
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchObject()) 
        {
            Debug.Log("Cutting");
            //There is a kitcheObect Here
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }

    #endregion
}

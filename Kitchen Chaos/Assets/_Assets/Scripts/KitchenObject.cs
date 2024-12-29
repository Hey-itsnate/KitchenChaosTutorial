using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent KitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO(){return kitchenObjectSO;}

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        //Clear Kitchen Object from current KitchenObjectParent
        if (this.KitchenObjectParent != null) 
        {
            this.KitchenObjectParent.ClearKitchenObject();
        }

        //Set the new KitchenObjectParent
        this.KitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchObject()) 
        {
            Debug.LogError("IKitchenObjectParent already has a Kitchen Object!");
        }
        kitchenObjectParent.SetKitchenObject(this);


        //Move Kitchen Obkect to IKitchenObject postion
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() 
    {
        return KitchenObjectParent;
    }
}

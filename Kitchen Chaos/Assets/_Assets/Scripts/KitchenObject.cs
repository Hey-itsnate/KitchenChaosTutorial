using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    /// <summary>
    /// Properly Destroys kithen object
    /// </summary>
    public void DestroySelf() 
    {
        KitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public IKitchenObjectParent GetKitchenObjectParent() 
    {
        return KitchenObjectParent;
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) 
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
}

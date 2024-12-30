using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPosition;
    private KitchenObject kitchenObject;

    public virtual void Interact(Player player) 
    {
        Debug.LogError("BaseCounter Interact");
    }

    public virtual void InteractAlternate(Player player)
    {
        Debug.LogWarning("BaseCounter Alt Interact");
    }


    #region IKitchenObjectParent Interface
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPosition;
    }

    public bool HasKitchObject()
    {
        return kitchenObject != null;
    }
    #endregion

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
}

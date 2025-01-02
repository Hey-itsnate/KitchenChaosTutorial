using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;
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

        if (kitchenObject != null) OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
    }
}

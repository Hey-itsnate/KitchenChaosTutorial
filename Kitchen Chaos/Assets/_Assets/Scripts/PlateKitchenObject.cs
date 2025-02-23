using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchObjects;
    private List<KitchenObjectSO> kitchenObjects;


    private void Awake()
    {
        kitchenObjects = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) 
    {
        if (!validKitchObjects.Contains(kitchenObjectSO) || kitchenObjects.Contains(kitchenObjectSO)) 
        {
            //returns false if not a valid ingredien or plate already has ingredient
            return false;
        }
        kitchenObjects.Add(kitchenObjectSO);
        OnIngredientAdded.Invoke(this, new OnIngredientAddedEventArgs {kitchenObjectSO = kitchenObjectSO });

        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOs() { return kitchenObjects; }
}

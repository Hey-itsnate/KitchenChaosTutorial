using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
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

        return true;
    }
}

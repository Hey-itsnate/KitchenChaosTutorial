using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipesSO;

    public KitchenObjectSO input;
    public KitchenObjectSO output;
}

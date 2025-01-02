using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    #region Methods

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeSpawned += Instance_OnRecipeSpawned;

        UpdateVisuals();
    }
    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Instance_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    private void Instance_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        foreach (Transform child in container) 
        {
            if (child == recipeTemplate) { continue; }
            Destroy(child.gameObject); 
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetRecipeListSOs()) 
        {
            Transform template = Instantiate(recipeTemplate, container);
            template.gameObject.SetActive(true);
            template.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }

    #endregion
}



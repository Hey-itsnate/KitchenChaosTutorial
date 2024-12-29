using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPosition;
    [SerializeField]private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private ClearCounter seconClearCounter;
    public bool Testing;

    private KitchenObject kitchenObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Testing && Input.GetKeyDown( KeyCode.T)) 
        {
            if (kitchenObject != null)
            {
                Debug.Log("Moved to new COunter");
                kitchenObject.SetKitchenObjectParent(seconClearCounter);
            }
        }
    }

    public void Interact(Player player) 
    {
        if (kitchenObject == null)
        {
            //Spawn KitchenOject as a child to the ClearCounter's CounterTopPosition.
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPosition.position, Quaternion.identity);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else 
        {
            //Give Object To player
            kitchenObject.SetKitchenObjectParent(player);
        }
    }

    public Transform GetKitchenObjectFollowTransform() 
    {
        return counterTopPosition;
    }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void SetKitchenObject(KitchenObject kitchenObject) 
    {
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject() 
    {
         kitchenObject = null;
    }

    public bool HasKitchObject() 
    {
        return kitchenObject != null;
    }
}

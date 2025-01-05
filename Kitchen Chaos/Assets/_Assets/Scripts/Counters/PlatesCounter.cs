using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    #region Fields
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnTimerDuration = 4f;
    private int platesSpawnedAmount, platersSpawnedAmountMax = 4;
    #endregion
    private void Update()
    {
        if (KitchenGameManager.instance.IsGamePLaying())
        {
            spawnPlateTimer += Time.deltaTime;
            if (spawnPlateTimer > spawnTimerDuration)
            {
                //plate spawn timer finished
                spawnPlateTimer = 0f;
                if (platesSpawnedAmount < platersSpawnedAmountMax)
                {
                    platesSpawnedAmount++;

                    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchObject()) 
        {
            //player has empty handed
            if (platesSpawnedAmount > 0) 
            {
                //Give player a plate if there are plates on the counter
                platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this,  EventArgs.Empty);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatesCounteVisual : MonoBehaviour
{
    #region Fields
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform CounterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjects;
    #endregion


    private void Awake()
    {
        plateVisualGameObjects = new List<GameObject>(); 
    }
    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjects[plateVisualGameObjects.Count - 1];
        plateVisualGameObjects.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, CounterTopPoint);

        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjects.Count, 0);

        plateVisualGameObjects.Add(plateVisualTransform.gameObject);
    }
}

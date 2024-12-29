using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.VFX;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjects;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter == baseCounter)
        {
            Show();
        }
        else 
        {
            Hide();
        }
    }

    void Hide() 
    {
        foreach (GameObject visualGameObject in visualGameObjects)
        { visualGameObject.SetActive(false); }
    }

    void Show() 
    {
        foreach (GameObject visualGameObject in visualGameObjects)
        { visualGameObject.SetActive(true); }
    }
}

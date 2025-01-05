using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";

    [SerializeField] TextMeshProUGUI countdownText;
    private Animator animator;
    private int previousCountdownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        int countdownNumber = Mathf.CeilToInt(KitchenGameManager.instance.GetCountdownStartTimer());
        KitchenGameManager.instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }
    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(KitchenGameManager.instance.GetCountdownStartTimer());
        countdownText.text = countdownNumber.ToString();

        if (previousCountdownNumber != countdownNumber) 
        {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.instance.PlayCouuntdownSound();
        }
        countdownText.text = Mathf.Ceil(KitchenGameManager.instance.GetCountdownStartTimer()).ToString();
    }
    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.instance.IsCountdownStartActive())
        {
            Show();
        }
        else 
        {
            Hide();
        }
    }

    private void Show()
    {
       gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        KitchenGameManager.instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        mainMenuButton.onClick.AddListener(LoadMainMenu);
        Hide();
    }
    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.instance.IsGameOver())
        {
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccefulRecipesAmount().ToString();
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
        mainMenuButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void LoadMainMenu() 
    {
        Loader.Load(Loader.Scene.MainMenu);
    }
}

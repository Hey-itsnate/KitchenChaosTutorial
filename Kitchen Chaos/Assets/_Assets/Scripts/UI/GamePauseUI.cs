using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton, mainMenuButton, optionButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.MainMenu); });
        optionButton.onClick.AddListener(() => { OptionsUI.instance.Show(); });
    }

    private void Start()
    {
        KitchenGameManager.instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    void Show() 
    {
    gameObject.SetActive(true);
    }

    void Hide() 
    {
        gameObject.SetActive(false);
    }
}

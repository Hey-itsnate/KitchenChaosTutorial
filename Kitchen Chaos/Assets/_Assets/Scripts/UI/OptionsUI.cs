using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI instance { get; private set; }

    [SerializeField] private Button soundEffectsButton, musicButton,closeButton;
    [SerializeField] TextMeshProUGUI soundEffectsText, musicText;

    private void Awake()
    {
        instance = this; 

        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.instance.ChangeVolume();
            UpdateVisuals();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.instance.ChangeVolume();
            UpdateVisuals();
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        KitchenGameManager.instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    public void Show() 
    {
        UpdateVisuals();
        gameObject.SetActive(true);
    }

    public void Hide() 
    {
        gameObject.SetActive(false);
    }

    void UpdateVisuals()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.instance.GetVolume() * 10f);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI instance { get; private set; }

    [SerializeField] private Button soundEffectsButton, musicButton,closeButton, moveUpButton, moveDownButton, moveLeftButton, moveRightButton, interactButton, interactAltButton, pauseButton;
    [SerializeField] private Button gamePad_InteractButton, gamePad_AltInteractButton, gamePad_PauseButton;
    [SerializeField] TextMeshProUGUI soundEffectsText, musicText, moveUpText, moveDownText, moveLeftText, moveRightText, interactText, interactAltText, pauseText;
    [SerializeField] TextMeshProUGUI gamePad_InteractText, gamePad_AltInteractText, gamePad_pauseText;
    [SerializeField] Transform pressToRebindKeyTransform;

    private Action onCloseButtonAction;

    private void Awake()
    {
        instance = this; 

        //Set Up option buttons
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
            onCloseButtonAction();
        });

        //SetUp Rebinding buttons
        moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact_Alternate); });
        pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
        gamePad_InteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamePad_Interact); });
        gamePad_AltInteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamePad_Interact_Alternate); });
        gamePad_PauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamePad_Pause); });
    }

    private void Start()
    {
        KitchenGameManager.instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        HidePressToRebindKey();
        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    public void Show(Action onCloseButtonAction) 
    {
        this.onCloseButtonAction = onCloseButtonAction;
        soundEffectsButton.Select();
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

        moveUpText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        pauseText.text = GameInput.instance.GetBindingText(GameInput.Binding.Pause);
        gamePad_InteractText.text = GameInput.instance.GetBindingText(GameInput.Binding.GamePad_Interact);
        gamePad_AltInteractText.text = GameInput.instance.GetBindingText(GameInput.Binding.GamePad_Interact_Alternate);
        gamePad_pauseText.text = GameInput.instance.GetBindingText(GameInput.Binding.GamePad_Pause);
    }

    void ShowPressToRebindKey() 
    {
        pressToRebindKeyTransform?.gameObject.SetActive(true);
    }

    void HidePressToRebindKey() 
    {
        pressToRebindKeyTransform?.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding) 
    {
        ShowPressToRebindKey();
        GameInput.instance.RebindBinding(binding, () => 
        {
            HidePressToRebindKey();
            UpdateVisuals();
        }); ;
    }
}

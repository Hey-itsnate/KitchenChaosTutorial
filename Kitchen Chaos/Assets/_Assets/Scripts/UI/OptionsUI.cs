using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI instance { get; private set; }

    [SerializeField] private Button soundEffectsButton, musicButton,closeButton, moveUpButton, moveDownButton, moveLeftButton, moveRightButton, interactButton, interactAltButton, pauseButton;
    [SerializeField] TextMeshProUGUI soundEffectsText, musicText, moveUpText, moveDownText, moveLeftText, moveRightText, interactText, interactAltText, pauseText;
    [SerializeField] Transform pressToRebindKeyTransform;
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

        moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact_Alternate); });
        pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
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

        moveUpText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        pauseText.text = GameInput.instance.GetBindingText(GameInput.Binding.Pause);
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

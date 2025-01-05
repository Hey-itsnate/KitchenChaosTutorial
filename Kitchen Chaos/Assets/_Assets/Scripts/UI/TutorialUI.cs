using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moveUpKeyText, moveDownKeyText, moveLeftKeyText, moveRightKeyText;
    [SerializeField] TextMeshProUGUI interactKeyText, interactAltText, pauseText;
    [SerializeField] TextMeshProUGUI gamepad_interactButtonText, gamepad_alt_interactButtonText, gamepad_pauseButtonText;

    private void Start()
    {
        GameInput.instance.OnBindingRebind += GameInput_OnBindingRebind;
        KitchenGameManager.instance.OnStateChanged += KitchenManager_OnStateChanged;
        UpdateVisuals();
        Show();
    }

    private void KitchenManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.instance.IsCountdownStartActive()) 
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals() 
    {
        moveUpKeyText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownKeyText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftKeyText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightKeyText.text = GameInput.instance.GetBindingText(GameInput.Binding.Move_Right);
        interactKeyText.text = GameInput.instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        pauseText.text = GameInput.instance.GetBindingText(GameInput.Binding.Pause);
        gamepad_interactButtonText.text = GameInput.instance.GetBindingText(GameInput.Binding.GamePad_Interact);
        gamepad_alt_interactButtonText.text = GameInput.instance.GetBindingText(GameInput.Binding.GamePad_Interact_Alternate);
        gamepad_pauseButtonText.text = GameInput.instance.GetBindingText(GameInput.Binding.GamePad_Pause);
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

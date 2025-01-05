using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultsUI : MonoBehaviour
{
    [SerializeField] private Image backgroundImage, iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color sucessColor, failedColor;
    [SerializeField] private Sprite successSprite, failedSprite;
    private const float DELAY_HIDE_DURATION = 1.5f;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        Hide();
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "Delivery\nFailed!";
        Show();
        StartCoroutine(DelayHide(DELAY_HIDE_DURATION));
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        backgroundImage.color = sucessColor;
        iconImage.sprite = successSprite;
        messageText.text = "Delivery\nSuccess!";
        Show();
        StartCoroutine(DelayHide(DELAY_HIDE_DURATION));
    }

    void Show() 
    {
        gameObject.SetActive(true);

    }

    void Hide() 
    {
        gameObject.SetActive(false);
    }

    IEnumerator DelayHide(float time) 
    {
        yield return new WaitForSeconds(time);
        Hide();
    }
}

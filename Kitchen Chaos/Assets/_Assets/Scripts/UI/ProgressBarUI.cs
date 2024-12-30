using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImg;
    [SerializeField] private GameObject hasProgressGameObject;
    private IHasProgress hasProgress;
    
    // Start is called before the first frame update
    void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null) Debug.LogError("Game Object: " + hasProgressGameObject + " does not have a component that implements the interface IHasProgress!");
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        barImg.fillAmount = 0f;

        Hide();
    }

    private void Show() 
    {
        gameObject.SetActive(true);
    }

    private void Hide() 
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Update the Bar Image fill amount.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImg.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else 
        {
            Show();
        }
    }
}

    

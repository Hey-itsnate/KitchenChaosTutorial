using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImg;
    [SerializeField] private CuttingCounter cuttingCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        cuttingCounter.OnCuttingProgressChanged += CuttingCounter_OnCuttingProgressChanged;

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
    private void CuttingCounter_OnCuttingProgressChanged(object sender, CuttingCounter.OnCuttingProgressChangedEventArgs e)
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

    

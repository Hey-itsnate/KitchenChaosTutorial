using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;

    private void Start()
    {
        Hide();
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount && e.progressNormalized <1;

        if (show)
        {
            Show();
        }
        else 
        {
            Hide();
        }
    }

    void Show() 
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
        Debug.Log("ping");
    }
}

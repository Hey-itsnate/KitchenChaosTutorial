using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;
    private float warningSoungTimer;
    private bool playWarningSound;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount && e.progressNormalized < 1;


    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoungTimer -= Time.deltaTime;
            if (warningSoungTimer < 0)
            {
                float warningSoundTimerMax = .2f;
                warningSoungTimer = warningSoundTimerMax;
            }

            SoundManager.instance.PlayWarningSound(stoveCounter.transform.position);
        }
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.StoveState.Frying || e.state == StoveCounter.StoveState.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else 
        {
            audioSource.Pause();
        }

        
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    [SerializeField] private AudioClipRefsSO soundRefSO;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += Delivery_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += Delivery_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter counter = sender as TrashCounter;
        PlaySound(soundRefSO.trash, counter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter counter = sender as BaseCounter;
        PlaySound(soundRefSO.objectDrop, counter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(soundRefSO.objectPickup, Player.Instance.transform.position);
    }

    void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, volume);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(soundRefSO.chop, cuttingCounter.transform.position);
    }

    

    private void Delivery_OnRecipeFailed(object sender, System.EventArgs e)
    {
        PlaySound(soundRefSO.deliveryFail,DeliveryCounter.Instance.transform.position);
    }

    private void Delivery_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        PlaySound(soundRefSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    public void PlayFootstepsSound(Vector3 position, float volume) 
    {
        PlaySound(soundRefSO.footStep, position, volume);
    }
}

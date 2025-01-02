using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
   private Player player;
    private float footStepTimer;
    private float footStepTimerDuration;




    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if (footStepTimer < 0f)
        {
            footStepTimer = footStepTimerDuration;

            if (player.IsWalking())
            {
                float volume = 1f;

                SoundManager.instance.PlayFootstepsSound(Player.Instance.transform.position, volume);
            }
        
        }
    }
}

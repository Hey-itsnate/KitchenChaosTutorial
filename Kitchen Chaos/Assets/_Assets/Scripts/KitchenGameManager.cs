using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager instance { get; private set; }

    public event EventHandler OnStateChanged;

    private enum GameState 
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private GameState state;
    private float waitingToStartTimer = 1f;
    private float countDownToStartTimer = 3;
    private float gamePlayingTimer = 10f;

    private void Awake()
    {
        instance = this; 
        state= GameState.WaitingToStart;
    }

    private void Update()
    {
        switch (state) 
        {
            case GameState.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f) 
                {
                    state = GameState.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.CountdownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer < 0f)
                {
                    state = GameState.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = GameState.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.GameOver:
                break;
        }

        Debug.Log(state);
    }

    public bool IsGamePLaying() 
    {
        return state == GameState.GamePlaying;
    }

    public bool IsCountdownStartActive() 
    {
        return state == GameState.CountdownToStart;
    }

    public float GetCountdownStartTimer() { return countDownToStartTimer; }
}

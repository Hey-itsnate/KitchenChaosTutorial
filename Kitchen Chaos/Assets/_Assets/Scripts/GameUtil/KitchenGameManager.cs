using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    #region Fields
    public static KitchenGameManager instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused, OnGameUnpaused;


    private enum GameState 
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private GameState state;
    private float countDownToStartTimer = 3;
    private float gamePlayingTimer;
    private float gamePlayingMax = 120f;
    private bool isGamePaused = false;
    #endregion

    #region Methods
    private void Awake()
    {
        instance = this; 
        state= GameState.WaitingToStart;
    }

    private void Start()
    {
        GameInput.instance.OnInteractPeformed += GameInput_OnInteractPeformed;
        GameInput.instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnInteractPeformed(object sender, EventArgs e)
    {
        if (state == GameState.WaitingToStart) 
        {
            state = GameState.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }



    private void Update()
    {
        switch (state) 
        {
            case GameState.WaitingToStart:
                break;
            case GameState.CountdownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer < 0f)
                {
                    state = GameState.GamePlaying;
                    gamePlayingTimer = gamePlayingMax;
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

        //Debug.Log(state);
    }

    /// <summary>
    /// Pause Game
    /// </summary>
    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsGamePLaying() 
    {
        return state == GameState.GamePlaying;
    }

    public bool IsCountdownStartActive() 
    {
        return state == GameState.CountdownToStart;
    }
    public bool IsGameOver() { return state == GameState.GameOver;  }
    public float GetCountdownStartTimer() { return countDownToStartTimer; }
    public float GetPlayingTimerNormalized() { return 1 - (gamePlayingTimer / gamePlayingMax); }

    #endregion
}

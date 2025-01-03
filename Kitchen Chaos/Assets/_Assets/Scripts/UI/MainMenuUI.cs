using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton, quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() => {
            //Click
            Loader.Load(Loader.Scene.GameScene);
        });

        quitButton.onClick.AddListener(() => {
            //Click
            Application.Quit();
        });
    }
}

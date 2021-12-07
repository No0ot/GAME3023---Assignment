using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControllerScript : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayMenuMusic();
    }

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}

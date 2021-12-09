using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    public AudioSource menuMusicSource;
    public AudioSource bgMusicSource;
    public AudioSource uiSoundSource;

    //Music Tracks
    public AudioClip menuMusic;
    public AudioClip overworldMusic;
    public AudioClip battleMusic;

    //UI sounds
    public AudioClip[] clickForward;
    public AudioClip[] clickBack;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        instance = this;
        //menuMusicSource.clip = menuMusic;
        //menuMusicSource.Play();
    }

    public void PlayMenuMusic()
    {
        menuMusicSource.Stop();
        bgMusicSource.Stop();
        bgMusicSource.clip = null;
        menuMusicSource.clip = menuMusic;
        menuMusicSource.Play();
    }
    /// <summary>
    /// Plays initial Gameplay music track
    /// </summary>
    public void PlayOverworldMusic()
    {
        menuMusicSource.Stop();
        bgMusicSource.Stop();
        bgMusicSource.clip = overworldMusic;
        bgMusicSource.Play();
    }

    public void PlayBattleMusic()
    {
        menuMusicSource.Stop();
        bgMusicSource.Stop();
        bgMusicSource.clip = battleMusic;
        bgMusicSource.Play();
    }

    public void PlayRandomClickForward()
    {
        uiSoundSource.clip = clickForward[Random.Range(0, (clickForward.Length))];
        uiSoundSource.Play();
    }
    /// <summary>
    /// Plays a random click sound for moving backward in menus.
    /// </summary>
    public void PlayRandomClickBackward()
    {
        uiSoundSource.clip = clickBack[Random.Range(0, (clickBack.Length))];
        uiSoundSource.Play();
    }
}

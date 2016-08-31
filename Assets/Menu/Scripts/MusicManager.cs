using UnityEngine;
using System.Collections;
using System;

public class MusicManager : MonoBehaviour {

    /*begin SINGLETON code*/
    private static MusicManager _instance = null;

    public static MusicManager instance
    {
        get
        {
            if (_instance == null)
            {
                LevelManager.instance.sendInterLevelMessage("BlueScreen", "Attempt to get instance of MusicManager before it awakens");
                LevelManager.instance.LoadLevel("BlueScreen");
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            DestroyObject(this);
            LevelManager.instance.sendInterLevelMessage("BlueScreen", "Attempt to create second instance of singleton " + name);
            LevelManager.instance.LoadLevel("BlueScreen");
        }
    }
    /*end SINGLETON code*/


    public AudioClip[] levelMusic;
    private AudioSource audioSource;
    private AudioClip currentLevelMusic = null;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        changeLevel(0);
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void changeLevel(int level)
    {
        if (levelMusic.Length > level)
        {
            AudioClip newLevelMusic = levelMusic[level];
            if (newLevelMusic && (!currentLevelMusic || newLevelMusic.name != currentLevelMusic.name))
            {
                Debug.Log("Playing music " + newLevelMusic);
                audioSource.Stop();//TODO fade out instead of dead stop
                audioSource.clip = newLevelMusic;
                if (newLevelMusic.length >= 10f) //heuristic--less than 10 seconds, assume not meant to be looped.
                {
                    audioSource.loop = true;
                }else
                {
                    audioSource.loop = false;
                }
                audioSource.Play();
                currentLevelMusic = newLevelMusic;
            }
        }

    }

    internal void setVolume(float volume)
    {
        audioSource.volume = volume;
    }
}

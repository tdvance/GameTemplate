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
                Debug.LogError("Attempt to get instance of MusicManager before it awakens");
                throw new Exception("Attempt to get instance of MusicManager before it awakens");
                //TODO replace with bluescreen
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
        Debug.Log("changeLevel called by " + name + " for level " + level);
        if (levelMusic.Length > level)
        {
            AudioClip newLevelMusic = levelMusic[level];
            if (newLevelMusic && (!currentLevelMusic || newLevelMusic.name != currentLevelMusic.name))
            {
                Debug.Log("Playing music " + newLevelMusic);
                audioSource.Stop();//TODO fade out instead of dead stop
                audioSource.clip = newLevelMusic;
                audioSource.loop = true;
                audioSource.Play();
                currentLevelMusic = newLevelMusic;
            }
        }

    }
}

using UnityEngine;
using System.Collections;
using System;

public class OptionsManager : MonoBehaviour {

    const string VOLUME_KEY = "volume";
    const string DIFFICULTY_KEY = "difficulty";
    const string PLAYER_NAME_KEY = "player_name";

    public float defaultVolume = 0.8f;
    public float defaultDifficulty = 2f;
    public string defaultPlayerName;

    // Use this for initialization
    void Start () {
        
        defaultPlayerName = Environment.UserName;
        MusicManager.instance.setVolume(getVolume());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public float getVolume()
    {
        if (PlayerPrefs.HasKey(VOLUME_KEY))
        {
            return PlayerPrefs.GetFloat(VOLUME_KEY);
        }else
        {
            return defaultVolume;
        }
    }

    public float getDifficulty()
    {
        if (PlayerPrefs.HasKey(DIFFICULTY_KEY))
        {
            return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
        }
        else
        {
            return defaultDifficulty;
        }
    }


    public string getPlayerName()
    {
        if (PlayerPrefs.HasKey(PLAYER_NAME_KEY))
        {
            return PlayerPrefs.GetString(PLAYER_NAME_KEY);
        }
        else
        {
            return defaultPlayerName;
        }
    }

    public void setVolume(float volume)
    {
        Debug.Log("Volume set to: " + volume);
        PlayerPrefs.SetFloat(VOLUME_KEY, Mathf.Clamp01(volume));
        MusicManager.instance.setVolume(getVolume());
    }

    public void setDifficulty(float difficulty)
    {
        Debug.Log("Difficulty set to: " + difficulty);
        PlayerPrefs.SetFloat(DIFFICULTY_KEY, Mathf.Round(Mathf.Clamp(difficulty, 1, 4)));
    }

    public void setPlayerName(string name)
    {
        Debug.Log("Player name set to: " + name);
        PlayerPrefs.SetString(PLAYER_NAME_KEY, name.Substring(0, Math.Min(15, name.Length)));
    }

    public void resetDefaults()
    {
        setVolume(defaultVolume);
        setDifficulty(defaultDifficulty);
        setPlayerName(defaultPlayerName);
    }

    /*begin SINGLETON code*/
    private static OptionsManager _instance = null;

    public static OptionsManager instance
    {
        get
        {
            if (_instance == null)
            {
                LevelManager.instance.sendInterLevelMessage("BlueScreen", "Attempt to get instance of OptionsManager before it awakens");
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
}

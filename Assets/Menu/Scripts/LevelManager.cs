using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelManager : MonoBehaviour
{
    private bool newSceneBeingLoaded = false;
    private string newSceneName = "";
    private string currentSceneName = "Init";
    private string lastSceneName = "None";
    private Dictionary<string, Queue> messages = new Dictionary<string, Queue>();

    public string gameScene = "BlueScreen";
    public string optionsScene = "BlueScreen";

    /*begin SINGLETON code*/

    private static LevelManager _instance = null;

    
    public static LevelManager instance
    {
        get
        {
            if (_instance == null)
            {
                //LevelManager.instance.sendInterLevelMessage("BlueScreen", "Attempt to get instance of MusicManager before it awakens");
                //LevelManager.instance.LoadLevel("BlueScreen");

                //can't use usual blue screen because it requires a level manager
                SceneManager.LoadScene("~NoLevelManager");
                throw new Exception("No level manager found.");
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
        else if(_instance != this)
        {
            DestroyObject(this);
            LevelManager.instance.sendInterLevelMessage("BlueScreen", "Attempt to create second instance of singleton " + name);
            LevelManager.instance.LoadLevel("BlueScreen");
        }
    }
    /*end SINGLETON code*/

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (newSceneBeingLoaded)
        {
            UpdateCurrentScene();
        }
    }

    public void LoadGame()
    {
        LoadLevel(gameScene);
    }

    private void UpdateCurrentScene()
    {

        Scene s = SceneManager.GetActiveScene();
        if (s.name.Equals(newSceneName) || s.name.EndsWith("/" + newSceneName))
        {
            newSceneBeingLoaded = false;
            lastSceneName = currentSceneName;
            currentSceneName = newSceneName;
            int level = s.buildIndex;
            MusicManager.instance.changeLevel(level);
        }

    }

    public void LoadOptions()
    {
        if (currentSceneName != optionsScene)
        {
            LoadLevel(optionsScene);
        }
    }

    public string getCurrentScene()
    {
        return currentSceneName;
    }

    public string getLastScene()
    {
        if (newSceneBeingLoaded)
        {
            return currentSceneName;//a new scene is being loaded, so last scene is this scene until next scene when it's last scene this time.
        }
        return lastSceneName;
    }

    public void LoadLevel(string name)
    {
        Debug.Log("Loading level: " + name);
        newSceneBeingLoaded = true;
        SceneManager.LoadScene(name);
        newSceneName = name;
    }

    public void sendInterLevelMessage(string levelName, string message)
    {
        if (!messages.ContainsKey(levelName))
        {
            messages[levelName] = new Queue();
        }
        messages[levelName].Enqueue(message);
    }

    public bool hasInterLevelMessage(string levelName)
    {
        if (messages.ContainsKey(levelName))
        {
            if (messages[levelName].Count > 0)
            {
                return true;
            }
        }
        return false;
    }

    public string receiveInterLevelMessage(string levelName)
    {
        if (hasInterLevelMessage(levelName))
        {
            return messages[levelName].Dequeue() as string;
        }
        return null;
    }

}

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ScoreManager : MonoBehaviour {

    private int _score = 0;

    //where to save high scores
    public string highScoresName = "HighScores";
    private string highScoresPath;

    //high score list
    public int maxHighScores = 10;
    private int[] highScores;

    //update high scores, if needed, this often rather than every frame.
    public float highScoreInterval = 30f;

    //lowest high score; if score exceeds this, time to update.
    private int highScoreThreshold = 0;

    //when true, don't invoke UpdateHighScores a second time.
    private bool updatingHighScores = false;

    // Use this for initialization
    void Start()
    {
        highScoresPath = Application.persistentDataPath + highScoresName;
        Reset();
        UpdateHighScores();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public int score
    {
        get
        {
            return _score;
        }
    }

    public void Add(int points)
    {
        _score += points;
        if(_score > highScoreThreshold && !updatingHighScores)
        {
            updatingHighScores = true;
            Invoke("UpdateHighScores", highScoreInterval);
        }
    }

    public void UpdateHighScores()
    {
        CancelInvoke("UpdateHighScores");//cancel pending calls

        if (maxHighScores < 1)
        {
            maxHighScores = 1;//TODO allow disabling high scores
        }
        highScores = new int[maxHighScores];

        //load high score list if it exists
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(highScoresPath))
        {
            //load high scores
            FileStream infile = File.Open(highScoresPath, FileMode.Open);
            int[] hs = (int[])bf.Deserialize(infile);
            infile.Close();

            //merge with current high scores and current score
            int[] allScores = new int[highScores.Length + hs.Length + 1];
            Array.Copy(hs, allScores, hs.Length);
            Array.Copy(highScores, 0, allScores, hs.Length, highScores.Length);
            Array.Sort(allScores);
            Array.Reverse(allScores);
            Array.Copy(allScores, highScores, highScores.Length);
            allScores[allScores.Length - 1] = score;

            //save high scores
            BinaryFormatter outBf = new BinaryFormatter();
            FileStream file = File.Open(highScoresPath, FileMode.OpenOrCreate);
            bf.Serialize(file, highScores);
            file.Close();
        }
        highScoreThreshold = highScores[highScores.Length - 1];
        updatingHighScores = false;
    }

    public void Reset()
    {
        _score = 0;
    }

   
    /*begin SINGLETON code*/

    private static ScoreManager _instance = null;

    public static ScoreManager instance
    {
        get
        {
            if (_instance == null)
            {
                LevelManager.instance.sendInterLevelMessage("BlueScreen", "Attempt to get instance of ScoreManager before it awakens");
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

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

public class ScoreManager : MonoBehaviour {

    [Serializable]
    public class HighScore :IComparable
    {
        public string name;
        public int score;

        public int CompareTo(object obj)
        {
            HighScore h = obj as HighScore;
            return score.CompareTo(h.score);
        }

    };

    private int _score = 0;

    //where to save high scores
    public string highScoresName = "HighScores";
    private string highScoresPath;

    //high score list
    public int maxHighScores = 10;
    private HighScore[] _highScores;

    // Use this for initialization
    void Start()
    {
        highScoresPath = Application.persistentDataPath + highScoresName;
        if (maxHighScores < 1)
        {
            maxHighScores = 1;//TODO allow disabling high scores
        }
        _highScores = new HighScore[maxHighScores];

        UpdateHighScores();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public HighScore[] highScores
    {
        get
        {
            return _highScores;
        }

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
    }

    public void ClearHighScores()
    {
        Debug.Log("Clearing high scores");
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = new HighScore();
            highScores[i].score = 0;
            highScores[i].name = "Nobody";
        }
        if (File.Exists(highScoresPath))
        {
            File.Delete(highScoresPath);
        }
    }

    public void UpdateHighScores()
    {
        CancelInvoke("UpdateHighScores");//cancel pending calls
        Debug.Log("UpdateHighScores called from " + name);

        //load high score list if it exists
        BinaryFormatter bf = new BinaryFormatter();
        HighScore[] hs;
        if (File.Exists(highScoresPath))
        {
            //load high scores
            Debug.Log("Loading previous high scores");
            FileStream infile = File.Open(highScoresPath, FileMode.Open);
            hs = (HighScore[])bf.Deserialize(infile);
            infile.Close();
            Debug.Log("Loaded: " + hs);
        }
        else
        {
            hs = new HighScore[highScores.Length];
            for (int i = 0; i < hs.Length; i++)
            {
                hs[i] = new HighScore();
                hs[i].score = 0;
                hs[i].name = "Nobody";
            }
        }

        //merge with current high scores and current score
        HighScore[] allScores = new HighScore[highScores.Length + hs.Length + 1];
        Array.Copy(hs, allScores, hs.Length);
        Array.Copy(highScores, 0, allScores, hs.Length, highScores.Length);
        allScores[allScores.Length - 1] = new HighScore();
        allScores[allScores.Length - 1].score = score;
        if (score > 0)
        {
            allScores[allScores.Length - 1].name = GetPlayerName();
        }else
        {
            allScores[allScores.Length - 1].name = "Nobody";
        }
        Array.Sort(allScores);
        Array.Reverse(allScores);
        Array.Copy(allScores, highScores, highScores.Length);
        Debug.Log("Merged with current: " + allScores);

        //save high scores
        FileStream file = File.Open(highScoresPath, FileMode.OpenOrCreate);
        bf.Serialize(file, highScores);
        file.Close();
        Debug.Log("High scores saved to " + highScoresPath);
    }

    private string GetPlayerName()
    {
        return Environment.UserName;
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

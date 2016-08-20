using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    public Text scoreText;

	// Use this for initialization
	void Start () {
        ScoreManager.instance.Reset();
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + ScoreManager.instance.score;
	}

    public void ScoreTenPoints()
    {
        ScoreManager.instance.Add(10);
    }

    public void GameOver()
    {
        ScoreManager.instance.UpdateHighScores();
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    public Text scoreText;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void ScoreTenPoints()
    {
        Debug.Log("ScoreTenPoints called from " + name);
        ScoreManager.instance.Add(10);
        scoreText.text = "Score: " + ScoreManager.instance.score;
    }

    public void GameOver()
    {
        Debug.Log("GameOver called from " + name);
        ScoreManager.instance.UpdateHighScores();
    }
}

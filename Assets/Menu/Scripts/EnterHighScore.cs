using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnterHighScore : MonoBehaviour {
    public Text scoreText;
    public Text playerName;
    public Text placeholderText;

    // Use this for initialization
    void Start()
    {
        scoreText.text = "Score: " + ScoreManager.instance.score;
        placeholderText.text = Environment.UserName;
    }

    public void Done()
    {
        Debug.Log("Entered name: " + playerName.text);
        ScoreManager.instance.UpdateHighScores(/*playerName.text*/);
    }

    // Update is called once per frame
    void Update () {
	
	}
}

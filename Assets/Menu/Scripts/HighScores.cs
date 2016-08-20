using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScores : MonoBehaviour {
    public Text HighScoreText;

	// Use this for initialization
	void Start ()
    {
        SetHighScoreText();
    }

    private void SetHighScoreText()
    {
        HighScoreText.text = "";
        int[] hs = ScoreManager.instance.highScores;
        for (int i = 0; i < hs.Length; i++)
        {

            HighScoreText.text += string.Format("{0,3}", i + 1) + ".  " + string.Format("{0,10}", hs[i]) + "\n";
        }
    }

    public void ClearHighScores()
    {
        ScoreManager.instance.ClearHighScores();
        SetHighScoreText();
    }

	// Update is called once per frame
	void Update () {
	
	}
}

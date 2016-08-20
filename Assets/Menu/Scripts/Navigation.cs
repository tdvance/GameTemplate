using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour {

    public string next = "";
    public string back = "";
    public string options = "";
    public string play = "";
    public string resetToDefaults = "";
    public string win = "";
    public string lose = "";

    
    public float duration = 10f;

	// Use this for initialization
	void Start () {

        if (duration >= 0)
        {
            Invoke("Next", duration);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Back()
    {
        CancelInvoke("Next");
        Debug.Log("Back from: " + name);
        if (back != "")
        {
            LevelManager.instance.LoadLevel(back);
        }
        else
        {
            LevelManager.instance.LoadLevel(LevelManager.instance.getLastScene());
        }
    }

    public void Next()
    {
        CancelInvoke("Next");
        Debug.Log("Next from: " + name);
        if (next != "")
        {
            LevelManager.instance.LoadLevel(next);
        }
       //otherwise, do nothing, at a "pendant" node of the state graph

    }

    public void Play()
    {
        CancelInvoke("Next");
        Debug.Log("Play from: " + name);
        if (play != "")
        {
            LevelManager.instance.LoadLevel(play);
        }
        else
        {
            LevelManager.instance.LoadGame();
        }
    }

    public void Win()
    {
        CancelInvoke("Next");
        Debug.Log("Win from: " + name);
        if (win != "")
        {
            LevelManager.instance.LoadLevel(win);
        }
        else
        {
            LevelManager.instance.LoadGame();
        }
    }

    public void Lose()
    {
        CancelInvoke("Next");
        Debug.Log("Lose from: " + name);
        if (lose != "")
        {
            LevelManager.instance.LoadLevel(lose);
        }
        else
        {
            LevelManager.instance.LoadGame();
        }
    }

    public void Options()
    {
        CancelInvoke("Next");
        Debug.Log("Options from: " + name);
        if (options != "")
        {
            LevelManager.instance.LoadLevel(options);
        }
        else
        {
            LevelManager.instance.LoadOptions();
        }
    }

    public void ResetToDefaults()
    {
        CancelInvoke("Next");
        Debug.Log("ResetToDefaults from: " + name);

        //TODO fill in this code

    }
}

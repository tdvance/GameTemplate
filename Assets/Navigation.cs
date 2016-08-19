using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour {

    public string next_scene = "";
    public string last_scene = "";
    public string game_scene = "";
    public string options_scene = "";
    public float duration = 10f;

	// Use this for initialization
	void Start () {
        Debug.Log("next_scene: " + next_scene);
        Debug.Log("last_scene: " + last_scene);
        Debug.Log("game_scene: " + game_scene);
        Debug.Log("options_scene: " + options_scene);

        if (duration >= 0)
        {
            Invoke("forward", duration);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void back()
    {
        CancelInvoke("forward");
        Debug.Log("Navigation: go back, scene: " + last_scene);
        if (last_scene != "")
        {
            LevelManager.instance.LoadLevel(last_scene);
        }
        else
        {
            LevelManager.instance.LoadLevel(LevelManager.instance.getLastScene());
        }
    }

    public void forward()
    {
        CancelInvoke("forward");
        Debug.Log("Navigation: go forward, scene: " + next_scene);
        if (next_scene != "")
        {
            LevelManager.instance.LoadLevel(next_scene);
        }
       //otherwise, do nothing, at a "pendant" node of the state graph

    }

    public void play()
    {
        CancelInvoke("forward");
        Debug.Log("Navigation: play game, game_scene: " + game_scene);
        if (game_scene != "")
        {
            LevelManager.instance.LoadLevel(game_scene);
        }
        else
        {
            LevelManager.instance.LoadGame();
        }
    }

    public void options()
    {
        CancelInvoke("forward");
        Debug.Log("Navigation: go to options menu, scene: " + options_scene);
        if (game_scene != "")
        {
            if (game_scene != options_scene)
            {
                LevelManager.instance.LoadLevel(options_scene);
            }
        }
        else
        {
            LevelManager.instance.LoadOptions();
        }
    }

    public void resetToDefaults()
    {
        CancelInvoke("forward");
      
        //TODO fill in this code

    }
}

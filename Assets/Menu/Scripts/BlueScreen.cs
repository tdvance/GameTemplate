using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlueScreen : MonoBehaviour {

    public Text message;

	// Use this for initialization
	void Start () {
        if(LevelManager.instance.hasInterLevelMessage("BlueScreen")){
            message.text = LevelManager.instance.receiveInterLevelMessage("BlueScreen");
            while (LevelManager.instance.hasInterLevelMessage("BlueScreen"))
            {
                message.text += "\n" + LevelManager.instance.receiveInterLevelMessage("BlueScreen");

            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

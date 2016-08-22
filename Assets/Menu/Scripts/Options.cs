using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    public InputField playerName;
    public Slider volumeControl;
    public Slider difficultySetting;

	// Use this for initialization
	void Start () {

        playerName.text = OptionsManager.instance.getPlayerName();
        volumeControl.value = OptionsManager.instance.getVolume();
        difficultySetting.value = OptionsManager.instance.getDifficulty();
        Debug.Log("volumeControl.value=" + volumeControl.value);
        volumeControl.onValueChanged.AddListener(delegate { changeVolume(); });
        difficultySetting.onValueChanged.AddListener(delegate { changeDifficulty(); });
    }

    public void changeVolume()
    {
        OptionsManager.instance.setVolume(volumeControl.value);
    }

    public void changeDifficulty()
    {
        OptionsManager.instance.setDifficulty(difficultySetting.value);
    }

    public void changePlayerName()
    {
        OptionsManager.instance.setPlayerName(playerName.text);
    }

    public void resetToDefaults()
    {
        OptionsManager.instance.resetDefaults();
        playerName.text = OptionsManager.instance.getPlayerName();
        volumeControl.value = OptionsManager.instance.getVolume();
        difficultySetting.value = OptionsManager.instance.getDifficulty();
    }
}

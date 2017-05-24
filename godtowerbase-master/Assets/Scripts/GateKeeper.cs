using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GateKeeper : MonoBehaviour {


	public Text levelText;
	public string levelNumber;
	public string password;
	public string nextScene;
	public InputField inputPassword;
	public Button okButton;
	public Text checkCorrect;
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad % 2 < 1) {
			levelText.text = levelNumber; 
		} else {
			levelText.text = "Level";
		}
	}


	public void checkPassword(){
		if (password == inputPassword.text) {
			{
				TKSceneManager.ChangeScene (nextScene);
			}
		} else {
			inputPassword.text = "";
			checkCorrect.gameObject.SetActive(true);

		}
			
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class StartScene : MonoBehaviour {

	public void Begin(){
		//SceneManager.LoadScene ("Level1");
		TKSceneManager.ChangeScene("Level1");
	}
}

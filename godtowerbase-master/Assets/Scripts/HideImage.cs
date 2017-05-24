using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideImage : MonoBehaviour {


	public Image image;
	// Use this for initialization
	void Start () {
		
	}

	public void changeWhenMouseIn(){
		var tempColor = image.color;
		tempColor.a = 0f;
		image.color = tempColor;
	}

	public void changeWhenMouseExit(){
		var tempColor = image.color;
		tempColor.a = 255f;
		image.color = tempColor;
	}

	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePicture : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public RectTransform buttonRectTransform;
	public Image imgInfont;
	public Image imgBehind;

	public void changePicture(){
		if (imgInfont.gameObject.activeInHierarchy) {
			imgBehind.gameObject.SetActive (true);
			imgInfont.gameObject.SetActive (false);
		} else {
			imgBehind.gameObject.SetActive (false);
			imgInfont.gameObject.SetActive (true);
		}
	}

	public void rotateButton(){
		if (buttonRectTransform.localRotation.z == -180) {
			buttonRectTransform.Rotate (new Vector3 (0, 0, 0));
		} else {
			buttonRectTransform.Rotate (new Vector3 (0, 0, -180));
		}
	}
}

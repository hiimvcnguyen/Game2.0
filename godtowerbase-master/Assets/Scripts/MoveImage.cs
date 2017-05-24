using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MoveImage : MonoBehaviour {

	private bool mouseDown = false;
	public Transform imageTransform;

	Vector3 imagePos ;
	Vector3 mousePos;;
	// Use this for initialization
	void Start () {
		
	}

	public void ChangePosOfImage(){
		imagePos = imageTransform.position;
		mousePos = Input.mousePosition;


	}
	// Update is called once per frame
	void Update () {
		
	}
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MoveImage : MonoBehaviour {

	private bool mouseDown = false;

	Vector3 imagePos;
	Vector3 mousePos;
	// Use this for initialization
	public Transform imageTransform;
	void Start () {
		//Cursor.SetCursor(dafaultCursor,hotSpot,curMode);
	}

	//public Texture2D dafaultCursor;
	//public Texture2D newCursor;

	private CursorMode curMode = CursorMode.Auto;
	private Vector2 hotSpot = Vector2.zero;

	/*public void CursorOnMouseIn(){

		Cursor.SetCursor(dafaultCursor,hotSpot,curMode);
	}

	public void CursorOnMouseOut(){
		Cursor.SetCursor(newCursor,hotSpot,curMode);
	}*/

	public void onPointerDown(){
		mouseDown = true;
		mousePos = Input.mousePosition;
		imagePos = imageTransform.localPosition;
	}

	public void onPointerUp(){
		mouseDown = false;
	}
		
	// Update is called once per frame
	void Update () {
		if (mouseDown) {
			Vector3 newMousePos = Input.mousePosition;
			Vector3 change = newMousePos - mousePos;
			Vector3 set = imagePos + change;
			imageTransform.localPosition = set;
		}
	}
}




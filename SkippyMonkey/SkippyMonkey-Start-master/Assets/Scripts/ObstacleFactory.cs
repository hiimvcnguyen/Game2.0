using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour {
	private static float SCREEN_HEIGHT = 480f;
	private static float DISTANCE = 200f;//distance between 2 obstacles
	public GameObject obstaclePrefab;

	private float positionY = 0;

	// Use this for initialization
	void Start () {

		for (int i = 0;; i++) {
			if (positionY >= SCREEN_HEIGHT)
				break;
			GameObject newObstacle = Instantiate (
				obstaclePrefab,
				new Vector3(Random.Range(-200f,200f),positionY,0),
				Quaternion.identity
			);
			positionY += DISTANCE;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

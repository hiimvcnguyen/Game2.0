using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour {

	public int numberOfRay;
	private Bounds colliderBouns;
	private BoxCollider2D bc2D;
	private RaycastOrigins raycastOrigins;
	public LayerMask collideMark;

	private int countTop = 0;
	private int countLeft = 0;
	private bool isCollingTop;
	private bool isCollingLeft;
	private bool isCollingBottom;
	private bool isCollingRight;


	private void Awake(){
		bc2D = GetComponent<BoxCollider2D> ();

	}

	public PlayerStatus Move(Vector2 velocity){
		UpdateColliderBounds ();
		velocity = RaycastHorizontal (velocity);
		velocity = RaycastVertical (velocity);
		return new PlayerStatus{
			velocity = velocity,
			isCollidingTop = isCollingTop,
			isCollidingLeft = isCollingLeft,
			isCollidingBottom = isCollingBottom,
			isCollidingRight = isCollingRight,
		};
	}



	private void CheckRaycastDirection(Vector2 velocity,string direction,ref Vector2 []raycastOrigin){
		if (direction.Equals ("Horizontal")) {
			for (int i = 0; i < numberOfRay; i++) {
				raycastOrigin[i] = velocity.x > 0 ? raycastOrigins.right[i] : raycastOrigins.left[i];
			}
			return;
		} else {
			for (int i = 0; i < numberOfRay; i++) {
				raycastOrigin[i] = velocity.y > 0 ? raycastOrigins.bottom[i] : raycastOrigins.top[i];
			}
			return;
		}
	}

	private void Hit(string direction,ref Vector2 velocity , Vector2 []raycastOrigin, LayerMask colliderMark){
		RaycastHit2D []hit = new RaycastHit2D[numberOfRay];
		countTop = 1;
		if (direction.Equals ("LeftOrRight")) {
			for(int i=0;i<numberOfRay;i++){
				hit[i] = Physics2D.Raycast (
					raycastOrigin[i],
					velocity.WithY (0),
					Mathf.Abs (velocity.x) + 0.005f,
					colliderMark
				);
				if (hit[i]) {
					countTop++;
					if (countTop == numberOfRay){
						Debug.Log ("Hit Horizontal");
					}
					velocity.x = (hit[i].distance - 0.005f) * Mathf.Sign(velocity.x);
				}
			}
		}else {
			countLeft = 1;
			for (int i = 0; i < numberOfRay; i++) {
				
				hit[i] = Physics2D.Raycast (
					raycastOrigin[i],
					velocity.WithX (0),
					Mathf.Abs (velocity.y) + 0.005f,
					colliderMark
				);
				if (hit[i]) {
					countLeft++;
					if (countLeft == numberOfRay) {
						isCollingTop = true;
						Debug.Log ("Hit Vertical" + isCollingTop);
					}
					velocity.y = (hit[i].distance - 0.005f) * Mathf.Sign (velocity.y);
				}
			}
		}
	}

	private Vector2 RaycastHorizontal(Vector2 velocity){
		Vector2 []raycastTopOrBottom = new Vector2[numberOfRay];
		this.CheckRaycastDirection (velocity, "Horizontal", ref raycastTopOrBottom);
		this.Hit ("LeftOrRight",ref velocity,raycastTopOrBottom,collideMark);
		return velocity;
	}

	private Vector2 RaycastVertical(Vector2 velocity){
		Vector2 []raycastLeftOrRight = new Vector2[numberOfRay];
		this.CheckRaycastDirection (velocity, "Vertical", ref raycastLeftOrRight);
		this.Hit ("TopOrBottom",ref velocity,raycastLeftOrRight,collideMark);
		return velocity;
	}



	private void UpdateColliderBounds(){
		colliderBouns = bc2D.bounds;
		colliderBouns.Expand (-0.01f); //co nho 1 chut
		UpdateRaycastOrigins();
	}

	private void UpdateRaycastOrigins(){
		raycastOrigins.left = new Vector2[numberOfRay];
		raycastOrigins.right = new Vector2[numberOfRay];
		raycastOrigins.top = new Vector2[numberOfRay];
		raycastOrigins.bottom = new Vector2[numberOfRay];
		float sizeX = colliderBouns.max.x-colliderBouns.min.x;

		float sizeY = colliderBouns.max.y-colliderBouns.min.y;
		float sizeSetX = 0;
		float sizeSetY = 0;
		int i = 0;
		while (true) {
			if (i == numberOfRay ) {
				break;
			}
			raycastOrigins.left [i] = SetRaycastOrgins (colliderBouns.min.x, sizeSetX);
			raycastOrigins.right [i] = SetRaycastOrgins (colliderBouns.max.x, sizeSetX);
			raycastOrigins.top [i] = SetRaycastOrgins (sizeSetY, colliderBouns.min.y);
			raycastOrigins.bottom [i] = SetRaycastOrgins (sizeSetY, colliderBouns.max.y);
			sizeSetX += sizeX / numberOfRay;
			sizeSetY += sizeY / numberOfRay;
			i++;
		}
	}


	private Vector2 SetRaycastOrgins(float positionX, float positionY){
		return new Vector2 (positionX, positionY);
	}
}


struct RaycastOrigins{

	public Vector2[] left;
	public Vector2[] right;
	public Vector2[] top;
	public Vector2[] bottom;
}

public struct PlayerStatus{
	public Vector2 velocity;
	public bool isCollidingTop;
	public bool isCollidingLeft;
	public bool isCollidingBottom;
	public bool isCollidingRight;
}
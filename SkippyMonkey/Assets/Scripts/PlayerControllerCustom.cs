using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;


[RequireComponent(typeof(Animator))]
public class PlayerControllerCustom : MonoBehaviour {


	public LayerMask collideMark;
	public float playerJumpSpeed;
	public float playerRunSpeed;
	private Vector2 velocity;


	private Animator anim;
	private static float SCREEN_HALF_WIDTH = 320f;
	private Controller2D controller2D;

	public void Start(){
		controller2D = GetComponent<Controller2D> ();
		anim = GetComponent<Animator> ();
		LeanTouch.OnFingerTap += Jump;
	}

	private void FixedUpdate(){
		//rgBody.velocity = rgBody.velocity.WithX(playerRunSpeed);
		if(transform.position.x > SCREEN_HALF_WIDTH){
			transform.position = transform.position.WithX(
				transform.position.x - 2 * SCREEN_HALF_WIDTH
			);
		}

		Vector3 tempPos = Vector3.zero;
		transform.position += tempPos.WithX(playerRunSpeed);
		velocity.x = transform.position.x;
		velocity.y = 0;
		transform.position = (Vector3)velocity;
		//Debug.Log ("X: " + transform.position.x + " - Y: " + transform.position.y);
	}

	protected virtual void OnDestroy(){ 
		LeanTouch.OnFingerTap -= Jump;
	}



	private void Jump(LeanFinger finger)
	{
		transform.position = transform.position.WithY (playerJumpSpeed);
		anim.SetBool("IsGrounded", false);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		anim.SetBool("IsGrounded", true);
	}
}

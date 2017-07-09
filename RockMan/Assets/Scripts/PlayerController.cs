using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(InputController))]
public class PlayerController : MonoBehaviour {
    public float movementSpeed;
    public float jumpSpeed;
	public float speedSlice;

    [Space]
    public float dashMultiplier;
    public float dashTime;


    public bool airborneSkillAvailable { get; private set; }
    public bool isDashing { get; private set; }
	public bool isAirDashing { get; private set; }
	public bool canClimb { get; private set; }
    

    private InputController inputController;
    private Controller2D controller2D;
    private Vector2 velocity;
    private int faceDirection;
	private float currentVelocityY;

    public PlayerStatus playerStatus { get; private set; }

    private void Awake()
	{
		inputController = GetComponent<InputController> ();
		controller2D = GetComponent<Controller2D> ();

		inputController.OnMovePressed += Move;
		inputController.OnJumpPressed += JumpIfPossible;
		inputController.OnDashPressed += DashIfPossible;
	}

    private void OnDestroy()
    {
        inputController.OnMovePressed -= Move;
        inputController.OnJumpPressed -= JumpIfPossible;
        inputController.OnDashPressed -= DashIfPossible;
    }


    private void FixedUpdate()
    {
		if ((playerStatus.isCollidingLeft || playerStatus.isCollidingRight) && !playerStatus.isCollidingBottom) {
			canClimb = true;
		}
		if (!playerStatus.isCollidingLeft && !playerStatus.isCollidingRight)
			canClimb = false;
		WallClimb ();

		if (isAirDashing) {
			currentVelocityY = velocity.y;
		}
        else velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        playerStatus = controller2D.Move(velocity * Time.fixedDeltaTime);

        transform.position += (Vector3)playerStatus.velocity;

        if (playerStatus.isCollidingBottom || playerStatus.isCollidingTop)
        {
            velocity.y = speedSlice;
        }

        if (playerStatus.isCollidingBottom) airborneSkillAvailable = true;
    }

    public void ActivateAirborneSkill()
    {
        airborneSkillAvailable = false;
    }


    private void JumpIfPossible()
    {
        if (playerStatus.isCollidingBottom)
        {
            Jump();
        }
    }
    public void Jump()
    {
        velocity.y = jumpSpeed;
    }


    private void DashIfPossible()
    {
        if (!isDashing && playerStatus.isCollidingBottom)
        {
            Dash();
        }
    }
	public void AirDashIfPossible()
	{
		isAirDashing = true;
		velocity.y = currentVelocityY;
		Dash();
	}
    public void Dash()
    {
        velocity.x = faceDirection * movementSpeed * dashMultiplier;
        isDashing = true;

        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
		isAirDashing = false;
    }

	public void WallClimb(){
		if (canClimb) {
			velocity.y = -2;
		}
	}


    private void Move(float direction)
    {
        if (!isDashing)
        {
            if (direction != 0)
            {
                faceDirection = (int)Mathf.Sign(direction);
            }

            velocity.x = direction * movementSpeed;
        }
    }
}

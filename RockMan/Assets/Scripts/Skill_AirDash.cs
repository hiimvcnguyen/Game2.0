using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(PlayerController))]
public class Skill_DoubleJump : MonoBehaviour {
	private InputController inputController;
	private PlayerController playerController;

	private void Awake()
	{
		inputController = GetComponent<InputController>();
		playerController = GetComponent<PlayerController>();

		inputController.OnDashPressed += AirDash;
	}

	private void OnDestroy()
	{
		inputController.OnDashPressed -= AirDash;
	}

	private void AirDash()
	{
		if(!playerController.playerStatus.isCollidingBottom
			&& playerController.airborneSkillAvailable && !playerController.isDashing)
		{

			playerController.AirDashIfPossible();
			playerController.ActivateAirborneSkill();
		}
	}
}
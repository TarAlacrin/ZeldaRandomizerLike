using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	public Rigidbody characterRigidbody;
	Vector3 facingDirection = Vector3.forward;
	Vector3 lastInertia = Vector3.zero;

	public float accel = 0.15f;
	public float maxSpeed = 5f;
	public float backwalkSlowdown = 0.7f;
	public float angleMultiplier = 2f;

	CharacterState currentAction = CharacterState.Nothing;


	private void Awake()
	{
		
	}


	void LateUpdate()
	{
		StateChecker();
	}

	public void StateChecker()
	{
		
	}



	public void MoveInputPressed(Vector2 vec2Input)
	{
		if(this.GetCanMove())
		{
			Vector3 inputDir = new Vector3(vec2Input.x, 0f, vec2Input.y);

			MoveCharacter(inputDir);
		}
	}

	public void LookInputPressed(Vector2 direction)
	{
		if(this.GetCanLook())
		{
			Vector3 toup = new Vector3(direction.x,0 , direction.y);

			if (toup != Vector3.zero)
				facingDirection = toup.normalized;

			this.transform.rotation = Quaternion.LookRotation(facingDirection, Vector3.up);

		}
	}


	public void MoveCharacter(Vector3 direction_Ws, bool overrideRestrictions = false)
	{
		if (this.GetCanMove() || overrideRestrictions)
		{
			Vector3 direction_ls = characterRigidbody.transform.InverseTransformDirection(direction_Ws);
			Vector3 currentVelocity = characterRigidbody.velocity;

			Vector3 projectedVelocity = currentVelocity + direction_Ws * accel *Time.deltaTime;
			float actualAccel = accel*Time.deltaTime;
			if (projectedVelocity.magnitude > maxSpeed)// && Vector3.Dot(currentVelocity, direction_Ws*accel*Time.deltaTime) > 0)
			{
				actualAccel = Mathf.Max(0f, maxSpeed - currentVelocity.magnitude);
			}

			characterRigidbody.AddRelativeForce(direction_ls* actualAccel, ForceMode.VelocityChange);
			//lastInertia = direction_Ws * actualAccel;
			LookCharacter(direction_Ws);
		}
	}


	public void LookCharacter(Vector3 inputV3, bool overrideRestrictions = false)
	{
		if (this.GetCanLook() || overrideRestrictions)
		{
			if (inputV3 != Vector3.zero)
				facingDirection = inputV3.normalized;

			this.transform.rotation = Quaternion.LookRotation(facingDirection, Vector3.up);
		}
	}




	public void TryAttack()
	{
		if(CanInteract())
		{
			StartCoroutine(AttackCoroutine());
		}
		
	}

	IEnumerator AttackCoroutine()
	{
		this.currentAction = CharacterState.Attacking;
		for(int i =0; i < 30; i++)
		{
			yield return null;
		}

		this.currentAction = CharacterState.Nothing;
	}


	public void TryUseItem()
	{
		Debug.DrawRay(this.transform.position, facingDirection, Color.blue, 0.4f);
	}


	public void TryJump()
	{
		this.currentAction = CharacterState.Jumping;
	}

	public void TryDash()
	{

	}



	public bool CanInteract()
	{
		return this.currentAction == CharacterState.Nothing;
	}


	public bool GetCanMove()
	{
		return this.currentAction == CharacterState.Nothing || this.currentAction == CharacterState.Jumping;
	}

	public bool GetCanLook()
	{
		return this.currentAction == CharacterState.Nothing;
	}
}

//[System.Flags]
public enum CharacterState
{
	 Nothing = 0,
	 Jumping = 1,
	 Dodging = 2,
	 Attacking = 4,
	 Iteming = 8,
	 Cutscene = 16,
}
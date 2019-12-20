using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	public float playerMoveSpeed;
	public float playerRotateSpeed;
	public GameObject playerGraphics;

	public float jumpPower;
	public float gravityPower;
	public int totalJumpCount;

	int jumpCount;
	float jumpValue;
	
	public bool hasGlider;
	public bool isGliding;
	public float gliderFallSpeed;

	public float groundOffset;

	public LayerMask groundLayer;
	public bool grounded;

	CharacterController _cc;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
		jumpCount = totalJumpCount;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 input = new Vector3(h, 0, v);

		if(input.sqrMagnitude > 1.0f)
		{
			input = input.normalized;
		}

		input *= playerMoveSpeed;
		input.y = _cc.velocity.y;

		if(!grounded)
		{
			if(Input.GetButtonDown("Jump") && hasGlider)
			{
				input.y = 0;
				isGliding = true;
			}			
			if(isGliding){
				input.y = gliderFallSpeed;
			}
			else{
				input.y -= gravityPower;
			}
		}

		if(Input.GetButtonUp("Jump") && hasGlider)
			{
				isGliding = false;
			}

        if(grounded){
			if(Input.GetButtonDown("Jump") && jumpCount > 0)
			{
				input.y = jumpPower;
				grounded = false;
				jumpCount --;
			}		
		}

        Debug.DrawRay(transform.position, Vector3.down * groundOffset, Color.green);

        RaycastHit hit;

		//THIS IS WHERE WE HIT THE GROUND
		if(Physics.Raycast(transform.position, Vector3.down, out hit, groundOffset, groundLayer))
		{
			jumpCount = totalJumpCount;
			grounded = true;
		}
		else{
			jumpCount = 0;
			grounded = false;
		}

		_cc.Move(input * Time.deltaTime);

		if(input.x != 0 || input.z != 0){
			//SNAP ROTATION
			//transform.eulerAngles = new Vector3(0, Mathf.Atan2(h, v) * Mathf.Rad2Deg, 0);
			
			//SMOOTH ROTATION
			playerGraphics.transform.rotation = Quaternion.Slerp(playerGraphics.transform.rotation, Quaternion.LookRotation(new Vector3(input.x, 0, input.z)), playerRotateSpeed * Time.deltaTime);
		}
    }
}

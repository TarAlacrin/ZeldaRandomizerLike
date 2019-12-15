using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	public float playerMoveSpeed;
	public float playerRotateSpeed;
	public GameObject playerGraphics;

	public bool grounded;

	Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 input = new Vector3(h, _rb.velocity.y, v);

		if(input.sqrMagnitude > 1.0f)
		{
			input = input.normalized;
		}

		if(!grounded)
		{
			input.y = -9.8f;
		}

		_rb.velocity = input * playerMoveSpeed;		

		if(_rb.velocity.x != 0 || _rb.velocity.z != 0){
			//SNAP ROTATION
			//transform.eulerAngles = new Vector3(0, Mathf.Atan2(h, v) * Mathf.Rad2Deg, 0);
			
			//SMOOTH ROTATION
			playerGraphics.transform.rotation = Quaternion.Slerp(playerGraphics.transform.rotation, Quaternion.LookRotation(new Vector3(input.x, 0, input.z)), playerRotateSpeed * Time.deltaTime);
		}
    }

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Ground")
		{
			grounded = true;
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if(collision.gameObject.tag == "Ground")
		{
			grounded = false;
		}
	}
}

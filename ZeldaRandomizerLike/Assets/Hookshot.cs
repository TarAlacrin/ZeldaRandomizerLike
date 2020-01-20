using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : MonoBehaviour
{
	public bool hasFired;
	public bool isPulling;
	public GameObject hook;
	public float hookSpeed;
	public float hookDistance;
	public LayerMask hookableLayer;
	public PlayerMovement playerMovement;
	public GameObject playerGraphics;
	public float hookTimeBeforeRetract;
	public float timer;
	Vector3 startpos;
	Vector3 hitpos;

	void Start()
	{
		startpos = hook.transform.localPosition;
	}

	void LateUpdate()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (!hasFired)
			{
				FireHookshot();
			}
		}
		
		RaycastHit hit;
		
		if (hasFired)
		{
			hook.transform.Translate(Vector3.forward * hookSpeed * Time.deltaTime);
			playerMovement.hasControl = false;
			playerMovement.canJump = false;
			timer += Time.deltaTime;
			if (timer > hookTimeBeforeRetract)
			{
				if (!isPulling)
				{
					hasFired = false;
					playerMovement.hasControl = true;
					playerMovement.canJump = true;
					hook.transform.localPosition = startpos;
					timer = 0;
				}
			}
			if (Physics.Raycast(hook.transform.position, hook.transform.forward, out hit, .5f, hookableLayer))
			{
				isPulling = true;
				hitpos = hit.transform.position;
			}
		}
		if (isPulling)
		{
			playerMovement.gameObject.transform.Translate(playerGraphics.transform.forward * hookSpeed * Time.deltaTime);
			hook.transform.position = hitpos;
			if (Physics.Raycast(transform.position, transform.forward, out hit, hookDistance, hookableLayer))
			{
				isPulling = false;
				hasFired = false;
				hook.transform.localPosition = startpos;
				playerMovement.hasControl = true;
				playerMovement.canJump = true;
				timer = 0;
			}
		}



	}

	public void FireHookshot()
	{
		hasFired = true;
	}
}

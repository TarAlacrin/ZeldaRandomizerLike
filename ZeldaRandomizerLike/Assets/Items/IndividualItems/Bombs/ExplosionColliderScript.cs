﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionColliderScript : MonoBehaviour
{
	
	private void OnTriggerEnter(Collider other)
	{
		Debug.LogError("TRIGGERENTER " + other.transform.name);
		ICanBeBombed bombable = other.gameObject.GetComponentInParent<ICanBeBombed>();

		if(bombable != null)
		{
			bombable.OnExplosion(this.transform.position);
		}
	}
}

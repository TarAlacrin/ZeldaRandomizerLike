using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanBeDamaged 
{
	void DealDamage(float damageAmount, Vector3 damageOrigin);
}

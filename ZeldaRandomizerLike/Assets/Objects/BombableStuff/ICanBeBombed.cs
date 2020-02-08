using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanBeBombed 
{
	void OnExplosion(Vector3 ExplostionPosition, int explosionLevel = 1);
}

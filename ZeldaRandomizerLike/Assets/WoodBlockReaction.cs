using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlockReaction : Reaction
{
	public GameObject fireVFX;
	public GameObject smokeVFX;
	GameObject spawnedSmokeVFX;
	GameObject spawnedFireVFX;
	public GameObject graphics;
	public float lightTime;
	public float burnTime;

    public override void Start()
    {

        base.Start();
    }

    public override void React(Element.PropertyType inElementType)
	{		
		if(inElementType == Element.PropertyType.Fire)
		{
			if(active){
				spawnedSmokeVFX = Instantiate(smokeVFX, transform.position, transform.rotation) as GameObject;
				StartCoroutine(LightC());
			}
		}

		base.React(inElementType);
	}

	public override void Unreact()
	{
		StopCoroutine(LightC());
		Destroy(spawnedSmokeVFX);	
		base.Unreact();
	}

	IEnumerator LightC()
	{
		yield return new WaitForSeconds(lightTime);
		active = false;
		spawnedFireVFX = Instantiate(fireVFX, transform.position, transform.rotation) as GameObject;
		Destroy(spawnedSmokeVFX);
		StartCoroutine(BurnC());
	}

	IEnumerator BurnC()
	{
		yield return new WaitForSeconds(burnTime);
		Destroy(spawnedFireVFX);
		Destroy(gameObject);
	}
}

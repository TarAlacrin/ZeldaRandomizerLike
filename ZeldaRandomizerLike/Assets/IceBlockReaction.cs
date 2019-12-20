using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockReaction : Reaction
{
	public GameObject reactionSpawnedObject;
	public Vector3 spawnedObjectOffset;
	public GameObject graphics;
	public float meltTime;

    public override void Start()
    {

        base.Start();
    }

    public override void React(Element.PropertyType inElementType)
	{
		if(inElementType == Element.PropertyType.Fire)
		{
			if(active){
				StartCoroutine(MeltC());
			}
		}

		base.React(inElementType);
	}

	public override void Unreact()
	{
		StopAllCoroutines();
		graphics.transform.localScale = Vector3.one;
		base.Unreact();
	}

	IEnumerator MeltC()
	{
		float timer = 0;
		while(timer < meltTime)
		{
			graphics.transform.localScale -= ((1-timer) * Time.deltaTime) * Vector3.one;
			timer += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		Instantiate(reactionSpawnedObject, transform.position + spawnedObjectOffset, transform.rotation);
		Destroy(gameObject);
	}
}

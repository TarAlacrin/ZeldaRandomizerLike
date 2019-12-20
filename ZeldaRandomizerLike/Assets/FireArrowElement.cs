using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrowElement : Element
{
	public GameObject fireVFX;

    public override void Start()
    {
		base.Start();
    }

	public override void OnTriggerEnter(Collider other)
	{
		if(active){
			Instantiate(fireVFX, transform.position, transform.rotation);
		}
		base.OnTriggerEnter(other);
	}
}

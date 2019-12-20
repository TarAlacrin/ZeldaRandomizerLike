using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlockReaction : Reaction
{
    // Start is called before the first frame update
    public override void Start()
    {

        base.Start();
    }

    public override void React(Element.PropertyType inElementType)
	{
		if(inElementType == Element.PropertyType.Ice)
		{

		}
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSwitchReaction : Reaction
{
	//TODO: THIS REFERENCE SHOULD NOT BE HANDLED VIA IOC
	[Dependency]
	IManageFlags flagManager = null;

	public string flagToControl;

	private void Awake()
	{
		IOCContainer.Resolve(this);
	}

	public override void Start()
	{
		base.Start();
	}


	public override void React(Element.PropertyType inElementType)
	{
		if (inElementType == Element.PropertyType.Fire)
		{
			if (active)
			{
				flagManager.SetSwitch(flagToControl, true);
			}
		}

		base.React(inElementType);
	}

	public override void Unreact()
	{
		flagManager.SetSwitch(flagToControl, false);
		base.Unreact();
	}



}

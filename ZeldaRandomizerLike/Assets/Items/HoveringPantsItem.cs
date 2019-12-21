﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringPantsItem : UsableItemBase, IAmUsableItem
{
	string IAmUsableItem.GetItemName()
	{
		return "Angel's Trousers";
	}

	void IAmUsableItem.ItemKeyDown()
	{
		Debug.Log(((IAmUsableItem)this).GetItemName() + "DONW");
	}

	void IAmUsableItem.ItemKeyUp()
	{
		Debug.Log(((IAmUsableItem)this).GetItemName() + "KEYUP!");
	}

	bool IAmUsableItem.PlayerHasItem()
	{
		return this.playerHasItem;
	}



	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

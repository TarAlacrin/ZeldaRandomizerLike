﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsBasicItem : UsableItemBase, IAmUsableItem
{
	string IAmUsableItem.GetItemName()
	{
		return "Arrows and Bow";
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

	void IAmUsableItem.ItemNoLongerActive()
	{
		Debug.Log(((IAmUsableItem)this).GetItemName() + "INACTIVE!");
	}

	ItemEquipStyle IAmUsableItem.GetEquipStyle()
	{
		return ItemEquipStyle.Lingers;
	}
	void IAmUsableItem.UnequipItem()
	{
		Debug.Log(((IAmUsableItem)this).GetItemName() + "UNEQUIPPED!");
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

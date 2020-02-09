﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsBasicItem : UsableItemBase, IAmUsableItem
{
	[SerializeField]
	private GameObject spawnPoint = null;
	[SerializeField]
	private GameObject bombPrefabToSpawn = null;

	string IAmUsableItem.GetItemName()
	{
		return "Bombs";
	}

	void IAmUsableItem.ItemKeyDown()
	{
		Instantiate(bombPrefabToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation, null);
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
		return ItemEquipStyle.DoesNotEquip;
	}
	void IAmUsableItem.UnequipItem()
	{
		Debug.Log(((IAmUsableItem)this).GetItemName() + "UNEQUIPPED!");
	}



	UsableItemType IAmUsableItem.GetItemType()
	{
		return UsableItemType.Bombs;
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

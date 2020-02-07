using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsBasicItem : UsableItemBase, IAmUsableItem
{
	public GameObject arrow;
	public Transform firePoint;
	public GameObject bow;

	string IAmUsableItem.GetItemName()
	{
		return "Arrows and Bow";
	}

	void IAmUsableItem.ItemKeyDown()
	{
		bow.SetActive(true);
		Debug.Log(((IAmUsableItem)this).GetItemName() + "DONW");
	}

	void IAmUsableItem.ItemKeyUp()
	{
		FireAnArrow();
		Debug.Log(((IAmUsableItem)this).GetItemName() + "KEYUP!");
	}

	bool IAmUsableItem.PlayerHasItem()
	{
		return this.playerHasItem;
	}

	void IAmUsableItem.ItemNoLongerActive()
	{
		bow.SetActive(false);
		Debug.Log(((IAmUsableItem)this).GetItemName() + "INACTIVE!");
	}


	ItemEquipStyle IAmUsableItem.GetEquipStyle()
	{
		return ItemEquipStyle.Lingers;
	}
	void IAmUsableItem.UnequipItem()
	{
		bow.SetActive(false);
		Debug.Log(((IAmUsableItem)this).GetItemName() + "UNEQUIPPED!");
	}

	UsableItemType IAmUsableItem.GetItemType()
	{
		return UsableItemType.Arrows;
	}

	void FireAnArrow()
	{
		Instantiate(arrow, firePoint.transform.position, firePoint.transform.rotation);
	}

}

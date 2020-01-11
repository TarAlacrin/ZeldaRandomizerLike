using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmUsableItem 
{
	UsableItemType GetItemType();
	bool PlayerHasItem();

	string GetItemName();
	void ItemKeyDown();
	void ItemKeyUp();

	//called when another item gets used. when this was the last used item TODO: Implement
	void ItemNoLongerActive();
	
	ItemEquipStyle GetEquipStyle();
	//called when item is moved off of the hotbar. TODO: implement
	void UnequipItem();

}

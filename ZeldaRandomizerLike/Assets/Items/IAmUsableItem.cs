using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmUsableItem 
{
	bool PlayerHasItem();

	string GetItemName();
	void ItemKeyDown();
	void ItemKeyUp();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHandler : MonoBehaviour, IEquipItems, IGetEquippedItems, IServiceProvider
{
	private IAmUsableItem[] equippedItems = new IAmUsableItem[4];

	void IServiceProvider.RegisterServices()
	{
		this.RegisterService<IGetEquippedItems>();
		this.RegisterService<IEquipItems>();
	}


	void IEquipItems.EquipItem(IAmUsableItem item, int slot)
	{
		//if you already have the new item equipped to a slot, this swaps the current item equipped in the slot
		for(int i =0; i < 4; i++)
		{
			if(equippedItems[i] != null && equippedItems[i].GetItemName() == item.GetItemName())
			{
				if (i != slot)
					equippedItems[i] = equippedItems[slot];
			}
		}

		equippedItems[slot] = item;
		//UpdateEquipedVisuals();
	}

	IAmUsableItem[] IGetEquippedItems.GetEquippedItems()
	{
		return equippedItems;
	}

}

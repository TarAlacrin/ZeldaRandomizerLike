using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHandler : MonoBehaviour, IEquipItems, IGetEquippedItems, IServiceProvider
{
	private IAmUsableItem[] equippedItems = new IAmUsableItem[4];

	[Dependency]
	private IHandlePlayerControlState playerControlState;

	void Awake()
	{
		this.ResolveDependencies();
	}

	void IServiceProvider.RegisterServices()
	{
		this.RegisterService<IGetEquippedItems>();
		this.RegisterService<IEquipItems>();
	}

	void IEquipItems.EquipItem(IAmUsableItem item, int slot)
	{
		//Handles the (slot == -1) scenario: if its equal to -1 then item will get equipped to first open slot. If none are open it will default to slot 0
		if(slot ==-1)
		{
			slot = 0;
			for(int j = 0; j<4; j++)
			{
				if (equippedItems[j] == null)
				{
					slot = j;
					break;
				}
			}
		}
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
	}

	IAmUsableItem[] IGetEquippedItems.GetEquippedItems()
	{
		return equippedItems;
	}



	void Update()
	{

	}


	void HandleItemUsage()
	{
		
	}

	bool NoItemsActive()
	{
		return !(Input.GetButton("UseItem1") || Input.GetButton("UseItem2") || Input.GetButton("UseItem3") || Input.GetButton("UseItem4"));
	}



	

}

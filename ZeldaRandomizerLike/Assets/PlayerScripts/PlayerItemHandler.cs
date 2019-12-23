using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHandler : MonoBehaviour, IEquipItems, IGetEquippedItems, IServiceProvider
{
	private IAmUsableItem[] equippedItems = new IAmUsableItem[4];

	private IAmUsableItem lastActiveItem;
	private IAmUsableItem lastEquippedBoot;
	

	[Dependency]
	private IHandlePlayerControlState playerControlState = null;

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

		bool unequipOccurred = true;

		//if you already have the new item equipped to a slot, this swaps the current item equipped in the slot
		for (int i =0; i < 4; i++)
		{
			if(equippedItems[i] != null && equippedItems[i].GetItemName() == item.GetItemName())
			{
				if (i != slot)
					equippedItems[i] = equippedItems[slot];
				unequipOccurred = false;
			}
		}

		if (unequipOccurred && equippedItems[slot] == null)
			unequipOccurred = false;

		if (unequipOccurred)
			equippedItems[slot].UnequipItem();

		equippedItems[slot] = item;
	}

	IAmUsableItem[] IGetEquippedItems.GetEquippedItems()
	{
		return equippedItems;
	}



	void Update()
	{
		if (playerControlState.PlayerCanUseItems())
			HandleItemUsage();
	}


	void HandleItemUsage()
	{
		for (int i = 0; i < 4; i++)
			if (equippedItems[i] != null)
			{
				CheckAndHandleItemDown(i);
				CheckAndHandleItemUp(i);
			}
	}

	bool CheckAndHandleItemDown(int itemnum)
	{
		if (!Input.GetButtonDown("UseItem" + itemnum))
			return false;

		if (!OtherItemsAreNotDown(itemnum))
			return false;

		ItemUseButtonDown(itemnum);
		return true;
	}

	bool CheckAndHandleItemUp(int itemnum)
	{
		if (!Input.GetButtonUp("UseItem" + itemnum))
			return false;

		if (lastActiveItem != equippedItems[itemnum])
			return false;

		ItemUseButtonUp(itemnum);
		return true;
	}


	bool OtherItemsAreNotDown(int otherThanItemNum)
	{
		for (int i = 0; i < 4; i++)
			if (i != otherThanItemNum && Input.GetButton("UseItem" + i))
				return false;

		return true;
	}

	void ItemUseButtonDown(int itemNum)
	{
		if (lastActiveItem != equippedItems[itemNum] && lastActiveItem != null)
			lastActiveItem.ItemNoLongerActive();

		lastActiveItem = equippedItems[itemNum];
		equippedItems[itemNum].ItemKeyDown();
	}

	void ItemUseButtonUp (int itemNum)
	{
		equippedItems[itemNum].ItemKeyUp();
	}


}

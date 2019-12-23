using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHudManager : MonoBehaviour
{
	[Dependency]
	IGetEquippedItems playerItemHandler = null;


	IAmUsableItem[] equippedItems;

	public List<TextMeshProUGUI> EquippedItemsTMPro;

	void Awake()
	{
		this.ResolveDependencies();
	}

	void Start()
	{
		equippedItems = playerItemHandler.GetEquippedItems();
	}


	void Update()
	{
		DisplayEquippedItems();
	}


	void DisplayEquippedItems()
	{
		for(int i =0; i <4; i++)
		{
			EquippedItemsTMPro[i].text = equippedItems[i] != null ? equippedItems[i].GetItemName() : "---";
		}
	}
}

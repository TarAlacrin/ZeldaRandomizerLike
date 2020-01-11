using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControllerManager : MonoBehaviour, IServiceProvider, IGetUsableitemsFromTypes
{
	Dictionary<UsableItemType, IAmUsableItem> dictionaryOfTypes = new Dictionary<UsableItemType, IAmUsableItem>();


	void IServiceProvider.RegisterServices()
	{
		this.RegisterService<IGetUsableitemsFromTypes>();
	}

	void Awake()
	{
		EstablishItemDictionary();
	}

	void EstablishItemDictionary()
	{
		IAmUsableItem[] allusables = this.gameObject.GetComponentsInChildren<IAmUsableItem>();

		foreach (IAmUsableItem usable in allusables)
		{
			dictionaryOfTypes.Add(usable.GetItemType(), usable);
		}
	}


	IAmUsableItem IGetUsableitemsFromTypes.GetUsableItemFromType(UsableItemType type)
	{
		return dictionaryOfTypes[type];
	}

}

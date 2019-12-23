using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour, IOpenMenu, IEquipItems, IServiceProvider
{
	[Dependency]
	private IEquipItems playerItemHandler =null;

	//This doesn't get used right now. Don't know if it will get used eventually though.
	//[SerializeField]
	//private GameObject itemSelectionPrefab;


	void IServiceProvider.RegisterServices()
	{
		this.RegisterService<IOpenMenu>("ItemMenu");
	}

	private void Awake()
    {
		this.ResolveDependencies();
	}

	void IOpenMenu.CloseMenu()
	{
		this.gameObject.SetActive(false);
	}

	void IOpenMenu.OpenMenu()
	{
		this.gameObject.SetActive(true);
	}

	bool IOpenMenu.MenuIsOpen()
	{
		return this.gameObject.activeInHierarchy;
	}

	void IEquipItems.EquipItem(IAmUsableItem item, int slot)
	{
		playerItemHandler.EquipItem(item, slot);
	}

}

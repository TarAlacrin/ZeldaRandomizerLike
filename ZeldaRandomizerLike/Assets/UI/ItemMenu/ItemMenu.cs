using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour, IOpenMenu, IEquipItems
{
	[SerializeField]
	private GameObject itemSelectionPrefab;
	[SerializeField]
	private GameObject itemContollerObjectParent;

	List<ItemMenuItemSelection> itemselectionlist;

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

	// Start is called before the first frame update
	void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void IEquipItems.EquipItem(IAmUsableItem item, int slot)
	{
		Debug.Log("equipping: " + item.GetItemName() + " to slot: " + slot);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemMenuItemSelection : MonoBehaviour, IPointerEnterHandler
{
	[SerializeField]
	private TextMeshProUGUI nameText = null;
	[SerializeField]
	private Button background = null;

	//this is for an alternate way of initializing the options.
	[SerializeField]
	private GameObject itemControllerObject =null;

	[SerializeField]
	private GameObject itemMenuObject =null;
	IEquipItems itemMenu;

	private IAmUsableItem _Itemcontroller;
	public IAmUsableItem itemController {
		get
		{
			return _Itemcontroller;
		}
		set
		{
			_Itemcontroller = value;
			SetUpVisuals();
		}
	}

	IEnumerator hoverroutine;
	

	void Awake()
	{
		background.OnPointerUp(new PointerEventData(EventSystem.current));
		if (itemController == null && itemControllerObject != null)
			itemController = itemControllerObject.GetComponent<IAmUsableItem>();

		itemMenu = itemMenuObject.GetComponent<IEquipItems>();
	}

	public void OnClick()
	{
		EquipToSlot(-1, true);
	}

	public void Update()
	{
		if(EventSystem.current.currentSelectedGameObject == background.gameObject)
		{
			if (Input.GetButton("UseItem0"))
				EquipToSlot(0);
			else if (Input.GetButton("UseItem1"))
				EquipToSlot(1);
			else if (Input.GetButton("UseItem2"))
				EquipToSlot(2);
			else if (Input.GetButton("UseItem3"))
				EquipToSlot(3);
		}
	}

	void EquipToSlot(int slot, bool mouseSelected = false)
	{
		if (!mouseSelected)
		{
			background.OnPointerDown(new PointerEventData(EventSystem.current));
			StartCoroutine(FakePointer());
		}
		itemMenu.EquipItem(itemController, slot);
	}

	IEnumerator FakePointer()
	{
		//DANG THIS JANK
		yield return new WaitForSecondsRealtime(0.07f);
		background.OnPointerUp(new PointerEventData(EventSystem.current));
	}

	void SetUpVisuals()
	{
		background.interactable = itemController.PlayerHasItem();
		nameText.text = itemController.GetItemName();
	}

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		EventSystem.current.SetSelectedGameObject(background.gameObject);
	}
}

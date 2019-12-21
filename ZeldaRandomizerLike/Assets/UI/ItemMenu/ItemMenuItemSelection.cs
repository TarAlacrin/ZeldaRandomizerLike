using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemMenuItemSelection : MonoBehaviour, IPointerEnterHandler
{
	[SerializeField]
	private TextMeshProUGUI nameText;
	[SerializeField]
	private Button background;

	//this is for an alternate way of initializing the options.
	[SerializeField]
	private GameObject itemControllerObject;

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

		itemMenu = this.gameObject.GetComponentInParent<IEquipItems>();
	}

	public void OnClick()
	{
		EquipToSlot(0, true);
	}

	public void Update()
	{
		if(EventSystem.current.currentSelectedGameObject == background.gameObject)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
				EquipToSlot(0);
			else if (Input.GetKeyDown(KeyCode.Alpha2))
				EquipToSlot(1);
			else if (Input.GetKeyDown(KeyCode.Alpha3))
				EquipToSlot(2);
			else if (Input.GetKeyDown(KeyCode.Alpha4))
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

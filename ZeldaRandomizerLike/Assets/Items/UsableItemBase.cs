using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItemBase : MonoBehaviour
{
	[SerializeField]
	protected bool playerHasItem = true;
}

public enum ItemEquipStyle
{
	DoesNotEquip,
	Lingers,
	Boots,
}
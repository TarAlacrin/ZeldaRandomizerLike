using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookshotItem : UsableItemBase, IAmUsableItem
{
	public bool hasFired;
	public bool isPulling;
	public GameObject hook;
	public GameObject handleVisuals;

	public float hookSpeed;
	public float hookDistance;
	public LayerMask hookableLayer;

	[Dependency]
	private IPlayerMovement playerMovement = null;
	private Transform playerGraphics;

	public float hookTimeBeforeRetract;
	public float timer;
	Vector3 startpos;
	Vector3 hitpos;

	void Awake()
	{
		this.ResolveDependencies();
		playerGraphics = playerMovement.GetGraphicsTransform();
	}

	void Start()
	{
		startpos = hook.transform.localPosition;
	}

	void LateUpdate()
	{
		if(hasFired || isPulling)
		{
			ProcessHookshot();
		}
	}


	void ProcessHookshot()
	{
		RaycastHit hit;

		if (hasFired)
		{
			hook.transform.Translate(Vector3.forward * hookSpeed * Time.deltaTime);
			playerMovement.SetPlayerHasControl(false);
			playerMovement.SetPlayerCanJump(false);
			timer += Time.deltaTime;
			if (timer > hookTimeBeforeRetract)
			{
				if (!isPulling)
				{
					hasFired = false;
					playerMovement.SetPlayerHasControl(true);
					playerMovement.SetPlayerCanJump(true);
					hook.transform.localPosition = startpos;
					timer = 0;
				}
			}
			if (Physics.Raycast(hook.transform.position, hook.transform.forward, out hit, .5f, hookableLayer))
			{
				isPulling = true;
				hitpos = hit.transform.position;
			}
		}
		if (isPulling)
		{
			playerMovement.GetMovementTransform().transform.Translate(playerGraphics.transform.forward * hookSpeed * Time.deltaTime);
			hook.transform.position = hitpos;
			if (Physics.Raycast(this.handleVisuals.transform.position, this.handleVisuals.transform.forward, out hit, hookDistance, hookableLayer))
			{
				isPulling = false;
				hasFired = false;
				hook.transform.localPosition = startpos;
				playerMovement.SetPlayerHasControl(true);
				playerMovement.SetPlayerCanJump(true);
				timer = 0;
			}
		}
	}

	public void FireHookshot()
	{
		hasFired = true;
	}

	UsableItemType IAmUsableItem.GetItemType()
	{
		return UsableItemType.Hookshot;
	}

	bool IAmUsableItem.PlayerHasItem()
	{
		return this.playerHasItem;
	}

	string IAmUsableItem.GetItemName()
	{
		return "Hookshot";
	}

	void IAmUsableItem.ItemKeyDown()
	{
		handleVisuals.GetComponent<MeshRenderer>().enabled = true;

		if (!hasFired)
		{
			FireHookshot();
		}
	}

	void IAmUsableItem.ItemKeyUp()
	{
		//Nothing right now
	}

	void IAmUsableItem.ItemNoLongerActive()
	{
		//Nothing right now
	}

	ItemEquipStyle IAmUsableItem.GetEquipStyle()
	{
		return ItemEquipStyle.Lingers;
	}

	void IAmUsableItem.UnequipItem()
	{
		handleVisuals.GetComponent<MeshRenderer>().enabled = false;
	}
}

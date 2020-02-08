using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHandler : MonoBehaviour, IHandlePlayerControlState, IServiceProvider
{
	//TODO Integrate this with PlayerMovement
	[Dependency("ItemMenu")]
	private IOpenMenu itemMenu = null;

	bool playerMovementEnabled = true;
	bool playerOpenMenuEnabled = true;
	bool playerItemUseEnabled = true;

	void IServiceProvider.RegisterServices()
	{
		this.RegisterService<IHandlePlayerControlState>();
	}

	private void Awake()
	{
		this.ResolveDependencies();
	}


	bool IHandlePlayerControlState.PlayerCanMove()
	{
		return playerMovementEnabled && PlayerHasFullDefaultControl();
	}
	void IHandlePlayerControlState.SetPlayerCanMove(bool canMove)
	{
		playerMovementEnabled = canMove;
	}

	bool IHandlePlayerControlState.PlayerCanUseItems()
	{
		return playerItemUseEnabled && PlayerHasFullDefaultControl();
	}
	void IHandlePlayerControlState.SetPlayerCanUseItems(bool canUseItems)
	{
		playerItemUseEnabled = canUseItems;
	}

	bool IHandlePlayerControlState.PlayerCanOpenMenus()
	{
		return playerOpenMenuEnabled && PlayerHasFullDefaultControl();
	}
	void IHandlePlayerControlState.SetPlayerCanOpenMenus(bool canOpenMenus)
	{
		playerOpenMenuEnabled = canOpenMenus;
	}

	void IHandlePlayerControlState.SetPlayerCanPerformActions(bool canMoveUseItemAndOpenMenu)
	{
		playerOpenMenuEnabled = canMoveUseItemAndOpenMenu;
		playerItemUseEnabled = canMoveUseItemAndOpenMenu;
		playerMovementEnabled = canMoveUseItemAndOpenMenu;
	}


	//This will probably be added to, when the game gets more complex. Heck this whole class should probably get reworked a few times.
	private bool PlayerHasFullDefaultControl()
	{
		return !HasMenuOpen();
	}

	private bool HasMenuOpen()
	{
		return itemMenu.MenuIsOpen();
	}
}

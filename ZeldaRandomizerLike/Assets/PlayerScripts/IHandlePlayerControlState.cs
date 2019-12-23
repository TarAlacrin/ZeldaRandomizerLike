using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandlePlayerControlState
{
	bool PlayerCanMove();
	void SetPlayerCanMove(bool canMove);

	bool PlayerCanUseItems();
	void SetPlayerCanUseItems(bool canUseItems);

	bool PlayerCanOpenMenus();
	void SetPlayerCanOpenMenus(bool canOpenMenus);

	void SetPlayerCanPerformActions(bool canMoveUseItemAndOpenMenu);
}

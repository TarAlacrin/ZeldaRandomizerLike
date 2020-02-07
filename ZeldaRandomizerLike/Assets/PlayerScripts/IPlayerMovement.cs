using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMovement
{

	void SetPlayerHasControl(bool playerHasControl);
	void SetPlayerCanJump(bool canJump);
	Transform GetGraphicsTransform();
	Transform GetMovementTransform();

}

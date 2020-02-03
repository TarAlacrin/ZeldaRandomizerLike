using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetFlagManagers
{
	IManageFlags GetParentFlagManager(Transform childtransform);
	void RegisterZoneManager(IManageFlags interfaceToRegister, Transform parentTransform, bool isGlobal);
}

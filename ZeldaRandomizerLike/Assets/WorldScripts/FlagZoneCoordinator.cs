using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class coordinates the different zoneFlagManagers and switches and makes sure each switch gets matched up with its proper zonemanager
public class FlagZoneCoordinator : MonoBehaviour, IGetFlagManagers, IServiceProvider
{
	struct TransformInterfacePair
	{
		public Transform parentTrans;
		public IManageFlags flagComponent;
	}

	private List<TransformInterfacePair> zoneFlagManagers = new List<TransformInterfacePair>();
	private IManageFlags globalFlagManager;

	void IServiceProvider.RegisterServices()
	{
		this.RegisterService<IGetFlagManagers>();
	}


	IManageFlags IGetFlagManagers.GetParentFlagManager(Transform childtransform)
	{
		foreach(TransformInterfacePair tip in zoneFlagManagers)
		{
			if (childtransform.IsChildOf(tip.parentTrans))
				return tip.flagComponent;
		}

		return globalFlagManager;
	}


	void IGetFlagManagers.RegisterZoneManager(IManageFlags interfaceToRegister, Transform parentTransform, bool isGlobal)
	{

		if (!isGlobal)
		{
			TransformInterfacePair tip = new TransformInterfacePair();
			tip.parentTrans = parentTransform;
			tip.flagComponent = interfaceToRegister;
			zoneFlagManagers.Add(tip);
		}
		else
			globalFlagManager = interfaceToRegister;


	}
}



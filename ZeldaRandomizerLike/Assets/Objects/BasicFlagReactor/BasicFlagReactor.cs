using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFlagReactor : MonoBehaviour
{
	[SerializeField]
	private GameObject gameObjectEnabledWhenSwitchTrue = null;
	[SerializeField]
	private string flagToCareAbout = null;

	[Dependency]
	IGetFlagManagers flagZoneCoordinator = null;
	IManageFlags flagManager;

	private void Awake()
	{
		IOCContainer.Resolve(this);
	}

	private void Start()
	{
		flagManager = flagZoneCoordinator.GetParentFlagManager(this.transform);
	}

	private void Update()
	{
		ToggleObject();
	}

	void ToggleObject()
	{
		if (flagManager.GetSwitchBool(flagToCareAbout))
			gameObjectEnabledWhenSwitchTrue.SetActive(true);
		else
			gameObjectEnabledWhenSwitchTrue.SetActive(false);
	}
}

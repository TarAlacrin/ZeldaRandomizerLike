using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabOnFlagTrue : MonoBehaviour
{
	[SerializeField]
	private GameObject prefabToSpawn =null;


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

	bool lastFrameValue = false;

	private void Update()
	{
		CheckForObjectSpawn();
	}

	void CheckForObjectSpawn()
	{
		bool thisFrameValue = flagManager.GetSwitchBool(flagToCareAbout);

		if(thisFrameValue && !lastFrameValue)
		{
			Instantiate(prefabToSpawn, this.transform.position+Vector3.one*Random.Range(-1f, 1f), Quaternion.identity, this.transform );
		}

		lastFrameValue = thisFrameValue;
	}


}

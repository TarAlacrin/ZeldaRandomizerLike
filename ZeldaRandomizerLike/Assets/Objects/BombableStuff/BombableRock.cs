﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombableRock : MonoBehaviour, ICanBeBombed
{
	[SerializeField]
	bool saveStateToMemory = false;
	[SerializeField]
	int ExplosionLevelNeededToBreak = 1;
	[SerializeField]
	private GameObject visualsAndCollider = null;
	[Dependency]
	IGetFlagManagers flagZoneCoordinator = null;
	IManageFlags flagManager;

	[SerializeField]
	private ParticleSystem explosionSystemPrefab =null;

	private int HasBeenExploded = 0;
	
	void Awake()
	{
		this.ResolveDependencies();
	}
	// Start is called before the first frame update
	void Start()
	{
		flagManager = flagZoneCoordinator.GetParentFlagManager(this.transform);
		bool hasBeenSetAlready;

		if (saveStateToMemory)
			flagManager.RegisterNewLocalSwitch(ref HasBeenExploded, GetFlagStringName(), out hasBeenSetAlready);
		else
			hasBeenSetAlready = false;

		if (hasBeenSetAlready)
			SetState(HasBeenExploded != 1);
	}


	string GetFlagStringName()
	{
		return "BombableRock " + this.gameObject.name + Mathf.Round(this.transform.position.x) + "-" + Mathf.Round(this.transform.position.y) + "-" + Mathf.Round(this.transform.position.z);
	}


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
			Explode();
	}

	void ICanBeBombed.OnExplosion(Vector3 ExplostionPosition, int explosionLevel)
	{
		if (explosionLevel >= ExplosionLevelNeededToBreak)
			Explode();
	}

	void Explode()
	{
		ParticleSystem psystem = Instantiate(explosionSystemPrefab, this.transform.position, this.transform.rotation, this.transform.parent);
		ParticleSystemRenderer p = psystem.GetComponent<ParticleSystemRenderer>();
		psystem.transform.localScale = this.transform.localScale;
		p.material = visualsAndCollider.GetComponent<MeshRenderer>().material;
		p.material.SetFloat("_Cull", 0f);
		SetState(false);
	}

	void SetState(bool active)
	{
		visualsAndCollider.SetActive(active);
	}


}

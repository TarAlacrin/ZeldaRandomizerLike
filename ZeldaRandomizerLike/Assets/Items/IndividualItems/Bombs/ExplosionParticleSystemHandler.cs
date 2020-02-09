using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticleSystemHandler : MonoBehaviour
{
	[SerializeField]
	Collider detonationCollider = null;

	[SerializeField]
	ParticleSystemForceField forceField = null;

	[SerializeField]
	float ageToKillForceField = 0.1f;

	[SerializeField]
	float ageToDie = 3f;
	float birthday;
    // Start is called before the first frame update
    void Start()
    {
		birthday = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
		if(Time.time - ageToKillForceField > birthday)
		{
			detonationCollider.gameObject.SetActive(false);
			forceField.gameObject.SetActive(false);
		}

		if (Time.time - ageToDie > birthday)
			Destroy(this.gameObject);
    }



}

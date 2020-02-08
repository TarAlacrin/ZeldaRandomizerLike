using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDetonator : MonoBehaviour
{
	[SerializeField]
	private float fuseLength = 10f;

	[SerializeField]
	private Light glowLight;
	private float initialIntensity;

	[SerializeField]
	private ParticleSystem fuseSystem;
	ParticleSystem.MainModule mainModule;
	float spawnTime;
	// Start is called before the first frame update
	void Start()
    {
		mainModule = fuseSystem.main;
		mainModule.duration = fuseLength;
		fuseSystem.Play();
		spawnTime = Time.time; 
    }

    // Update is called once per frame
    void Update()
    {
		if(Time.time - fuseLength -0.5f > spawnTime)
		{
			Detonate();
		}
		FadeLight();

	}


	void FadeLight()
	{
		float lifeTimeLength = mainModule.startLifetime.Evaluate((Time.time-spawnTime)/fuseLength);
		lifeTimeLength *= 5f;//converts the scale from 0-0.2f to 0-1f which is what we will give the light intensity

		glowLight.intensity = lifeTimeLength * 2.5f+0.5f;
	}

	void Detonate()
	{

	}
}

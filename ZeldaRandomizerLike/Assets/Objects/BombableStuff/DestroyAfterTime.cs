using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

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
		if (Time.time - ageToDie > birthday)
			Destroy(this.gameObject);

	}
}

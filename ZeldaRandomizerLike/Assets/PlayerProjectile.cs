using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerProjectile : MonoBehaviour
{
	public float arrowSpeed;
	public float arrowArc;

	Rigidbody _rb;

	public float despawnTime;
    
    void Start()
    {
		_rb = GetComponent<Rigidbody>();
        _rb.AddRelativeForce(new Vector3(0, arrowArc, arrowSpeed));
    }

    void OnTriggerEnter(Collider other)
	{
		if(other.tag != "Player")
		{
			_rb.velocity = Vector3.zero;
			_rb.useGravity = false;
			_rb.isKinematic = true;
			StartCoroutine(Despawn());
		}
	}

	IEnumerator Despawn()
	{
		yield return new WaitForSeconds(despawnTime);
		Destroy(gameObject);
	}
}

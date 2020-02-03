using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
	public enum PropertyType { Fire, Water, Ice };
	public PropertyType propertyType;

	public bool active = true;

	virtual public void Start()
	{

	}

	virtual public void OnTriggerEnter(Collider other)
	{
		//TODO: make these interfaces. No need for direct references.
		if (other.GetComponent<Reaction>())
		{
			other.GetComponent<Reaction>().React(propertyType);
		}
		active = false;
	}

	virtual public void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<Reaction>())
		{
			other.GetComponent<Reaction>().Unreact();
		}
		active = true;
	}
}

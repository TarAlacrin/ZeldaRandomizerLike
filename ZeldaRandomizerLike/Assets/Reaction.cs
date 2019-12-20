using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{
    public enum ObjectType{Burnable, Meltable, Freezable};
	public ObjectType objectType;

	public bool active = true;

	virtual public void Start()
	{

	}

	virtual public void React(Element.PropertyType inElementType)
	{
		active = false;
	} 

	virtual public void Unreact()
	{
		active = true;
	} 
}

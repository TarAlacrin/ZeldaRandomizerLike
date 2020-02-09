using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeMesh : MonoBehaviour
{
	public List<Mesh> objectVariants;
	public bool rotation;
	public float rotationMin, rotationMax;
	public enum Axis{X, Y, Z};
	public Axis rotationAxis;
    
	public void Randomize()
	{
		int randIndex = Random.Range(0, objectVariants.Count);
		GetComponent<MeshFilter>().mesh = objectVariants[randIndex];
		if(rotation)
		{
			if(rotationAxis == Axis.X)
			{
				transform.localEulerAngles = new Vector3(Random.Range(rotationMin, rotationMax), transform.localEulerAngles.y, transform.localEulerAngles.z);
			}
			if(rotationAxis == Axis.Y)
			{
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Random.Range(rotationMin, rotationMax), transform.localEulerAngles.z);
			}
			if(rotationAxis == Axis.Z)
			{
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y,Random.Range(rotationMin, rotationMax));
			}
		}
	}
}

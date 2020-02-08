using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomizeMesh)), CanEditMultipleObjects]
public class RandomizeMeshEditor : Editor
{	

    public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		List<RandomizeMesh> randomizer = new List<RandomizeMesh>();

		foreach(Object o in targets)
		{
			randomizer.Add((RandomizeMesh)o);
		}

		if(GUILayout.Button("Randomize Mesh"))
		{			
			foreach(RandomizeMesh r in randomizer)
			{
				r.Randomize();
			}
		}
		randomizer.Clear();
	}
}

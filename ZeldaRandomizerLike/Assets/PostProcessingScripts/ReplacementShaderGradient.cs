using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplacementShaderGradient : MonoBehaviour
{
	public Shader shad;
	public Texture2D dithererr;
	public RenderTexture rentex;
	Camera cam;
    // Start is called before the first frame update
    void Start()
    {
		cam = new GameObject().AddComponent<Camera>();
		cam.gameObject.transform.parent = Camera.main.transform;
		cam.transform.localPosition = Vector3.zero;
		cam.transform.localRotation = Quaternion.identity;
		cam.enabled = false;
		Shader.SetGlobalTexture("_DitherTex", dithererr);

		//Camera.main.SetReplacementShader(shad, "RenderType");
	}

	// Update is called once per frame
	void Update()
    {
		cam.CopyFrom(Camera.main);

		cam.targetTexture = rentex;
		cam.renderingPath = RenderingPath.Forward;
		//TODO: do a bunch of the calculations here so they only have to get done once
		cam.RenderWithShader(shad, "RenderType");

	}
}

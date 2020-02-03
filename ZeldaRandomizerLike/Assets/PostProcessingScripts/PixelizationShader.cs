using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelizationShader : MonoBehaviour
{
	public Material shaderMaterial;
	public int depth = 40;


	private void Update()
	{
		Camera cam = this.gameObject.GetComponent<Camera>();
		cam.depthTextureMode = DepthTextureMode.DepthNormals;
		cam.depth = depth;
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		//this just me fiddling around with image effects really
		Graphics.Blit(source, destination, shaderMaterial);
	}
}

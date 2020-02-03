using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelizationShader : MonoBehaviour
{
	public Material shaderMaterial;


	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{

		//this just me fiddling around with image effects really
		Graphics.Blit(source, destination, shaderMaterial);
	}
}

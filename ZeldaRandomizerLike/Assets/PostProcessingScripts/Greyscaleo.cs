using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(GreyscaleoRenderer), PostProcessEvent.AfterStack, "Custom/Greyscaleo")]
public sealed class Greyscaleo : PostProcessEffectSettings
{
	[Range(1f, 4f), Tooltip("Grayscale effect intensity.")]
	public FloatParameter blend = new FloatParameter { value = 3f };
	public FloatParameter minDistance = new FloatParameter { value = 997f };
	public FloatParameter maxDistance = new FloatParameter { value = 1f };

}

public sealed class GreyscaleoRenderer : PostProcessEffectRenderer<Greyscaleo>
{

	public override DepthTextureMode GetCameraFlags()
	{
		return DepthTextureMode.Depth;
	}

	public override void Render(PostProcessRenderContext context)
	{
		var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Greyscaleo"));
		sheet.properties.SetFloat("_Blend", settings.blend);
		sheet.properties.SetFloat("_minDistance", settings.minDistance);
		sheet.properties.SetFloat("_maxDistance", settings.maxDistance);
		//Get mipmapps and chuck those in in lieu of trying to do it algorithmically
		context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
	}
}
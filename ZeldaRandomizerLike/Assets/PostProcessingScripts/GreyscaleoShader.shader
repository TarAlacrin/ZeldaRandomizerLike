Shader "Hidden/Custom/Greyscaleo"
{
	HLSLINCLUDE

		#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
	
			TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
			float _Blend;
			float _minDistance;
			float _maxDistance;
			TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);



			float4 grabAverage(float2 uv, float tileSize)
			{
				float2 pixelPosInitial = floor(uv*_ScreenParams.xy) ;/// _ScreenParams.xy

				float2 pixelPosAdjusted = pixelPosInitial - fmod(pixelPosInitial, tileSize);


				pixelPosAdjusted /= _ScreenParams.xy;
				//float2 pixelPosMin = floor(uv*_ScreenParams.xy) / _ScreenParams.xy;
				//float2 pixelPosMax = ceil(uv*_ScreenParams.xy) / _ScreenParams.xy;

				//float4 pixAverage = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(pixelPosMin.x, pixelPosMin.y));
				//pixAverage += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(pixelPosMin.x, pixelPosMax.y));
				//pixAverage += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(pixelPosMax.x, pixelPosMin.y));
				//pixAverage += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(pixelPosMax.x, pixelPosMax.y));
				return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, pixelPosAdjusted);//pixAverage *= 0.25;
			}




			float4 Frag(VaryingsDefault i) : SV_Target
			{
				float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);

				float4 depth = SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord);

				//float luminance = dot(color.rgb, float3(0.2126729, 0.7151522, 0.0721750));
				//color.rgb = lerp(color.rgb, luminance.xxx, _Blend.xxx);
				float adjustedDepth = ((1 - depth.r) - _minDistance * 0.001) / ((_maxDistance)*0.001);
				

				float tileSize = pow(floor(abs(adjustedDepth)) + 1,3);
				
					//float4(adjustedDepth, frac(adjustedDepth), frac(adjustedDepth*0.5)*2,1)
				return grabAverage(i.texcoord, tileSize); //lerp(color, , )); //depth.r*_maxDistance; //float4(_Blend + ((depth.r)*(_maxDistance)-_minDistance*.1),0,0,1);
			}
	
			ENDHLSL
	
			SubShader
			{
				Cull Off ZWrite Off ZTest Always
	
				Pass
			{
			HLSLPROGRAM
	
			#pragma vertex VertDefault
			#pragma fragment Frag

			ENDHLSL
		}
	}
}

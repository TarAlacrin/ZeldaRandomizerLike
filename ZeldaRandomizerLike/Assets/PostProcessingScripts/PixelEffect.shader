Shader "Hidden/BWDiffuse" 
{
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_bwBlend("Black & White blend", Range(0, 1)) = 0
		_dimension("Width of each square", Float) = 2
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "RGBConverter.cginc"
			 
			uniform sampler2D _MainTex;
			uniform float _bwBlend;
			uniform int _dimension;

			float4 grabAverage(float2 uv)
			{
				float2 pixelPosMin = floor(uv*_ScreenParams.xy)/_ScreenParams.xy;
				float2 pixelPosMax = ceil(uv*_ScreenParams.xy)/_ScreenParams.xy;

				float4 pixAverage = tex2D(_MainTex, float2(pixelPosMin.x, pixelPosMin.y));
				pixAverage += tex2D(_MainTex, float2(pixelPosMin.x, pixelPosMax.y));
				pixAverage += tex2D(_MainTex, float2(pixelPosMax.x, pixelPosMin.y));
				pixAverage += tex2D(_MainTex, float2(pixelPosMax.x, pixelPosMax.y));
				return pixAverage *= 0.25;
			}

			float4 ColorToHueIndex(float4 RGB)
			{
				return float4(RGBtoHSV(RGB.rgb), RGB.a);
			}

			float4 HueIndexToColor(float4 HHHA)
			{
				return float4(HSVtoRGB(HHHA.rgb), HHHA.a);
			}

			float4 organizeAverage(float2 uv)
			{
				 
				float2 pixelPos = floor(uv*_ScreenParams.xy);

				float2 positionInSquare = floor(fmod(pixelPos, 2));
				pixelPos -= positionInSquare; 

				float2 uvPosMin = pixelPos / _ScreenParams.xy;
				float2 uvPosMax = (pixelPos+1) / _ScreenParams.xy;


				


				float4x4 pixelPlots;

				/*float4 pixLB*/pixelPlots[0] = ColorToHueIndex(tex2D(_MainTex, float2(uvPosMin.x, uvPosMin.y)));
				/*float4 pixLT*/pixelPlots[1] = ColorToHueIndex(tex2D(_MainTex, float2(uvPosMin.x, uvPosMax.y)));
				/*float4 pixRB*/pixelPlots[2] = ColorToHueIndex(tex2D(_MainTex, float2(uvPosMax.x, uvPosMin.y)));
				/*float4 pixRT*/pixelPlots[3] = ColorToHueIndex(tex2D(_MainTex, float2(uvPosMax.x, uvPosMax.y)));

				

				//return pixelPlots[positionInSquare.x*2+ positionInSquare.y];
			
				return HueIndexToColor(pixelPlots[(positionInSquare.x) * 2 + (1-positionInSquare.y)]);
			}



			float HueBlocker(float Hue)
			{
				return round(Hue * 16)/16;
			} 

			float SatBlocker(float Sat)
			{
				return round(Sat * 3)*0.3;
			}

			
			float LumBlocker(float Lum)
			{
				return round(Lum *10)/16;
			} 


			float3 HueIndexAverager(float3 HSB)
			{
				return float3(HueBlocker(HSB.x), SatBlocker(HSB.y), LumBlocker(HSB.z));
			}




			float4 averageColorSpace(float2 uv)
			{
				float4 hueIndex = ColorToHueIndex(tex2D(_MainTex, uv));

				hueIndex = float4(HueIndexAverager(hueIndex.rgb), hueIndex.a);

				return HueIndexToColor(hueIndex);
			}


			float4 frag(v2f_img i) : COLOR
			{
				float4 c = tex2D(_MainTex, i.uv);

				//float lum = c.r*.3 + c.g*.59 + c.b*.11;
				//float3 bw = float3(lum, lum, lum);

				//float4 result = c;
				//result.rgb = lerp(c.rgb, bw, _bwBlend);



				return lerp(c, averageColorSpace(i.uv), _bwBlend);
			}
			ENDCG
		}
	}
}
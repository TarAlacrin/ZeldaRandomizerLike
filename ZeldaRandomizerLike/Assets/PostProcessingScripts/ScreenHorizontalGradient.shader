// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/ScreenHorizontalGradient"
{
    Properties
    {
		_Dimensions("x = height ForCalc, z = minIntensityValue, w = intensityGradientSize", Vector) = (0.5, 0.5,0.0,0.75)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque"  }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
			 sampler2D _DitherTex;
	float4 _DitherTex_ST;
	float4 _Dimensions;

            struct appdata
            {
                float4 vertex : POSITION;
				float2 uv : TEXCOORD0;

            };

            struct v2f
            {
				float4 cameraForwardAndD3 :TEXCOORD0;
				float4 worldPosAndD1 :TEXCOORD2;
                UNITY_FOG_COORDS(1)
                float4 pos : SV_POSITION;
				float2 uv : TEXCOORD3;

			};



			float getfov()
			{
				float t = unity_CameraProjection._m11;
				const float Rad2Deg = 180 / UNITY_PI;
				float fov = atan(1.0f / t);
				return fov;
			}


			float GetGradientFromPosition(float3 wsPos, float D3dist, float d1dist, float3 camForward)
			{
				float2 HOffset = wsPos.xz - _WorldSpaceCameraPos.xz;
				float dotProjDist = abs(dot(HOffset, normalize(camForward.xz)));
				return (dotProjDist - d1dist) / D3dist;
			}

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _DitherTex);

				float3 vertWPos = mul(unity_ObjectToWorld, v.vertex).xyz;

				float3 camforward = mul((float3x3)unity_CameraToWorld, float3(0, 0, 1));
				float camAng = abs(atan(length(camforward.xz) / camforward.y));
				float fovAng = getfov();

				float camOffset = 28;//_Dimensions.x;
				//camOffset = _WorldSpaceCameraPos.y;//TODO: FIX 

				float d1 = tan(camAng - fovAng)*camOffset;
				float d2 = tan(camAng + fovAng)*camOffset;

				float D3 = d2-d1;


				o.cameraForwardAndD3 = float4(camforward.x, camforward.y, camforward.z, D3);
				o.worldPosAndD1 = float4(vertWPos.x, vertWPos.y, vertWPos.z, d1);

                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

			float extrabanding(float inf, float extrabandingAmount)
			{
				extrabandingAmount += 256;
				inf *= extrabandingAmount;
				inf = round(inf)/extrabandingAmount;
				return inf;
			}

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
				float scale  = GetGradientFromPosition(i.worldPosAndD1.xyz, i.cameraForwardAndD3.w, i.worldPosAndD1.w, i.cameraForwardAndD3.xyz);
			

				float dimw = _Dimensions.w;
				float dimz = _Dimensions.z;
				dimz = .1;
				dimw = .7;

				scale *= dimw *(1 - dimz);
				scale += dimz;

				//scale = pow(scale, 2.23);

				//float ditherVal = tex2D(_DitherTex, i.pos.xy*0.04)*0.0045;

				scale = pow(scale, 2.23);
				//secondary dither adds in slashes

				float timedithersig = 0.7;
				float timedither = tex2D(_DitherTex, i.pos.xy*0.02)*(timedithersig)+1-timedithersig;


				float secondarydither = tex2D(_DitherTex, i.pos.xy*0.01);

				float timemod = cos(_Time.x)*0.5 + 0.5;
				timemod = 1 - timedither * timemod;

				secondarydither = pow(secondarydither, 5.0)*2.0*timemod+_Time.x*0.1;

				float ditherVal = tex2D(_DitherTex, i.pos.xy*0.045+secondarydither)*0.0045;
				scale += ditherVal;

				fixed4 col = scale;

				col.a = 1;
                // apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}

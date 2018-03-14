Shader "leesue/Color"
{
	Properties
	{
		_MainColor ("Texture", Color) = (1,1,1,1)
		_HightestbBrightness ("Hightestb Brightness", Range(-1, 1)) = 1
		_LowestBrightness ("Lowest Brightness", Range(-1, 1)) = 0
		// [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("Src Blend Mode", Float) = 1
		[Toggle]_UseMeshColor("use mesh's color", Int) = 0
		[Toggle]_LittleShadow("little shadow", Int) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Cull off

		Pass
		{
			CGPROGRAM
			#pragma shader_feature _USEMESHCOLOR_ON 
			#pragma shader_feature _LITTLESHADOW_ON
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			// #pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct mAppdata
			{
				float4 vertex : POSITION;
				// float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
                float4 normal : NORMAL;
			};

			struct v2f
			{
				// float2 uv : TEXCOORD0;
				// UNITY_FOG_COORDS(1)
				float4 pos : SV_POSITION;
				float3 normal : NORMAL;
				fixed4 color : TEXCOORD0;
				float4 vertex : TEXCOORD1;
			};

			fixed4 _MainColor; 
			// float4 _MainColor_ST;
			float _HightestbBrightness;
			float _LowestBrightness;
			
			v2f vert (mAppdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.vertex = v.vertex;
				o.color = v.color; 
				o.normal = v.normal;
				// o.uv = TRANSFORM_TEX(v.uv, _MainColor);
				// UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				// fixed4 col = tex2D(_MainColor, i.uv);
				// apply fog
				// UNITY_APPLY_FOG(i.fogCoord, col);
				// return i.color;
                fixed4 col;
#if _USEMESHCOLOR_ON
				col = i.color;
#else
				col = _MainColor;
#endif

				float light = 1;
#if _LITTLESHADOW_ON
				fixed3 objectLightDir = normalize(ObjSpaceLightDir(i.vertex));
				// objectLightDir = normalize(_WorldSpaceLightPos0.xyz);
				// float light = dot(normalize(i.vertex.xyz - _WorldSpaceCameraPos.xyz), normalize(i.normal.xyz));
				light = dot(normalize(objectLightDir), normalize(i.normal));
#endif
				light = (light - -1) / 2 * (_HightestbBrightness - _LowestBrightness) + _LowestBrightness;
					// col = mul(col, light);
				col = col * light;
					return col;
			}
			ENDCG
		}
	}
}

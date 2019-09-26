Shader "Custom/Alpha"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_Glossiness("Smoothness", Range(0.0,1.0)) = 0.5
		[Gamma] _Metallic("Metallic", Range(0.0,1.0)) = 0.0
		[HideInInspector] _MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard

		half4 _Color;
	sampler3D _DitherMaskLOD;

		struct Input
		{
			float2 uv_MainTex;
			float3 worldPos;
			float4 screenPos;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			half alphaRef = tex3D(_DitherMaskLOD, float3(IN.screenPos.xy / IN.screenPos.w * _ScreenParams.xy * 0.25, _Color.a*0.9375)).a;
			clip(alphaRef - 0.01);

			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG

		pass
		{
			Name "ShadowCaster"
			Tags{"LightMode" = "ShadowCaster"}

			ZWrite On ZTest LEqual

			CGPROGRAM
			#pragma target 3.0

			#pragma vertex vert
			#pragma fragment frag

			#define UNITY_STANDARD_USE_SHADOW_OUTPUT_STRUCT
			#define UNITY_STANDARD_USE_DITHER_MASK
			#define UNITY_STANDARD_USE_SHADOW_UVS

			#include "UnityStandardShadow.cginc"

			struct VertexOutput
			{
				V2F_SHADOW_CASTER_NOPOS
				float2 tex : TEXCOORD1;
			};

			void vert(VertexInput v, out VertexOutput o, out float4 opos : SV_POSITION)
			{
				TRANSFER_SHADOW_CASTER_NOPOS(o, opos)
					o.tex = v.uv0;
			}

			half4 frag(VertexOutput i, UNITY_VPOS_TYPE vpos : VPOS) : SV_Target
			{
				half alphaRef = tex3D(_DitherMaskLOD, float3(vpos.xy*0.25, _Color.a*0.9375)).a;
				clip(alphaRef - 0.01);

				SHADOW_CASTER_FRAGMENT(i)
			}

			ENDCG
		}
	}
		FallBack "Differd"
}

Shader "Custom/CutTexture" {

	Properties{
		_MainTex ("_MainTex", 2D) = "white" {}
		_CutSize ("Cut on left and right from original texture", Range (0, 1)) = 0
	}
	SubShader{

		Pass {
			ZTest Always Cull Off ZWrite Off Fog{ Mode Off }
			CGPROGRAM

			#pragma target 3.0

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct Interpolators {
				float4 position : SV_POSITION;
				float2 Main_uv : TEXCOORD0;
			};

			struct VertexData {
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _CutSize;

			Interpolators vert (VertexData v) {
				Interpolators i;
				i.position = UnityObjectToClipPos(v.position);

				_MainTex_ST.x = 1.0 - 2.0 * _CutSize;
				_MainTex_ST.z += _CutSize;
				i.Main_uv = TRANSFORM_TEX(v.uv, _MainTex);

				return i;
			}

			float4 frag (Interpolators i) : SV_TARGET {
				return tex2D(_MainTex, i.Main_uv);
			}

			ENDCG
		}
	}
}
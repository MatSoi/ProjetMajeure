Shader "Custom/BarilletShader" {
//Code inspiré par le code de Mr Odin.

	Properties{
		_MainTex ("_MainTex", 2D) = "white" {}
		_BarrelDistortion1 ("First barrel distorsion coeff", Range (0, 1)) = 0
		_BarrelDistortion2 ("First barrel distorsion coeff", Range (0, 1)) = 0
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
				float2 uv : TEXCOORD0;
			};

			struct VertexData {
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _BarrelDistortion1;
			float _BarrelDistortion2;
			float _CutSize;

			Interpolators vert (VertexData v) {
				Interpolators i;

				i.position = UnityObjectToClipPos(v.position);

				//i.uv = TRANSFORM_TEX(v.uv, _MainTex);
				i.uv = float2((1.0 + i.position.x) / 2.0, (1.0 + i.position.y) / 2.0);

				return i;
			}

			float2 brownConradyDistortion(float2 uv)
		    {
		    // positive values of K1 give barrel distortion, negative give pincushion
		    float r2 = uv.x*uv.x + uv.y*uv.y;
		    uv *= 1.0 + _BarrelDistortion1 * r2 + _BarrelDistortion2 * r2 * r2;
		   
		    // tangential distortion (due to off center lens elements)
		    // is not modeled in this function, but if it was, the terms would go here
		    return uv;
		    }


			float4 frag (Interpolators i) : SV_TARGET {

				float2 uv = i.uv; // 0 -> 1
			    uv = uv * 2.0 - 1.0; // -1 -> 1
			    if (i.uv.x > 0.5)
			    	uv.x = uv.x * 2.0 - 1.0; // 0->1 to -1->1
			    else
			    	uv.x = uv.x * 2.0 + 1.0; // -1->0 to -1->1

			    uv = brownConradyDistortion(uv);
			  
			    if (abs(uv.x) >= 1.0 || abs(uv.y) >= 1.0) {return float4(0.0,0.0,0.0,0.0);}
			    if (i.uv.x > 0.5)
			    	uv.x = (uv.x + 1.0) / 2.0;// -1->1 to 0->1
			    else
			    	uv.x = (uv.x - 1.0) / 2.0;// -1->1 to -1->0
			    uv = uv * 0.5 + 0.5;

			    //Cutting indesirable space on texture to get asymetric frustrum
			    _MainTex_ST.x = 1.0 - 2.0 * _CutSize;
				_MainTex_ST.z += _CutSize;
				uv = uv * _MainTex_ST.x + _MainTex_ST.z;

			    fixed4 tex = tex2D(_MainTex, uv);
			    float4 col;
			    col.rgb = tex.rgb;

				return col;
			}

			ENDCG
		}
	}
}
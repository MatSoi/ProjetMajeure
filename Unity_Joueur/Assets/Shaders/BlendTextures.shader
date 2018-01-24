// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/BlendTextures" {

	Properties{
		_MainTexLeft ("_MainTexLeft", 2D) = "white" {}
		_BlendTexLeft ("_BlendTexLeft", 2D) = "white" {}
		_MainTexRight ("_MainTexRight", 2D) = "white" {}
		_BlendTexRight ("_BlendTexRight", 2D) = "white" {}
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
				float2 MainLeft_uv : TEXCOORD0;
				float2 MainRight_uv : TEXCOORD1;
				float2 BlendLeft_uv : TEXCOORD2;
				float2 BlendRight_uv : TEXCOORD3;
			};

			struct VertexData {
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTexLeft;
       	 	sampler2D _BlendTexLeft;
        	sampler2D _MainTexRight;
        	sampler2D _BlendTexRight;
			float4 _MainTexLeft_ST;
			float4 _BlendTexLeft_ST;
			float4 _MainTexRight_ST;
			float4 _BlendTexRight_ST;


			Interpolators vert (VertexData v) {
				Interpolators i;
				i.position = UnityObjectToClipPos(v.position);

				//Modification des coordonnées de textures pour former une texture correspondant à 2 images:
				//Chaque texture est répété 2 fois selon les X

				_MainTexLeft_ST.x *= 2.0;
				i.MainLeft_uv = TRANSFORM_TEX(v.uv, _MainTexLeft);

				_MainTexRight_ST.x *= 2.0;
				i.MainRight_uv = TRANSFORM_TEX(v.uv, _MainTexRight);

				_BlendTexLeft_ST.x *= 2.0;
				i.BlendLeft_uv = TRANSFORM_TEX(v.uv, _BlendTexLeft);

				_BlendTexRight_ST.x *= 2.0;
				i.BlendRight_uv = TRANSFORM_TEX(v.uv, _BlendTexRight);

				return i;
			}

			float4 frag (Interpolators i) : SV_TARGET {
				float4 col;
				if (i.MainLeft_uv.x < 1)//Si dans la première partie de la texture (uv selon x < 1), alors on mélange texture virtuelle et réelle de la caméra de gauche
				{
				fixed4 backgroundTex = tex2D(_MainTexLeft, i.MainLeft_uv);
     			fixed4 sceneTex = tex2D(_BlendTexLeft, i.BlendLeft_uv);                           
      
      			fixed4 backgroundOutput = backgroundTex.rgba * (1.0 - (sceneTex.a));
      			fixed4 blendOutput = sceneTex.rgba * sceneTex.a;

      			col.rgb = backgroundOutput.rgb + blendOutput.rgb;//Mélange des textures par simple addition
      			col.a = backgroundOutput.a;
				}
				if (i.MainRight_uv.x > 1)//Si dans la première partie de la texture (uv selon x > 1), alors on mélange texture virtuelle et réelle de la caméra de droite
				{
				i.MainRight_uv.x -= 1.0;//décale la texture selon x pour obtenir la bonne image
				fixed4 backgroundTex = tex2D(_MainTexRight, i.MainRight_uv);
     			fixed4 sceneTex = tex2D(_BlendTexRight, i.BlendRight_uv);                           
      
      			fixed4 backgroundOutput = backgroundTex.rgba * (1.0 - (sceneTex.a));
      			fixed4 blendOutput = sceneTex.rgba * sceneTex.a;

      			col.rgb = backgroundOutput.rgb + blendOutput.rgb;//Mélange des textures par simple addition
      			col.a = backgroundOutput.a;
				}
				return col;
			}

			ENDCG
		}
	}
}
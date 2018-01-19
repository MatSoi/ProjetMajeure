Shader "Custom/Blend2Textures" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BlendTex ("_BlendTex", 2D) = "white" {}
        _Blend1 ("Blend between Base and Blend 1 textures", Range (0, 1) ) = 0 
	}
	SubShader {
		ZTest Always Cull Off ZWrite Off Fog{ Mode Off }
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
        sampler2D _BlendTex;
        float _Blend1;

		struct Input {
			float2 uv_MainTex;
			float4 screenPos;
		};

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		//UNITY_INSTANCING_CBUFFER_START(Props)
		// put more per-instance properties here
		//UNITY_INSTANCING_CBUFFER_END

		 //Barillet deformation
		 float barrelPower = 1.2;
  
		 float2 Distort(float2 p)
			{
			  float theta = atan2(p.y,p.x);
			  float radius = length(p);
			  radius = pow(radius,barrelPower);
			  p.x = radius*cos(theta);
			  p.y = radius*sin(theta);
			   return 0.5*(p+1.0);
			}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			//Deformation de barrillet
//			float2 p = float2(0.0,0.0);
//			if(IN.screenPos.x<=_ScreenParams.x/2.0)
//			{
//			p = float2(2.0*(IN.screenPos.x/(_ScreenParams.x/2.0))-1.0, 
//						1.0-2.0*(IN.screenPos.y/(_ScreenParams.y)));
//			}
//			else
//			{
//			p = float2(2.0*((IN.screenPos.x-( _ScreenParams.x/2.0))/( _ScreenParams.x/2.0))-1.0,
//			             1.0-2.0*(IN.screenPos.y/(_ScreenParams.y))
//			             );
//			}
//
//			p = Distort(p);
//			IN.uv_MainTex = p;
			
		    fixed4 backgroundTex = tex2D(_MainTex, IN.uv_MainTex);
     		fixed4 sceneTex = tex2D(_BlendTex, IN.uv_MainTex);                           
      
      		fixed4 backgroundOutput = backgroundTex.rgba * (1.0 - (sceneTex.a * _Blend1));
      		fixed4 blendOutput = sceneTex.rgba * sceneTex.a * _Blend1;

      		o.Albedo = backgroundOutput.rgb + blendOutput.rgb;
		}
		ENDCG
	}

	FallBack "Diffuse"
}

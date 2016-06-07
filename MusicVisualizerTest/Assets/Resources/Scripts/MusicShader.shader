Shader "Custom/MusicShader"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_HeightMin("Height Min", Float) = -1
		_HeightMax("Height Max", Float) = 1
		_ColorMin("Tint Color At Min", Color) = (0,0,0,1)
		_ColorMax("Tint Color At Max", Color) = (1,1,1,1)
		_ColorThree("GHFHFH", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque" }

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed4 _ColorMin;
		fixed4 _ColorMax;
		fixed4 _ColorThree;
		float _HeightMin;
		float _HeightMax;

		struct Input
		{
			float2 uv_MainTex;
			float3 worldPos;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			float h = (_HeightMax - IN.worldPos.y) / (_HeightMax - _HeightMin);
			fixed4 tintColor1 = lerp(_ColorMin, _ColorMax, h);
			fixed4 tintColor2 = lerp(_ColorMax, _ColorThree, h);
			fixed4 tintColor3 = lerp(_ColorThree, _ColorMin, h);
			o.Albedo = c.rgb * tintColor1.rgb * tintColor2.rgb;
			o.Alpha = c.a * tintColor1.rgb * tintColor2.rgb;
		}
		ENDCG
	}

	Fallback "Diffuse"
}
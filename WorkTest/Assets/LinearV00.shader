Shader "GameStudio00/Skybox/LinearV00" {
	Properties{
		_Color1("Color 1", Color) = ( 0.52,	0.98,	1.0, 1 )
		_Color2("Color 2", Color) = ( 0.0,	0.494,	1.0, 1 )
		_Scale("Scale", Float) = 1.15
	}
	SubShader
	{
		Cull Off
		ZWrite Off

		Tags
		{
			"Queue" = "Geometry"
			"PreviewType" = "Skybox"
		}
		

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			



			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				float4 position : TEXCOORD0;
			};

			float4 _Color1;
			float4 _Color2;
			float  _Scale;


			v2f vert( appdata v )
			{
				v2f o;
				o.vertex = o.position = UnityObjectToClipPos( v.vertex );
			
				return o;
			}


			fixed4 frag( v2f i ) : SV_Target
			{
				fixed4 col = lerp( _Color1, _Color2, clamp( _Scale * (i.position.y/i.position.w + 1.0f )/2.0f, 0.0f, 1.0f )  );
			
				return col;
			}
			ENDCG
		}
	}
}

Shader "MyShader/DrawShader"
{
	Properties
	{
		[NoScaleOffset] _MainTex("Texture", 2D) = "gray" {}
		_Color("Color", Color) = (1,1,1,1)
		_Pos("Position", Vector) = (0,0,0,0)
		_Radius("Radius",Range(0,1)) = 0
		_Glow("Glow",Range(0,2)) = 0.3
	}

		SubShader
		{
			ZTest Always Cull Off ZWrite Off
			Fog{ Mode off }

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				sampler2D _MainTex;
				fixed4 _Pos;
				fixed4 _Color;
				fixed _Radius;
				fixed _Glow;
				
				fixed segment(fixed2 p, fixed2 a, fixed2 b) {
					fixed2 ab = b - a, ap = p - a;
					return length(ap - ab * clamp(dot(ab, ap) / dot(ab, ab), 0.0, 1.0));
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed dist = segment(i.uv, _Pos.zw, _Pos.xy);
					fixed4 newColor = (1 - smoothstep(_Radius * (1 - _Glow), _Radius, dist));
					fixed4 col = tex2D(_MainTex, i.uv);
					return col + newColor;
				}
				ENDCG
			}
		}
}

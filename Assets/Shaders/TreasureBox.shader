Shader "Custom/TreasureBox" {
    Properties {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _CoatingTex ("Coating Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _alpha ("Cotaing Alpha", Range(0,1)) = 0.8
        _speedX ("Scroll X Amplitude", Float) = 0.1
        _speedY ("Scroll Speed Y", Float) = 0.1
    }

    SubShader{
        Tags { 
            "Queue"="Transparent" 
            "RenderType"="Transparent" 
        }

        Blend One OneMinusSrcAlpha

        Pass {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            struct VertexInput {
                float4 pos	:	POSITION;    // 3D座標
                float4 color:	COLOR;
                float2 uv	:	TEXCOORD0;   // テクスチャ座標
            };

            struct VertexOutput {
                float4 v	:	SV_POSITION; // 2D座標
                float4 color:	COLOR;	
                float2 uv	:   TEXCOORD0;   // テクスチャ座標
            };

            float4 _Color;
            float _alpha;
            float _speedX, _speedY;
            sampler2D _MainTex, _CoatingTex;

            VertexOutput vert (VertexInput input) {
            	//テクスチャごとにUV座標をいじるため頂点シェーダーでは座標変換のみ
            	VertexOutput output;
            	output.v = UnityObjectToClipPos(input.pos);
            	output.uv = input.uv;
            	output.color = input.color * _Color;

            	return output;
            }

            float4 frag (VertexOutput output) : SV_Target {
            	float4 baseColor = tex2D(_MainTex, output.uv) * output.color;

            	//UVスクロール
            	output.uv.x += _SinTime.w * _speedX;
            	output.uv.y += _speedY * _Time.y;
            	float4 mixColor = tex2D(_CoatingTex, output.uv) * output.color;

            	//通常合成
                return mixColor * _alpha + baseColor * (1-_alpha);
            }
        ENDCG
        }

    }
}
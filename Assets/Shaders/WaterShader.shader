Shader "Custom/WaterShaderWithVoronoiDistortionAndManualDirection"
{
    Properties
    {
        _Color ("Water Color", Color) = (0.0, 0.5, 1.0, 1.0) // Base water color
        _MainTex ("Water Texture", 2D) = "white" {} // Tiling water texture
        _VoronoiTex ("Voronoi Texture", 2D) = "white" {} // Voronoi texture for distortion
        _WaveSpeed ("Wave Speed", Range(0, 1)) = 0.1 // Speed of the wave movement
        _WaveStrength ("Wave Strength", Range(0, 1)) = 0.2 // Strength of the Voronoi distortion
        _WaveDirection ("Wave Direction", Vector) = (1.0, 0.0, 1.0, 1.0) // Manual wave direction as a Vector
        _OffsetSpeed ("Offset Speed", Range(0, 1)) = 0.1 // Speed of the offset movement
        _OffsetDirection ("Offset Direction", Vector) = (1.0, 0.0, 0.0, 0.0) // Direction of the offset movement
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 200

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

            // Texture samplers
            sampler2D _MainTex;
            sampler2D _VoronoiTex;

            // Properties
            float4 _Color;
            float _WaveSpeed;
            float _WaveStrength;
            float2 _WaveDirection; // Manual wave direction
            float _OffsetSpeed; // Speed of the offset movement
            float2 _OffsetDirection; // Direction of the offset movement

            float4 _MainTex_ST; // Tiling and offset for the main texture

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Apply tiling to the main texture's UVs using _MainTex_ST
                float2 uv = TRANSFORM_TEX(v.uv, _MainTex);

                // Apply offset scaling based on _OffsetSpeed and _OffsetDirection
                float2 offset = _OffsetDirection * (_OffsetSpeed * _Time.y);
                o.uv = uv + offset;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Animate UVs based on wave speed and manual direction
                float2 uvDistortion = i.uv + normalize(_WaveDirection) * (_WaveSpeed * _Time.y);

                // Sample the Voronoi texture (used for distortion)
                float3 voronoiTex = tex2D(_VoronoiTex, uvDistortion).rgb;

                // Apply Voronoi distortion to the UVs
                float2 distortedUV = i.uv + (voronoiTex.rg - 0.5) * _WaveStrength;

                // Sample the main water texture with the distorted UVs
                float3 mainTex = tex2D(_MainTex, distortedUV).rgb;

                // Combine color and main texture (multiplying RGB values and keeping the alpha from _Color)
                float4 waterColor = float4(_Color.rgb * mainTex, _Color.a);

                return waterColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
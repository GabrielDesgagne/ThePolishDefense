Shader "Custom/bumpMap"
{
    Properties{
        _MyDiffuse("Diffuse Texture", 2D) = "White" {}
        _MyBump("Bump Texture", 2D) = "bump" {}
        _MySlider("Bump Amount", Range(0,10)) = 1
        _MyBright("Brightness", Range(0,10)) = 1
        _MyCube("Cube Map", CUBE) = "White" {}
    }
    
    SubShader
    {
        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MyDiffuse;
        sampler2D _MyBump;
        half _MySlider;
        half _MyBright;
        samplerCUBE _MyCube;

        struct Input{
            float2 uv_MyDiffuse;
            float2 uv_MyBump;
            float3 worldRefl; INTERNAL_DATA
        };

        void surf(Input IN, inout SurfaceOutput o){
            o.Albedo = tex2D(_MyDiffuse, IN.uv_MyDiffuse).rgb;
            o.Emission = texCUBE (_MyCube, WorldReflectionVector (IN, o.Normal)).rgb;
            o.Normal = UnpackNormal(tex2D(_MyBump, IN.uv_MyBump)) * _MyBright;
            o.Normal *= float3(_MySlider,_MySlider,1);
            
        }

        ENDCG
    }
    FallBack "Diffuse"
}

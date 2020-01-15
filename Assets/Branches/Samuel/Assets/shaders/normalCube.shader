Shader "Custom/normalCube"
{
    Properties
    {
        _MyNormal("Bump Texture",2D) = "bump" {}
        
        _MyCube("Cube Map", CUBE) = "White" {}
    }
    SubShader
    {

        ZWrite on        
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert
        sampler2D _MyNormal;
        samplerCUBE _MyCube;
        struct Input{
            float2 uv_MyBump;
            float3 worldRefl; INTERNAL_DATA
        };
        void surf(Input IN, inout SurfaceOutput o){
            o.Albedo = texCUBE (_MyCube, WorldReflectionVector (IN, o.Normal)).rgb;
            o.Normal = UnpackNormal(tex2D(_MyNormal, IN.uv_MyBump)) * 0.3;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

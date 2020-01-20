Shader "Custom/PhongLighting"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _SpecColor("Color", Color) = (1,1,1,1)
        _Spec("Specular", Range(0,1)) = 0.5
        _Gloss("Gloss", Range(0,1)) = 0.5
    }
    SubShader
    {
      Tags{
          "Queue" = "Geometry"
      }

        CGPROGRAM
       #pragma surface surf Lambert

       float4 _Color;
       fixed _Spec;
       fixed _Gloss;

       struct Input{
           float2 uv_MainTex;
       };

       void surf(Input IN, inout SurfaceOutput o){
           o.Albedo = _Color.rgb;
       }
       
        ENDCG
    }
    FallBack "Diffuse"
}

Shader "Custom/SamLighting"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _RampTex("Ramp Texture", 2D) = "White"{}
    }
    SubShader
    {
        Tags{
            "Queue" = "Geometry"
        }
        CGPROGRAM
        #pragma surface surf SamLighting
        float4 _Color;
        sampler2D _RampTex;

        half4 LightingSamLighting(SurfaceOutput s, fixed3 lightDir, fixed atten){
            half diff = dot (s.Normal, lightDir);
            float h = diff * 0.5 + 0.5;
            float2 rh = h;
            float3 ramp = tex2D(_RampTex, rh).rgb;

            float4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * (ramp);
            c.a = s.Alpha;
            return c;
        }

        
        struct Input{
            float2 uv_MainTex;
            float3 viewDir;
        };

        void surf(Input IN, inout SurfaceOutput o){
            float diff = dot (o.Normal, IN.viewDir);
            float h = diff * 0.5+0.5;
            float2 rh = h;
            o.Albedo = _Color.rgb; //tex2D(_RampTex, rh).rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

Shader "Unlit/FOVMask"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "geometry-500"}
        ColorMask 0
        ZWrite off
        Stencil {
            Ref 1
            Pass replace
        }
        Pass
        {
        }
    }
}

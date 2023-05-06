// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/VertexPaintShader"
{
	Properties
	{
		_Albedo1("Albedo1", 2D) = "white" {}
		_Normal2("Normal2", 2D) = "white" {}
		_Albedo2("Albedo2", 2D) = "white" {}
		_Normal1("Normal1", 2D) = "white" {}
		_ORM1("ORM1", 2D) = "white" {}
		_ORM2("ORM2", 2D) = "white" {}
		_metalic("metalic", Float) = 0
		[Toggle(_REVERTNORMAL_ON)] _RevertNormal("Revert Normal", Float) = 0
		[Toggle(_REVERSENOMAL_ON)] _ReverseNomal("Reverse Nomal", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma shader_feature_local _REVERSENOMAL_ON
		#pragma shader_feature_local _REVERTNORMAL_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _Normal1;
		uniform float4 _Normal1_ST;
		uniform sampler2D _Normal2;
		uniform float4 _Normal2_ST;
		uniform sampler2D _Albedo1;
		uniform float4 _Albedo1_ST;
		uniform sampler2D _Albedo2;
		uniform float4 _Albedo2_ST;
		uniform float _metalic;
		uniform sampler2D _ORM1;
		uniform float4 _ORM1_ST;
		uniform sampler2D _ORM2;
		uniform float4 _ORM2_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal1 = i.uv_texcoord * _Normal1_ST.xy + _Normal1_ST.zw;
			float2 uv_Normal2 = i.uv_texcoord * _Normal2_ST.xy + _Normal2_ST.zw;
			float3 lerpResult11 = lerp( (tex2D( _Normal1, uv_Normal1 )).rgb , (tex2D( _Normal2, uv_Normal2 )).rgb , i.vertexColor.r);
			#ifdef _REVERSENOMAL_ON
				float3 staticSwitch23 = ( lerpResult11 * float3(0,-1,0) );
			#else
				float3 staticSwitch23 = lerpResult11;
			#endif
			o.Normal = staticSwitch23;
			float2 uv_Albedo1 = i.uv_texcoord * _Albedo1_ST.xy + _Albedo1_ST.zw;
			float2 uv_Albedo2 = i.uv_texcoord * _Albedo2_ST.xy + _Albedo2_ST.zw;
			float3 lerpResult4 = lerp( (tex2D( _Albedo1, uv_Albedo1 )).rgb , (tex2D( _Albedo2, uv_Albedo2 )).rgb , i.vertexColor.r);
			o.Albedo = lerpResult4;
			o.Metallic = _metalic;
			float2 uv_ORM1 = i.uv_texcoord * _ORM1_ST.xy + _ORM1_ST.zw;
			float4 tex2DNode12 = tex2D( _ORM1, uv_ORM1 );
			float2 uv_ORM2 = i.uv_texcoord * _ORM2_ST.xy + _ORM2_ST.zw;
			float4 tex2DNode13 = tex2D( _ORM2, uv_ORM2 );
			float lerpResult18 = lerp( tex2DNode12.g , tex2DNode13.g , i.vertexColor.r);
			#ifdef _REVERTNORMAL_ON
				float staticSwitch19 = ( 1.0 - lerpResult18 );
			#else
				float staticSwitch19 = lerpResult18;
			#endif
			o.Smoothness = staticSwitch19;
			float lerpResult17 = lerp( tex2DNode12.r , tex2DNode13.r , i.vertexColor.r);
			o.Occlusion = lerpResult17;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18917
1211;73;708;509;1734.303;200.2941;3.201751;True;False
Node;AmplifyShaderEditor.SamplerNode;9;-515.4692,367.3477;Inherit;True;Property;_Normal2;Normal2;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-481.4677,123.3886;Inherit;True;Property;_Normal1;Normal1;3;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;7;-137.4879,218.4991;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;10;-165.7119,305.4468;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;13;-275.594,961.6598;Inherit;True;Property;_ORM2;ORM2;5;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;12;-303.3758,743.1725;Inherit;True;Property;_ORM1;ORM1;4;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;1;-831.0815,359.4103;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;18;41.91727,519.8926;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-57.41711,32.68237;Inherit;True;Property;_Albedo2;Albedo2;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;11;115.6153,260.3319;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;22;-72.03117,429.5384;Inherit;False;Constant;_Vector0;Vector 0;8;0;Create;True;0;0;0;False;0;False;0,-1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;2;-259.8699,-166.1935;Inherit;True;Property;_Albedo1;Albedo1;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;5;231.3595,64.42694;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;6;144.8702,-75.56299;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;169.8869,429.5826;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;20;188.2876,609.9885;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;601.9615,465.6427;Inherit;False;Property;_metalic;metalic;6;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;4;486.8167,206.6261;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;17;430.2105,602.0297;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;23;412.9845,356.1478;Inherit;False;Property;_ReverseNomal;Reverse Nomal;8;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;19;402.1902,490.2938;Inherit;False;Property;_RevertNormal;Revert Normal;7;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;800.473,395.1444;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Custom/VertexPaintShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;8;0
WireConnection;10;0;9;0
WireConnection;18;0;12;2
WireConnection;18;1;13;2
WireConnection;18;2;1;1
WireConnection;11;0;7;0
WireConnection;11;1;10;0
WireConnection;11;2;1;1
WireConnection;5;0;3;0
WireConnection;6;0;2;0
WireConnection;21;0;11;0
WireConnection;21;1;22;0
WireConnection;20;0;18;0
WireConnection;4;0;6;0
WireConnection;4;1;5;0
WireConnection;4;2;1;1
WireConnection;17;0;12;1
WireConnection;17;1;13;1
WireConnection;17;2;1;1
WireConnection;23;1;11;0
WireConnection;23;0;21;0
WireConnection;19;1;18;0
WireConnection;19;0;20;0
WireConnection;0;0;4;0
WireConnection;0;1;23;0
WireConnection;0;3;14;0
WireConnection;0;4;19;0
WireConnection;0;5;17;0
ASEEND*/
//CHKSM=99AF2CD07F0C7438480B56B3D31FE3CE7437A1F9
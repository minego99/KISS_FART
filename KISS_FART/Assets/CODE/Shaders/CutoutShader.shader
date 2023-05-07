// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/CutoutShader"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_ORM("ORM", 2D) = "white" {}
		[Toggle(_INVERTROUGHNESS_ON)] _Invertroughness("Invert roughness", Float) = 1
		[Toggle(_INVERTNORMAL_ON)] _Invertnormal("Invert normal", Float) = 0
		_poz("poz", Float) = 0
		_T_grass_blade_O("T_grass_blade_O", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Grass"  "Queue" = "AlphaTest+0" }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma shader_feature_local _INVERTNORMAL_ON
		#pragma shader_feature_local _INVERTROUGHNESS_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _ORM;
		uniform sampler2D _T_grass_blade_O;
		uniform float4 _T_grass_blade_O_ST;
		uniform float _poz;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float3 tex2DNode3 = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			#ifdef _INVERTNORMAL_ON
				float3 staticSwitch9 = ( tex2DNode3 * float3(0,-1,0) );
			#else
				float3 staticSwitch9 = tex2DNode3;
			#endif
			o.Normal = staticSwitch9;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode2 = tex2D( _Albedo, uv_Albedo );
			o.Albedo = (tex2DNode2).rgb;
			o.Metallic = 0.0;
			float4 tex2DNode5 = tex2D( _ORM, i.uv_texcoord );
			#ifdef _INVERTROUGHNESS_ON
				float staticSwitch8 = ( 1.0 - tex2DNode5.g );
			#else
				float staticSwitch8 = tex2DNode5.g;
			#endif
			o.Smoothness = staticSwitch8;
			o.Occlusion = tex2DNode5.r;
			o.Alpha = 1;
			float2 uv_T_grass_blade_O = i.uv_texcoord * _T_grass_blade_O_ST.xy + _T_grass_blade_O_ST.zw;
			clip( pow( tex2D( _T_grass_blade_O, uv_T_grass_blade_O ).r , _poz ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18917
3341;73;498;918;561.0674;115.3409;1.682948;True;False
Node;AmplifyShaderEditor.TexCoordVertexDataNode;15;-526.3346,495.996;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-848.0355,86.83676;Inherit;True;Property;_Normal;Normal;2;0;Create;True;0;0;0;False;0;False;-1;None;bc75a1650c7275142b3c2b57201161e0;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;11;-745.3994,276.8192;Inherit;False;Constant;_Vector0;Vector 0;6;0;Create;True;0;0;0;False;0;False;0,-1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;5;-586.7625,320.6726;Inherit;True;Property;_ORM;ORM;3;0;Create;True;0;0;0;False;0;False;-1;206dc0008c2826543886478e7e648c47;206dc0008c2826543886478e7e648c47;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-538.0355,-101.1632;Inherit;True;Property;_Albedo;Albedo;1;0;Create;True;0;0;0;False;0;False;-1;None;441e4b8f1be9cca4692e18e624a8d3b1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-481.3994,173.8192;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;6;46.59302,640.1865;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-15.98683,551.0067;Inherit;False;Property;_poz;poz;7;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;19;-342.0651,592.3086;Inherit;True;Property;_T_grass_blade_O;T_grass_blade_O;8;0;Create;True;0;0;0;False;0;False;-1;5c2711767bef49e45aaeca4544cf0733;5c2711767bef49e45aaeca4544cf0733;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;4;-212.0355,-15.16324;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;12;-182.7578,228.036;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;9;-352.3994,116.8192;Inherit;False;Property;_Invertnormal;Invert normal;5;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;13;8.242249,226.036;Inherit;False;Property;_InvertMask;Invert Mask;6;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;8;-202.3994,322.8192;Inherit;False;Property;_Invertroughness;Invert roughness;4;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;124.3687,140.5653;Inherit;False;Constant;_Float0;Float 0;7;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;17;193.0132,521.0067;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;308.4198,224.6363;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Custom/CutoutShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Grass;;AlphaTest;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;1;15;0
WireConnection;10;0;3;0
WireConnection;10;1;11;0
WireConnection;6;0;5;2
WireConnection;4;0;2;0
WireConnection;12;0;2;4
WireConnection;9;1;3;0
WireConnection;9;0;10;0
WireConnection;13;1;2;4
WireConnection;13;0;12;0
WireConnection;8;1;5;2
WireConnection;8;0;6;0
WireConnection;17;0;19;1
WireConnection;17;1;18;0
WireConnection;0;0;4;0
WireConnection;0;1;9;0
WireConnection;0;3;14;0
WireConnection;0;4;8;0
WireConnection;0;5;5;1
WireConnection;0;10;17;0
ASEEND*/
//CHKSM=BDD0F3C9195B27317DD757411F875DFAC2F9485E
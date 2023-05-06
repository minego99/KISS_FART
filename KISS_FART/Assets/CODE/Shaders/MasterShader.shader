// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/MasterShader"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_ORM("ORM", 2D) = "white" {}
		[Toggle(_INVERSESMOOTHNESS_ON)] _InverseSmoothness("InverseSmoothness", Float) = 0
		[Toggle(_INVERTNORMAL_ON)] _InvertNormal("Invert Normal", Float) = 0
		_Offset("Offset", Vector) = (1,1,0,0)
		_Tilling("Tilling", Vector) = (1,1,0,0)
		_metallic(" metallic", Float) = 0
		_EmissiveMask("Emissive Mask", 2D) = "white" {}
		_EmissiveIntensity("Emissive Intensity ", Float) = 0
		[HDR]_EmissiveColor("Emissive Color", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma shader_feature_local _INVERTNORMAL_ON
		#pragma shader_feature_local _INVERSESMOOTHNESS_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float2 _Offset;
		uniform float2 _Tilling;
		uniform sampler2D _Albedo;
		uniform sampler2D _EmissiveMask;
		uniform float4 _EmissiveMask_ST;
		uniform float _EmissiveIntensity;
		uniform float4 _EmissiveColor;
		uniform float _metallic;
		uniform sampler2D _ORM;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 temp_output_16_0 = ( ( i.uv_texcoord + _Offset ) * _Tilling );
			float3 temp_output_13_0 = (UnpackNormal( tex2D( _Normal, temp_output_16_0 ) )).xyz;
			#ifdef _INVERTNORMAL_ON
				float3 staticSwitch23 = ( temp_output_13_0 * float3(0,-1,0) );
			#else
				float3 staticSwitch23 = temp_output_13_0;
			#endif
			o.Normal = staticSwitch23;
			o.Albedo = (tex2D( _Albedo, temp_output_16_0 )).rgb;
			float2 uv_EmissiveMask = i.uv_texcoord * _EmissiveMask_ST.xy + _EmissiveMask_ST.zw;
			o.Emission = ( tex2D( _EmissiveMask, uv_EmissiveMask ) * _EmissiveIntensity * _EmissiveColor ).rgb;
			o.Metallic = _metallic;
			float4 tex2DNode3 = tex2D( _ORM, temp_output_16_0 );
			#ifdef _INVERSESMOOTHNESS_ON
				float staticSwitch12 = ( 1.0 - tex2DNode3.g );
			#else
				float staticSwitch12 = tex2DNode3.g;
			#endif
			o.Smoothness = staticSwitch12;
			o.Occlusion = tex2DNode3.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18917
1211;73;708;509;82.90329;-376.5232;1;True;False
Node;AmplifyShaderEditor.Vector2Node;17;-1251.677,514.8836;Inherit;False;Property;_Offset;Offset;5;0;Create;True;0;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TexCoordVertexDataNode;14;-1398.519,-53.94681;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-1041.677,331.8836;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;18;-1038.677,542.8836;Inherit;False;Property;_Tilling;Tilling;6;0;Create;True;0;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-875.6766,352.8836;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-596.3495,224.021;Inherit;True;Property;_Normal;Normal;1;0;Create;True;0;0;0;False;0;False;-1;None;15eed828cd9a9c94ea3ae9e4efbe25d8;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;13;-245.945,235.7686;Inherit;False;True;True;True;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;22;-213.7632,337.893;Inherit;False;Constant;_Vector0;Vector 0;6;0;Create;True;0;0;0;False;0;False;0,-1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;3;-705.7493,443.2209;Inherit;True;Property;_ORM;ORM;2;0;Create;True;0;0;0;False;0;False;-1;None;7c7df965939494b4cab5ce1e6bfcd218;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;27;98.29675,270.0552;Inherit;True;Property;_EmissiveMask;Emissive Mask;8;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-601.3495,29.02103;Inherit;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;0;False;0;False;-1;None;6bb985576c98d754cb308e234cceac79;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;5;-324.0868,738.5306;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;14.23682,278.893;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;30;359.4764,564.4888;Inherit;False;Property;_EmissiveColor;Emissive Color;10;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;28;183.4898,441.39;Inherit;False;Property;_EmissiveIntensity;Emissive Intensity ;9;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;497.5055,360.1784;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-10.26428,546.3099;Inherit;False;Property;_metallic; metallic;7;0;Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;4;-259.3495,84.02103;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;23;253.2368,250.893;Inherit;False;Property;_InvertNormal;Invert Normal;4;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;12;149.8588,607.2694;Inherit;False;Property;_InverseSmoothness;InverseSmoothness;3;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;888.1548,315.6312;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Custom/MasterShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;14;0
WireConnection;15;1;17;0
WireConnection;16;0;15;0
WireConnection;16;1;18;0
WireConnection;2;1;16;0
WireConnection;13;0;2;0
WireConnection;3;1;16;0
WireConnection;1;1;16;0
WireConnection;5;0;3;2
WireConnection;21;0;13;0
WireConnection;21;1;22;0
WireConnection;29;0;27;0
WireConnection;29;1;28;0
WireConnection;29;2;30;0
WireConnection;4;0;1;0
WireConnection;23;1;13;0
WireConnection;23;0;21;0
WireConnection;12;1;3;2
WireConnection;12;0;5;0
WireConnection;0;0;4;0
WireConnection;0;1;23;0
WireConnection;0;2;29;0
WireConnection;0;3;26;0
WireConnection;0;4;12;0
WireConnection;0;5;3;1
ASEEND*/
//CHKSM=AD35C82F52527DC98EEA119AD884F6470463CB4C
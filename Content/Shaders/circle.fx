#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
//float2 CirclePosition;
float Radius;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	//return tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
	float4 color = tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;

	float distance = float(pow(0.5f - input.TextureCoordinates.y, 2));
	distance =  distance + float(pow(0.5f - input.TextureCoordinates.x, 2));
	float PI = 3.141592653589793f;
	float angle = atan2(0.5f - input.TextureCoordinates.y, 0.5f - input.TextureCoordinates.x) * 180 / PI;

	if(distance < Radius && distance > Radius - 0.0009f && (angle % 8 >= 2 || angle % 8 <= -2))
		color = float4(1,1,1,1);
	return color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};
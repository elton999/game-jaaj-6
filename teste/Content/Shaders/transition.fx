#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float radious;

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
	float4 color = float4(0.0f,0.0f,0.0f,1);//tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
	
	float distance = float(pow(0.5f - input.TextureCoordinates.y, 2));
	distance =  distance + float(pow(0.5f - input.TextureCoordinates.x, 2) / 0.35f);
	
	if(distance < radious)
		color = float4(0.0f,0.0f,0.0f,0.0f) * 0.0f;	

	return color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};
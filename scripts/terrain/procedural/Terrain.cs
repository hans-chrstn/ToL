using System;
using System.Linq;
using Godot;

public partial class Terrain : Node2D
{
	[Export]
	public NoiseTexture2D NoiseHeightTexture { get; set; }

	
	private Noise _noise;

	private TileMapLayer _tileMap;
	private int _sourceID = 0;
	private Vector2I  _waterAtlas = new Vector2I(1, 4);
	private Vector2I _grassAtlas = new Vector2I(1, 1);

	private int _width = 100;
	private int _height = 100;

	public override void _Ready()
	{
		_noise = NoiseHeightTexture.Noise;
		_tileMap = GetNode<TileMapLayer>("TileMapLayer");
		ProcessChunks();
	}

	public void ProcessChunks()
	{
		for (int x = 0; x < _width; x++)
		{
			for (int y = 0; y < _height; y++)
			{
				float noise_val = _noise.GetNoise2D(x, y);

				if (noise_val >= 0.0)
				{
					_tileMap.SetCell(new Vector2I(x, y), _sourceID, _grassAtlas);
				}
				// else if (noise_val < 0.0)
				// {
				// 	_tileMap.SetCell(new Vector2I(x, y), _sourceID, _waterAtlas);

				// }
			}
		}

	}
}

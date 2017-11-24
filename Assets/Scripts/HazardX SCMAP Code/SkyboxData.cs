﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SkyboxData
{

	//TODO find what are these values are!
	/*
	public short[] BeginValues = new short[32];
	public Vector3[] beginvectors;
	public float[] BeginFloats;
	public int[] BeginInts;
	public Color[] BeginColors;
	*/

	public SkyboxValues Data;

	[System.Serializable]
	public class SkyboxValues
	{
		public byte[] BeginBytes = new byte[0];
		public string Albedo = "/textures/environment/Decal_test_Albedo001.dds";
		public string Glow = "/textures/environment/Decal_test_Glow001.dds";

		public int Length = 0;
		public byte[] MidBytes = new byte[0];
		public byte[] MidBytesStatic = new byte[0];

		public string Clouds = "/textures/environment/cirrus000.dds";

		public byte[] EndBytes = new byte[0];
	}


	public void Load(BinaryReader Stream)
	{
		// It should be some kind of settings for procedural skybox (colors, UV coordinates, rect, scale)

		Data = new SkyboxValues();

		// Sun and sky settins
		Data.BeginBytes = Stream.ReadBytes(64); // 16 x 4 bytes?

		// Planet and moon textures
		Data.Albedo = Stream.ReadStringNull();
		Data.Glow = Stream.ReadStringNull();


		// This should be settings for planets and moons on skybox

		//Array of layers
		// Planets, moons
		Data.Length = Stream.ReadInt32();
		if(Data.Length > 0)
			Data.MidBytes = Stream.ReadBytes(Data.Length * 40); // 10 x 4 bytes? 5 x 8 bytes?

		//Total of 19 bytes
		Data.MidBytesStatic = Stream.ReadBytes(19); // 4x 4 bytes + RGB (3bytes)?

		//Procedural Clouds Texture
		Data.Clouds = Stream.ReadStringNull();

		// Find total of 88 bytes
		// Animation settings and coordinates for procedural clouds
		Data.EndBytes = Stream.ReadBytes(88); // 11 x 8 bytes?

	}

	public void Save(BinaryWriter Stream)
	{
		Stream.Write(Data.BeginBytes);

		Stream.Write(Data.Albedo, true);
		Stream.Write(Data.Glow, true);

		Stream.Write(Data.Length);
		Stream.Write(Data.MidBytes);
		Stream.Write(Data.MidBytesStatic);
		
		Stream.Write(Data.Clouds, true);

		Stream.Write(Data.EndBytes);
	}
}

using SharpConfig;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameConfig : Singleton<GameConfig>
{
	public string configFileName = "gameconfig.cfg";
	[System.NonSerialized]
	public Configuration cfg = new Configuration();

	void Awake()
	{
		if (!File.Exists (configFileName))
		{
			Debug.Log ("Setting up a default config since no file was found!");
			SetupCleanCFG ();
			SaveConfig ();
		}

		// Load the configuration.
		cfg = Configuration.LoadFromFile(configFileName);
	}

	public void SaveConfig()
	{
		Debug.Log ("Saving Client config...");

		// Save the configuration.
		cfg.SaveToFile (configFileName);
	}

	private void SetupCleanCFG()
	{
		ServerPort = 42424;
		ServerName = "Grasslands World";
	}

	public int ServerPort { get { return cfg["Server"]["Port"].IntValue; } set { cfg["Server"]["Port"].IntValue = value; } }
	public string ServerName { get { return cfg["Server"]["Name"].StringValue; } set { cfg["Server"]["Name"].StringValue = value; } }
}

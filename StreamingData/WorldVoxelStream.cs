[BoltGlobalBehaviour]
public class WorldVoxelStreamCallbacks : Bolt.GlobalEventListener
{
    public static UdpKit.UdpChannelName WorldVoxelChannel;

    public override void BoltStartBegin()
    {
		print("Creating WorldVoxel channel");

        WorldVoxelChannel = BoltNetwork.CreateStreamChannel("WorldVoxelChannel", UdpKit.UdpChannelMode.Reliable, 1);
    }

	//public override void Connected(BoltConnection connection) {
	//	connection.SetStreamBandwidth(1024 * 2000);
	//}

    public override void SceneLoadRemoteDone(BoltConnection c)
	{
		try
		{
			if (BoltNetwork.IsServer)
			{
				print($"Sending WorldVoxel data");
				byte[] data = WorldEnvironment.Instance.SaveGameToByteArray();
				c.StreamBytes(WorldVoxelChannel, data);
			}
		}
		catch(System.Exception ex)
		{
			Debug.LogError("SceneLoadRemoteDone Exception");
			Debug.LogException(ex);
		}
    }

    public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
    {
		try
		{
			//Debug.LogError("StreamDataReceived");
			var length = data.Data.Length;
			Debug.Log($"Received WorldVoxel data length: {length}");
			//BoltLog.Info($"WorldVoxel data received: {length}");
			WorldEnvironment.Instance.LoadGameFromByteArray(data.Data, true, true);
		}
		catch(System.Exception ex)
		{
			Debug.LogError("SceneLoadRemoteDone Exception");
			Debug.LogException(ex);
		}
    }
}

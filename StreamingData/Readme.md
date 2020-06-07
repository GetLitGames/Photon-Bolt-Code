It should work as-is. You can add that file to your NetworkCallbacks.cs or in a separate class, just don't attach it to any objects.

Replace the SaveGameToByteArray and LoadGameFromByteArray with your own code.

This same example will work to force all clients to load a game in the middle of gameplay from the server like so:

byte[] data = WorldEnvironment.Instance.SaveGameToByteArray();
foreach(BoltConnection client in BoltNetwork.Clients) {
	client.StreamBytes(WorldVoxelStreamCallbacks.WorldVoxelChannel, data);
}



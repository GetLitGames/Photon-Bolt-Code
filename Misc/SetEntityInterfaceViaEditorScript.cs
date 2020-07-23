var objs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Unfiltered);
foreach(GameObject obj in objs)
{
    var entity = obj.GetComponent<BoltEntity>();
    using (var mod = entity.ModifySettings())
    {
        mod.persistThroughSceneLoads = false;
        mod.allowInstantiateOnClient = true;
        mod.clientPredicted = false;
        mod.prefabId = BoltPrefabs.Player;
        mod.updateRate = 1;
        mod.sceneId = Bolt.UniqueId.None;
        mod.serializerId = BoltInternal.StateSerializerTypeIds.IItemState;
    }
    EditorUtility.SetDirty(obj);
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncMusicStateBehavior : Bolt.EntityBehaviour<ISyncMusicState>
{
	public AudioSource TargetAudioSource;
	public float CheckSyncEverySecs = 2f;
	public float AllowedTimeOutOfSync = .5f;

	bool isInSync = false;
	float nextSyncCheckTime = 0;

	public override void Attached()
	{
	}

	void Update()
	{
		if (TargetAudioSource)
		{
			if (BoltNetwork.IsServer)
			{
				state.IsPlaying = TargetAudioSource.isPlaying;
				state.Time = 0;

				if (TargetAudioSource.isPlaying && nextSyncCheckTime <= Time.time)
					state.Time = TargetAudioSource.time;
			}
			else
			{
				if (nextSyncCheckTime <= Time.time)
				{
					nextSyncCheckTime = Time.time + CheckSyncEverySecs;

					if (state.IsPlaying != TargetAudioSource.isPlaying)
						isInSync = false;
					else
					if (state.IsPlaying)
					{
						if (TargetAudioSource.time < (state.Time - AllowedTimeOutOfSync) || 
							TargetAudioSource.time > (state.Time + AllowedTimeOutOfSync))
						{
							isInSync = false;
						}
					}
				}

				if (!isInSync)
				{
					isInSync = true;
					if (TargetAudioSource.isPlaying && !state.IsPlaying)
						TargetAudioSource.Stop();
					else
					if (!TargetAudioSource.isPlaying && state.IsPlaying)
						TargetAudioSource.Play();
					
					if (state.IsPlaying)
						TargetAudioSource.time = state.Time;
				}
			}
		}
	}
}

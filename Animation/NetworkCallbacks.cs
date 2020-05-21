using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bolt;
using Bolt.Matchmaking;
using Bolt.Photon;
using UdpKit;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.SceneManagement;

[BoltGlobalBehaviour]
public class AnimationEventBehavior : Bolt.GlobalEventListener, IAnimationEventListener
{
	public enum Type
	{
		Trigger,
		Bool,
		Float
	}

	public override void OnEvent(AnimationEvent ev)
	{
		if (ev.AnimationEntity == null) // during startup/connecting we get these
			return;

		AnimState animState = (AnimState)ev.AnimState;
		switch((Type)ev.AnimationType)
		{
			case Type.Trigger:
				ev.AnimationEntity.GetComponent<CharacterAnimations>().SetAnimState(animState); //GetComponentInChildren<Animator>().SetTrigger(ev.AnimationName);
				break;
			case Type.Bool:
				ev.AnimationEntity.GetComponent<CharacterAnimations>().SetAnimState(animState, ev.BoolValue); //.GetComponentInChildren<Animator>().SetBool(ev.AnimationName, ev.BoolValue);
				break;
			case Type.Float:
				ev.AnimationEntity.GetComponent<CharacterAnimations>().SetAnimState(animState, ev.FloatValue); //.GetComponentInChildren<Animator>().SetFloat(ev.AnimationName, ev.FloatValue);
				break;
		}
	}

	public static void SendEvent(BoltEntity entity, AnimState animState)
	{
		if (!BoltNetwork.IsRunning || entity == null)
			return;

		try
		{
			var ev = AnimationEvent.Create(Bolt.GlobalTargets.Others, Bolt.ReliabilityModes.ReliableOrdered);
			ev.AnimationEntity = entity;
			ev.AnimationType = (int)Type.Trigger;
			ev.AnimState = (int)animState;
			ev.Send();
		}
		catch(System.Exception ex)
		{
			Debug.LogException(ex);
		}
	}
	public static void SendEvent(BoltEntity entity, AnimState animState, bool boolValue)
	{
		if (!BoltNetwork.IsRunning || entity == null)
			return;

		try
		{
			var ev = AnimationEvent.Create(Bolt.GlobalTargets.Others, Bolt.ReliabilityModes.ReliableOrdered);
			ev.AnimationEntity = entity;
			ev.AnimationType = (int)Type.Bool;
			ev.AnimState = (int)animState;
			ev.BoolValue = boolValue;
			ev.Send();
		}
		catch(System.Exception ex)
		{
			Debug.LogException(ex);
		}
	}
	public static void SendEvent(BoltEntity entity, AnimState animState, float floatValue)
	{
		if (!BoltNetwork.IsRunning || entity == null)
			return;

		try
		{
			var ev = AnimationEvent.Create(Bolt.GlobalTargets.Others, Bolt.ReliabilityModes.ReliableOrdered);
			ev.AnimationEntity = entity;
			ev.AnimationType = (int)Type.Float;
			ev.AnimState = (int)animState;
			ev.FloatValue = floatValue;
			ev.Send();
		}
		catch(System.Exception ex)
		{
			Debug.LogException(ex);
		}
	}
}
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Reflection;

public class CharacterAnimations : MonoBehaviour
{
	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public enum AnimState {
		Default,
		Relax,
		Walk,
		WalkBackward,
		Run,
		RunBackward,
		Dash,
		StrafeLeft,
		StrafeRight,
		Jump,
		RightPunchAttack,
		LeftPunchAttack,
		Defend,
		TakeDamage,
		Die,
		LookUpWaveHand,
		Push,
		Pull,
		RollForward,
		RightThrow,
		LeftThrow,
		NodHead,
		ShakeHead,
		WaveHand,
		Stunned,
		PickUp,
		DrinkPotion,
		ChopTree,
		Digging,
		Talking,
		Sitting,
		LayGround,
		Crying,
		Clapping,
		Victory,
		HammeringOnAnvil,
		PushButton,
		SawingWood,
		SpawnGround,
		IsDead,
		IsAggro,
		ForwardBackward,
		LeftRight,
		Respawn,
		MeleeRightAttackDagger,
		CrouchWalk,
		CrouchWalkBackward,
		CrouchIdle,
		Die02,
		Die03, // Remember to add new triggers in IsTrigger
		Dig
	}

	bool IsTrigger(AnimState state)
	{
		switch (state)
		{
			case AnimState.Jump:
			case AnimState.ChopTree:
			case AnimState.RightPunchAttack:
			case AnimState.LeftPunchAttack:
			case AnimState.TakeDamage:
			case AnimState.Die:
			case AnimState.PickUp:
			case AnimState.DrinkPotion:
			case AnimState.PushButton:
			case AnimState.RightThrow:
			case AnimState.LeftThrow:
			case AnimState.CastSpell:
			case AnimState.SpawnGround:
			case AnimState.Respawn:
			case AnimState.MeleeRightAttackDagger:
			case AnimState.Die02:
			case AnimState.Die03:
			case AnimState.Dig:
				return true;

			default:
				return false;
		}
	}

	public void SetAnimState(AnimState state, bool value = true)
	{
		if (IsTrigger(state))
		{
			if (state == AnimState.Die)
			{
				anim.parameters.Where(x => x.type == AnimatorControllerParameterType.Bool).ForEach(x => anim.SetBool(x.name, false));
				anim.parameters.Where(x => x.type == AnimatorControllerParameterType.Trigger).ForEach(x => anim.ResetTrigger(x.name));

				anim.SetBool("IsDead", true);
				anim.SetTrigger("Die");

				//anim.CrossFadeInFixedTime("Die", .5f);
			}
			else
				anim.SetTrigger(state.ToString());
		}
		else
		{
			if (GetAnimState(state) == value)
				return;

			anim.SetBool(state.ToString(), value);
		}
	}

	public void SetAnimState(AnimState state, float value)
	{
		if (GetAnimStateF(state) == value)
			return;

		anim.SetFloat(state.ToString(), value);
	}

	public void SetAnimStateReplicated(AnimState state, bool value = true)
	{
		if (IsTrigger(state))
		{
			anim.SetTrigger(state.ToString());
			AnimationEventBehavior.SendEvent(Entity, state);
		}
		else
		{
			if (GetAnimState(state) == value)
				return;

			anim.SetBool(state.ToString(), value);
			AnimationEventBehavior.SendEvent(Entity, state, value);
		}
	}

	public void SetAnimStateReplicated(AnimState state, float value)
	{
		if (GetAnimStateF(state) == value)
			return;

		anim.SetFloat(state.ToString(), value);
		AnimationEventBehavior.SendEvent(Entity, state, value);
	}

	public bool GetAnimState(AnimState state)
	{
		return anim.GetBool(state.ToString());
	}

	public float GetAnimStateF(AnimState state)
	{
		return anim.GetFloat(state.ToString());
	}
}
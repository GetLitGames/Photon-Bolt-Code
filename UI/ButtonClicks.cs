using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClicks : MonoBehaviour
{
	public ButtonClickType ClickType;

	void Awake()
	{
		var buttons = GetComponentsInChildren<Button>();
		foreach(var button in buttons)
		{
			button.onClick.AddListener(() =>
			{
				SoundManager.Instance.PlayUISound(UIManager.Instance.ButtonClickSounds[(int)ClickType]);
			});
		}
	}
}

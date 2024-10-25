using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillLevelupButton : MonoBehaviour
{
	public Text skillNameText;
	public Button button;

	public void SetSkillSelectButton(string skillName, Action onClick)
	{
		skillNameText.text = skillName;
		button.onClick.AddListener(() => onClick());
	}
}

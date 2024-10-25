using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillLevelupButton : MonoBehaviour
{
	public Text skillNameText;
	public Button button;

	public void SetSkillSelectButton(string skillName, UnityAction onClick)
	{
		skillNameText.text = skillName;
		button.onClick.AddListener(() => onClick());
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
	public string skillName;
	public int skillLevel;
	public bool isTargetting;         // 가장 가까운 적을 향하는 스킬인지
	public GameObject[] skillPrefabs;  // 5개의 프리팹을 참조하여 각 레벨에 맞는 프리팹을 로드하도록 활용
	public GameObject currentSkillObject;
}

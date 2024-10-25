using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
	public string skillName;
	public int skillLevel;
	public GameObject skillPrefab;
	public bool isTargetting; // 가장 가까운 적을 향하는 스킬인지
}

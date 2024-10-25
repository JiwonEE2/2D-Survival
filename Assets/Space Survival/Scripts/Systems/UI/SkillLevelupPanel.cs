using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillLevelupPanel : MonoBehaviour
{
	public RectTransform list;
	public SkillLevelupButton buttonPrefab;


	// 플레이어가 레벨업을 하면 패널 활성화 요청
	public void LevelUpPanelOpen(List<Skill> skillList, Action<Skill> callback)
	{
		gameObject.SetActive(true);
		Time.timeScale = 0f;

		// 스킬 2개 UI에 표시할 예정
		List<Skill> selectedSkillList = new();
		while (selectedSkillList.Count < 2) // 2개의 스킬이 선택될 때까지 반복
		{
			int ranNum = Random.Range(0, skillList.Count);  // 랜덤한 숫자 하나 뽑기
			Skill selectedSkill = skillList[ranNum];        // 랜덤하게 선택된 스킬 가져오기
			if (selectedSkillList.Contains(selectedSkill))  // 이미 뽑힌 스킬이 또 뽑혔으면
			{
				continue;
			}
			selectedSkillList.Add(selectedSkill);           // 뽑힌 스킬을 List에 넣기.

			// 선택된 스킬을 레벨업하는 버튼 생성
			SkillLevelupButton skillButton = Instantiate(buttonPrefab, list);

			skillButton.SetSkillSelectButton(selectedSkill.skillName, () =>
			{
				callback(selectedSkill);
				LevelUpPanelClose();
			});
		}
	}

	// 레벨업 패널을 닫을 시 LevelUpPanelOpen의 callback을 호출
	public void LevelUpPanelClose()
	{
		foreach (Transform buttons in list)
		{
			Destroy(buttons.gameObject);
		}
		Time.timeScale = 1f;
		gameObject.SetActive(false);
	}
}

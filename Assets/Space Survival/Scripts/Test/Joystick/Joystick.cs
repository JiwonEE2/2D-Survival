using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
	public Transform background;
	public Transform handle;

	private const float MAX_DIST = 200f;  // handle이 이동할 수 있는 최대 거리

	public float x => handle.localPosition.x / MAX_DIST;
	public float y => handle.localPosition.y / MAX_DIST;

	private void Start()
	{
		background.gameObject.SetActive(false);
#if UNITY_EDITOR
		Input.simulateMouseWithTouches = true;
#endif
	}

	private void Update()
	{
		//print(Input.touchCount);
		// 터치 입력이 가능한 디바이스 (대표적으로 스마트폰)에 터치 입력이 있을 경우 해당 터치 개수만큼 카운트함
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			//background.gameObject.SetActive(true);
			//background.position = touch.position;

			switch (touch.phase)
			{
				case TouchPhase.Began:  // 터치가 시작될 때
					background.gameObject.SetActive(true);
					background.position = touch.position;
					break;
				case TouchPhase.Moved:  // 터치 후 손가락을 움직일 때
					handle.position = touch.position;
					handle.localPosition = Vector3.ClampMagnitude(handle.localPosition, MAX_DIST);
					break;
				case TouchPhase.Ended:  // 터치가 끝날 때 (손가락을 뗄 때)
				case TouchPhase.Canceled: // 시스템 또는 화면 밖으로 손가락이 나가는 등 터치가 취소될 때
					handle.localPosition = Vector3.zero;
					background.gameObject.SetActive(false);
					break;
			}
		}
	}
}

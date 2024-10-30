using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveController : MonoBehaviour
{
	public Joystick joystick;
	// ������ Input�� �̿��ؼ� �̵���Ű����
	public float moveSpeed;

	private void Update()
	{
		// ��ó�� ���ù� if : ������ �ܰ迡�� ���ǵ� �ɺ��� ���� �Ϻ� �ڵ带 ������ �����ϰ� ������ ��
#if UNITY_STANDALONE_WIN
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
#elif UNITY_ANDROID
		float x = joystick.x;
		float y = joystick.y;
#endif
		Vector3 moveDir = new Vector3(x, y) * Time.deltaTime * moveSpeed;
		transform.Translate(moveDir);
	}
}

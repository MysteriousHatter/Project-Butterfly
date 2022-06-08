using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @kurtdekker
// follows an arbitrary object at 90 degrees to its right
// always regards the object's look point
// has some lerping smoothness to its positioning
// replace with Cinemachine if you want more features already!

public class CameraFollowPlayer : MonoBehaviour
{

	public Transform PositionTarget;
	public Transform LookTarget;

	public float Distance;
	public float Above;

	// how snappy the camera's location moves
	public float MoveSnappiness;

	// how snappy the camera's gaze moves
	public float LookSnappiness;

	Vector3 LookPosition;

	void Update()
	{
		
	}
	void Reset()
	{
		Distance = 10.0f;
		Above = 4.0f;
		MoveSnappiness = 3.0f;
		LookSnappiness = 2.0f;
	}

	bool HaveRun;

	void LateUpdate()
	{
		if (PositionTarget)
		{
			Vector3 right = PositionTarget.right;

			Vector3 position = PositionTarget.position + right * Distance + Vector3.up * Above;

			transform.position = Vector3.Lerp(transform.position, position, MoveSnappiness * Time.deltaTime);

			LookPosition = Vector3.Lerp(LookPosition, LookTarget.position, LookSnappiness * Time.deltaTime);

			if (!HaveRun)
			{
				transform.position = position;
				LookPosition = LookTarget.position;
			}

			transform.LookAt(LookPosition);
		}

		HaveRun = true;
	}
}

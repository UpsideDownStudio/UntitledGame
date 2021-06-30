using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
	[FormerlySerializedAs("Target")] public Transform target;
	[FormerlySerializedAs("Smooth")] public float smooth = 5.0f;
	[FormerlySerializedAs("Offset")] public Vector3 offset;

	private void Start()
	{
		offset = transform.position;
	}

	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth);
	}
}

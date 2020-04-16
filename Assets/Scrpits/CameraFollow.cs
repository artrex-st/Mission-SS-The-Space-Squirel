using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform CameraTransform, PlayerTransform; // Camera Transform
	[Range(0f,100f)]
	public float speed=3;
	void Start()
	{
		PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
		CameraTransform = GetComponent<Transform>();

	}
	void LateUpdate()
	{
		SimpleFollow();
	}

	void SimpleFollow()
	{
		CameraTransform.position = Vector3.Lerp(CameraTransform.position, new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, CameraTransform.position.z), speed * Time.deltaTime);
	}

	void RoundFollow()
	{
		CameraTransform.position = Vector3.Lerp(CameraTransform.position, new Vector3(Mathf.Round(PlayerTransform.position.x), Mathf.Round(PlayerTransform.position.y), CameraTransform.position.z), speed * Time.deltaTime);
	}
	void MaxRangeFollow()
	{
		CameraTransform.position = Vector3.MoveTowards(CameraTransform.position, new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, CameraTransform.position.z), speed * Time.deltaTime);
	}


}

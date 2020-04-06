using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform CameraTransform, PlayerTransform; // Camera Transform
	void Start()
	{
		PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
		CameraTransform = GetComponent<Transform>();

	}
	void Update()
	{
		CameraTransform.position = Vector3.Lerp(CameraTransform.position,new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, CameraTransform.position.z), 3 * Time.deltaTime);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixa : MonoBehaviour
{
	public bool olharParaJogador;
	public GameObject trigger;

	private Transform jogadorTransform;

	private CameraFixaTrigger cameraTrigger;

	internal float modificadorHorizontal;
	internal float modificadorVertical;

	void Start()
	{
		jogadorTransform = GameObject.Find("Jogador").transform;

		if (trigger)
		{
			cameraTrigger = trigger.GetComponent<CameraFixaTrigger>();
			cameraTrigger.camera = GetComponent<Camera>();
		}

		modificadorHorizontal = transform.localRotation.y > 0.5f ? -1f : 1f;
		modificadorVertical = transform.localRotation.y > 0.5f ? -1f : 1f;
	}

	void Update()
	{
		if (olharParaJogador)
			transform.LookAt(jogadorTransform);
	}
}

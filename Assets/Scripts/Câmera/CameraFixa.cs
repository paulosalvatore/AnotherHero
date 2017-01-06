using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixa : MonoBehaviour
{
	public bool olharParaJogador;
	public GameObject trigger;

	private GameObject jogador;

	private CameraFixaTrigger cameraTrigger;

	internal float modificadorHorizontal;
	internal float modificadorVertical;

	void Start()
	{
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
		if (!jogador)
			jogador = GameObject.Find("Jogador");

		if (olharParaJogador)
			transform.LookAt(jogador.transform);
	}
}

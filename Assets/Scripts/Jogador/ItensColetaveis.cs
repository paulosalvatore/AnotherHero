﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensColetaveis : MonoBehaviour
{
	internal bool itemColetado = false;
	
	internal BotaoAuxilio botaoAuxilio;
	internal new Camera camera;

	private ControladorCena controladorCena;

	void Start()
	{
		controladorCena = ControladorCena.Pegar();

		Transform botaoAuxilioTransform = transform.FindChild("BotãoAuxílio");
		if (botaoAuxilioTransform)
		{
			botaoAuxilio = botaoAuxilioTransform.GetComponent<BotaoAuxilio>();
			botaoAuxilio.gameObject.SetActive(false);
		}

		Transform cameraTransform = transform.FindChild("Camera");
		if (cameraTransform)
		{
			camera = cameraTransform.GetComponent<Camera>();
		}
	}

	void Update()
	{
		if (controladorCena == null)
		{
			controladorCena = ControladorCena.Pegar();

			controladorCena.AlteracaoExibicaoAuxilioInteracao(false);

			if (botaoAuxilio)
				botaoAuxilio.Desativar();
		}
		else if (itemColetado)
		{
			gameObject.SetActive(false);

			controladorCena.AlteracaoExibicaoAuxilioInteracao(false);

			if (botaoAuxilio)
				botaoAuxilio.Desativar();
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (itemColetado || controladorCena == null)
			return;

		if (collider.CompareTag("Player"))
		{
			//controladorCena.AlteracaoExibicaoAuxilioInteracao(true, "RB or LB = pick");

			if (botaoAuxilio)
				botaoAuxilio.Ativar();

			if (camera)
				controladorCena.jogadorScript.forcarCameraDisponivel = camera;

			controladorCena.jogadorScript.inventarioScript.itemDisponivel = gameObject;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			controladorCena.AlteracaoExibicaoAuxilioInteracao(false);

			if (botaoAuxilio)
				botaoAuxilio.Desativar();

			if (controladorCena.jogadorScript.inventarioScript.itemDisponivel == gameObject)
				controladorCena.jogadorScript.inventarioScript.itemDisponivel = null;

			if (camera && controladorCena.jogadorScript.forcarCamera == camera)
				controladorCena.jogadorScript.forcarCamera = null;

			if (camera && controladorCena.jogadorScript.forcarCameraDisponivel == camera)
				controladorCena.jogadorScript.forcarCameraDisponivel = null;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
	internal BotaoAuxilio botaoAuxilio;
	internal new Camera camera;

	private ControladorCena controladorCena;

	void Start ()
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
	}

	void OnTriggerEnter(Collider collider)
	{
		if (controladorCena == null)
			return;

		if (collider.CompareTag("Player"))
		{
			if (botaoAuxilio)
				botaoAuxilio.Ativar();

			if (camera)
				controladorCena.jogadorScript.forcarCameraDisponivel = camera;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			if (botaoAuxilio)
				botaoAuxilio.Desativar();

			if (camera && controladorCena.jogadorScript.forcarCamera == camera)
				controladorCena.jogadorScript.forcarCamera = null;

			if (camera && controladorCena.jogadorScript.forcarCameraDisponivel == camera)
				controladorCena.jogadorScript.forcarCameraDisponivel = null;
		}
	}
}

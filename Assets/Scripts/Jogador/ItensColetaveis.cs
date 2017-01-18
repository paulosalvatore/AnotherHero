using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensColetaveis : MonoBehaviour
{
	public GameObject botaoAuxilio;
	public Vector3 posicaoBotaoAuxilio;

	internal bool itemColetado = false;

	private ControladorCena controladorCena;

	void Start()
	{
		controladorCena = ControladorCena.Pegar();

		if (botaoAuxilio)
		{
			botaoAuxilio = Instantiate(botaoAuxilio) as GameObject;
			botaoAuxilio.transform.parent = transform;
			botaoAuxilio.transform.localPosition = posicaoBotaoAuxilio;
		}
	}

	void Update()
	{
		if (controladorCena == null)
		{
			controladorCena = ControladorCena.Pegar();

			controladorCena.AlteracaoExibicaoAuxilioInteracao(false);

			botaoAuxilio.SetActive(false);
		}
		else if (itemColetado)
		{
			gameObject.SetActive(false);

			controladorCena.AlteracaoExibicaoAuxilioInteracao(false);

			botaoAuxilio.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (itemColetado || controladorCena == null)
			return;

		if (collider.CompareTag("Player"))
		{
			//controladorCena.AlteracaoExibicaoAuxilioInteracao(true, "RB or LB = pick");

			botaoAuxilio.SetActive(true);
			
			controladorCena.jogadorScript.inventarioScript.itemDisponivel = gameObject;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			controladorCena.AlteracaoExibicaoAuxilioInteracao(false);

			botaoAuxilio.SetActive(false);

			if (controladorCena.jogadorScript.inventarioScript.itemDisponivel == gameObject)
				controladorCena.jogadorScript.inventarioScript.itemDisponivel = null;
		}
	}
}

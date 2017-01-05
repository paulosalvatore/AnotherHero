using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCenario : MonoBehaviour
{
	[Header("Controlador da Cena")]
	public GameObject controladorCenaPrefab;
	private ControladorCena controladorCena;

	[Header("Jogador")]
	public GameObject spawnPointJogador;
	private GameObject jogador;

	void Start ()
	{
		controladorCena = ControladorCena.Pegar(controladorCenaPrefab);
	}

	void Update ()
	{
		if (!jogador)
		{
			jogador = GameObject.Find("Jogador");
			if (jogador)
			{
				jogador.GetComponent<Rigidbody>().isKinematic = false;

				GameObject destino = spawnPointJogador;
				if (controladorCena.saida != null)
				{
					GameObject procurarDestino = GameObject.Find(controladorCena.saida);

					if (procurarDestino)
						destino = procurarDestino.transform.FindChild("Spawn").gameObject;
				}

				jogador.transform.position = new Vector3(
					destino.transform.position.x,
					Mathf.Max(jogador.transform.position.y, destino.transform.position.y),
					destino.transform.position.z
				);

				jogador.transform.rotation = destino.transform.rotation;

				controladorCena.jogadorMovimentoScript.AlterarMovimento(false);
				controladorCena.jogadorScript.AtualizarCamerasCenario();
				controladorCena.jogadorScript.mudandoCena = false;
			}
		}
	}
}

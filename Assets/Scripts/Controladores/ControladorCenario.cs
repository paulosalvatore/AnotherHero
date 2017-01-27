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
	private bool jogadorInstanciado = false;

	void Start ()
	{
		controladorCena = ControladorCena.Pegar(controladorCenaPrefab);
	}

	void Update ()
	{
		ProcurarJogador();
	}

	void ProcurarJogador()
	{
		if (jogadorInstanciado)
			return;

		jogador = !jogador ? GameObject.Find("Jogador") : null;
		if (jogador)
			InstanciarJogador();
	}

	void InstanciarJogador()
	{
		jogadorInstanciado = true;
		// Desativa efeitos da gravidade ao jogador
		jogador.GetComponent<Rigidbody>().isKinematic = false;

		// Verifica se o jogador vem de outra cena, caso venha, procurar o local por onde ele saiu
		// Caso encontre um local de mesmo nome na nova cena, o spawnPoint passa a ser esse novo local
		// Caso não encontre, o destino será o spawnPoint do Jogador
		GameObject destino = spawnPointJogador;
		if (controladorCena.saida != null)
		{
			GameObject procurarDestino = GameObject.Find(controladorCena.saida);
			if (procurarDestino)
				destino = procurarDestino.transform.FindChild("Spawn").gameObject;
		}

		// Aplica a nova posição baseada na posição do destino
		jogador.transform.position = new Vector3(
			destino.transform.position.x,
			Mathf.Max(jogador.transform.position.y, destino.transform.position.y),
			destino.transform.position.z
		);

        // Aplicar apenas a rotação do 'destino' no eixo Y.
        jogador.transform.rotation = new Quaternion(


    jogador.transform.rotation.x,
	destino.transform.rotation.y,
	jogador.transform.rotation.z,
	jogador.transform.rotation.w
		);

		controladorCena.jogadorScript.FinalizaMudancaCena();
	}
}

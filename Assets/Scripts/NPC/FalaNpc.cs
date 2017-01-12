using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalaNpc : MonoBehaviour
{
	/*
	 * TODO
	 * Falta colocar um informativo para que o jogador saiba que ele deve pressionar o botão A
	*/

	// Falas declaradas no Inspector
	public List<string> falas;

	// Formatação das falas checando o limite de caracteres para cada frase
	private List<string> frases = new List<string>();
	private string fraseEmFormacao = "";
	private string fraseAtual;
	private int indexFraseAtual = 0;
	private bool proximaFraseDisponivel;
	private bool completarFraseAtualDisponivel;
	private bool completarFraseAtual = false;

	private bool npcIniciado = false;

	private ControladorCena controladorCena;

	void Update()
	{
		if (controladorCena == null)
		{
			controladorCena = ControladorCena.Pegar();

			if (controladorCena)
				ProcessarFalas();
		}
		
		if (Input.GetButtonDown("A Button"))
			ProsseguirExibicaoFraseAtual();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player"))
			IniciarInteracaoNpc();
	}


	void ProcessarFalas()
	{
		foreach (string fala in falas)
		{
			if (fala.Length == 0)
				continue;

			if (fala.Length > controladorCena.maximoCaracteres)
			{
				string[] falaSeparada = fala.Split(' ');
				foreach (string palavraFala in falaSeparada)
				{
					if (fraseEmFormacao.Length + palavraFala.Length > controladorCena.maximoCaracteres)
						ArmazenarFalaDisponivel();

					fraseEmFormacao += palavraFala + " ";
				}
			}
			else
				fraseEmFormacao = fala;

			ArmazenarFalaDisponivel();
		}
	}

	void ArmazenarFalaDisponivel()
	{
		frases.Add(fraseEmFormacao.Substring(0, fraseEmFormacao.Length - 1));
		fraseEmFormacao = "";
		proximaFraseDisponivel = true;
	}

	void ExibirFraseAtual()
	{
		if (frases.Count <= indexFraseAtual)
			return;

		fraseAtual = frases[indexFraseAtual];
		indexFraseAtual++;

		completarFraseAtualDisponivel = true;
		
		StartCoroutine(IniciarExibicaoFraseAtual());
	}

	IEnumerator IniciarExibicaoFraseAtual()
	{
		int indiceFraseAtual = 0;
		string construirFrase = "";

		while (true)
		{
			if (completarFraseAtual)
			{
				CompletarFraseAtual();
				break;
			}

			if (completarFraseAtual || indiceFraseAtual >= fraseAtual.Length)
			{
				FinalizarExibicaoFraseAtual();
				break;
			}

			char letraFraseAtual = fraseAtual[indiceFraseAtual];
			construirFrase += letraFraseAtual;

			controladorCena.textoNpc.text = construirFrase;

			indiceFraseAtual++;

			yield return new WaitForSeconds(0.01f);
		}
	}

	IEnumerator InformarProximaFraseDisponivel()
	{
		while (true)
		{
			if (!completarFraseAtualDisponivel && proximaFraseDisponivel && controladorCena.textoNpc.text.Length > 0)
			{
				char ultimoCaractereTextoNpc = controladorCena.textoNpc.text[controladorCena.textoNpc.text.Length - 1];
				
				controladorCena.textoNpc.text = fraseAtual + (ultimoCaractereTextoNpc == '_' ? "" : "_");
			}

			yield return new WaitForSeconds(0.5f);
		}
	}

	void CompletarFraseAtual()
	{
		controladorCena.textoNpc.text = fraseAtual;
		completarFraseAtual = false;
		FinalizarExibicaoFraseAtual();
	}

	void FinalizarExibicaoFraseAtual()
	{
		completarFraseAtualDisponivel = false;
		proximaFraseDisponivel = frases.Count > indexFraseAtual ? true : false;
	}

	void ProsseguirExibicaoFraseAtual()
	{
		if (completarFraseAtualDisponivel)
		{
			completarFraseAtual = true;
			completarFraseAtualDisponivel = false;
			return;
		}

		if (proximaFraseDisponivel)
			ExibirFraseAtual();
		else
			EncerrarInteracaoNpc();
	}

	void IniciarInteracaoNpc()
	{
		if (npcIniciado)
			return;

		indexFraseAtual = 0;
		proximaFraseDisponivel = true;

		npcIniciado = true;

		StartCoroutine(InformarProximaFraseDisponivel());

		controladorCena.AlteracaoExibicaoFalaNpc(true);
		controladorCena.jogadorScript.movimentoScript.AlterarMovimento(false);

		ProsseguirExibicaoFraseAtual();
	}

	void EncerrarInteracaoNpc()
	{
		if (!npcIniciado)
			return;

		npcIniciado = false;

		controladorCena.AlteracaoExibicaoFalaNpc(false);
		controladorCena.jogadorScript.movimentoScript.AlterarMovimento(true);
	}
}

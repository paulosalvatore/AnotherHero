using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FalaNpc : MonoBehaviour
{
	public GameObject falaNpc;
	public List<string> falas;

	private Text textoNpc;

	private bool npcFalando;

	private JogadorMovimento jogador;

	private int maximoCaracteres = 160;

	void Start()
	{
		textoNpc = falaNpc.transform.FindChild("Texto").GetComponent<Text>();
		jogador = GameObject.Find("Jogador").GetComponent<JogadorMovimento>();
	}

	void Update()
	{
		if (npcFalando)
		{
			if (Input.GetButtonDown("A Button"))
			{
				if (aguardandoProximaFala)
				{
					aguardandoProximaFala = false;
					indiceAtual++;
					falaAtual = palavrasAtuais[indiceAtual];
					construcaoFalaAtual = "";
					indiceFalaAtual = 0;
				}
				else if (aguardandoEncerramentoFala)
				{
					processandoFala = false;
					EncerrarFalaNpc();
				}
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			IniciarFalaNpc();
		}
	}

	private List<string> falasAtuais = new List<string>();
	private int indiceAtual = 0;
	private int indiceFalaAtual = 0;
	private string construcaoFalaAtual = "";
	private string falaAtual = "";
	private bool processandoFala = false;
	private List<string> palavrasAtuais = new List<string>();

	void IniciarFalaNpc()
	{
		if (npcFalando || falas.Count == 0)
			return;

		textoNpc.text = "";
		/* Pegar a frase atual do NPC (0)
		 * Checar o número de caracteres
		 * Formar o número de etapas necessárias
		 * Se proximaEtapa == true, colocar _ no fim do quadro
		 * Alterar variável fraseAtual para o texto atual
		 * Ativar processamento da frase
		 * Inicia coroutine de processamento de frase
		 * Caso aperte A novamente, colocar a frase inteira e alterar o estado para
		 * aguardando A para proxima frase
		 */
		 
		//if (caracteresTotal > maximoCaracteres)
		//{

		indiceAtual = 0;
		falaAtual = falas[indiceAtual];
		string[] palavrasSeparadas = falaAtual.Split(' ');
		//List<string> palavrasRestantes = new List<string>();
		//string palavrasAtuais = "";
		palavrasAtuais = new List<string>();
		int indicePalavrasAtuais = 0;
		foreach (string palavra in palavrasSeparadas)
		{
			string checarPalavra = palavra + " ";

			if (palavrasAtuais.Count <= indicePalavrasAtuais)
				palavrasAtuais.Add("");

			string construirFala = palavrasAtuais[indicePalavrasAtuais];
			if (construirFala.Length + checarPalavra.Length <= maximoCaracteres)
				palavrasAtuais[indicePalavrasAtuais] += checarPalavra;
			else
			{
				palavrasAtuais[indicePalavrasAtuais] = palavrasAtuais[indicePalavrasAtuais].Substring(0, palavrasAtuais[indicePalavrasAtuais].Length - 1);
				
				indicePalavrasAtuais++;

				if (palavrasAtuais.Count <= indicePalavrasAtuais)
					palavrasAtuais.Add("");

				construirFala = palavrasAtuais[indicePalavrasAtuais];
				palavrasAtuais[indicePalavrasAtuais] += checarPalavra;
			}
		}
		//}
		
		falaAtual = palavrasAtuais[indiceAtual];
		int caracteresTotal = falaAtual.Length;

		falasAtuais.Add(falaAtual);
		construcaoFalaAtual = "";
		indiceFalaAtual = 0;
		processandoFala = true;
		StartCoroutine(ProcessarFala());

		AlterarEstadoNpcFalando(true);
	}

	//private float intervaloConstrucaoFrase = 0.05f;
	private float intervaloConstrucaoFrase = 0.01f;
	private float intervaloAguardandoProximaFala = 0.5f;
	private bool aguardandoProximaFala = false;
	private bool aguardandoEncerramentoFala = false;
	private bool utilizarIntervaloConstrucaoFrase = true;

	IEnumerator ProcessarFala()
	{
		while (processandoFala)
		{
			if (aguardandoProximaFala)
			{
				if (textoNpc.text[textoNpc.text.Length - 1] == '_')
					textoNpc.text = falaAtual;
				else
					textoNpc.text = falaAtual + '_';
				utilizarIntervaloConstrucaoFrase = false;
			}
			else
			{
				utilizarIntervaloConstrucaoFrase = true;

				construcaoFalaAtual += falaAtual[indiceFalaAtual];
				textoNpc.text = construcaoFalaAtual;

				indiceFalaAtual++;
				
				if (falaAtual.Length == indiceFalaAtual)
				{
					if (palavrasAtuais.Count - 1 > indiceAtual)
					{
						aguardandoProximaFala = true;
					}
					else
					{
						aguardandoEncerramentoFala = true;
						break;
					}
				}
			}

			yield return new WaitForSeconds(utilizarIntervaloConstrucaoFrase ? intervaloConstrucaoFrase : intervaloAguardandoProximaFala);
		}
	}

	void EncerrarFalaNpc()
	{
		if (!npcFalando)
			return;

		AlterarEstadoNpcFalando(false);
	}

	void AlterarEstadoNpcFalando(bool estado)
	{
		npcFalando = estado;
		falaNpc.SetActive(estado);
		jogador.AlterarFalandoComNpc(estado);
	}
}

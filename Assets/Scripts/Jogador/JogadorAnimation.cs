using UnityEngine;
using System.Collections;

public class JogadorAnimation : MonoBehaviour
{
	//[Header("Arma")]
	//public GameObject arma;
	//internal bool exibirArma;

	//[Header("Lanterna")]
	//public GameObject lanterna;
	internal bool exibirLanterna;

	internal bool alavanca;

	internal Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator>();
	}

	public void AtualizarAnimacao(string nomeAnimacao)
	{
		if (!animator)
			return;

		if (nomeAnimacao == "Walk")
		{
			animator.SetInteger("velocidade", 1);
		}
		else if (nomeAnimacao == "Run")
		{
			animator.SetInteger("velocidade", 2);
		}
		else if (nomeAnimacao == "Idle")
		{
			animator.SetInteger("velocidade", 0);
		}

		//animator.SetBool("arma", exibirArma);
		animator.SetBool("lanterna", exibirLanterna);

		if (alavanca)
		{
			animator.SetTrigger("alavanca");
			alavanca = false;
		}

		//AlterarEstadoArma(exibirArma);
		//AlterarEstadoLanterna(exibirLanterna);
	}

	void AlterarEstadoArma(bool novoEstado)
	{
		//arma.SetActive(novoEstado);
	}

	void AlterarEstadoLanterna(bool novoEstado)
	{
		//lanterna.SetActive(novoEstado);
	}
}

using UnityEngine;
using System.Collections;

public class JogadorAnimation : MonoBehaviour
{
	[Header("Arma")]
	public GameObject arma;
	internal bool exibirArma;

	[Header("Lanterna")]
	public GameObject lanterna;
	internal bool exibirLanterna;

	private Animator animator;

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
			animator.SetFloat("velocidade", 0.1f);
		}
		else if (nomeAnimacao == "Run")
		{
			animator.SetFloat("velocidade", 0.31f);
		}
		else if (nomeAnimacao == "Idle")
		{
			animator.SetFloat("velocidade", 0f);
		}
		/*else if (nomeAnimacao == "RunGun")
		{
			animator.SetFloat("velocidade", 0.31f);
		}*/

		animator.SetBool("arma", exibirArma);
		animator.SetBool("lanterna", exibirLanterna);

		AlterarEstadoArma(exibirArma);
		AlterarEstadoLanterna(exibirLanterna);
	}

	void AlterarEstadoArma(bool novoEstado)
	{
		arma.SetActive(novoEstado);
	}

	void AlterarEstadoLanterna(bool novoEstado)
	{
		lanterna.SetActive(novoEstado);
	}
}

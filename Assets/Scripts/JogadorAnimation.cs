using UnityEngine;
using System.Collections;

public class JogadorAnimation : MonoBehaviour
{
	public GameObject arma;

	private Animator animator;
	internal bool exibirArma;

	void Start ()
	{
		animator = GetComponent<Animator>();
	}

	void FixedUpdate ()
	{
		exibirArma = false;

		if (Input.GetKeyDown(KeyCode.Z))
			AtualizarAnimacao("Idle");
		else if (Input.GetKeyDown(KeyCode.X))
			AtualizarAnimacao("Walk");
		else if (Input.GetKeyDown(KeyCode.C))
			AtualizarAnimacao("Run");
		else if (Input.GetKeyDown(KeyCode.V))
		{
			exibirArma = true;
			AtualizarAnimacao("RunGun");
		}
	}

	public void AtualizarAnimacao(string nomeAnimacao)
	{
		if (nomeAnimacao == "Walk")
		{
			animator.SetFloat("velocidade", 0.1f);
			animator.SetBool("arma", false);
		}
		else if (nomeAnimacao == "Run")
		{
			animator.SetFloat("velocidade", 0.31f);
			animator.SetBool("arma", false);
		}
		else if (nomeAnimacao == "Idle")
		{
			animator.SetFloat("velocidade", 0f);
			animator.SetBool("arma", false);
		}
		else if (nomeAnimacao == "RunGun")
		{
			animator.SetFloat("velocidade", 0.31f);
			animator.SetBool("arma", true);
		}

		AlterarEstadoArma(exibirArma);
	}

	void AlterarEstadoArma(bool estadoArma)
	{
		arma.SetActive(estadoArma);
	}
}

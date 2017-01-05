using UnityEngine;
using System.Collections;

public class TrocarCenarioAntigo : MonoBehaviour
{
	public GameObject destino;

	public Animator canvasAnimator;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			JogadorMovimento jogador = collider.GetComponent<JogadorMovimento>();
			if (jogador.mudancaLiberada && jogador.atual != gameObject)
			{
				jogador.mudancaLiberada = false;
				jogador.atual = destino;
				collider.transform.position = destino.transform.position;
				canvasAnimator.SetTrigger("fadeOut");
				canvasAnimator.SetTrigger("fadeIn");
			}
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			JogadorMovimento jogador = collider.GetComponent<JogadorMovimento>();
			if (!jogador.mudancaLiberada && jogador.atual == gameObject)
			{
				jogador.mudancaLiberada = true;
				jogador.atual = null;
			}
		}
	}
}

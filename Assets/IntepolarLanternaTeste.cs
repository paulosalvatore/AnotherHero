using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntepolarLanternaTeste : MonoBehaviour
{
	[Header("Index da Camada de Animação")]
	public int layerIndex;

	[Header("Duração e FPS da Transição")]
	public float duracaoTransicao;
	public int fpsTransicao;

	[Header("Botão que acionará a Transição")]
	public string buttonName;

	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}
	
	void Update()
	{
		if (Input.GetButtonDown(buttonName))
		{
			float layerWeight = animator.GetLayerWeight(layerIndex);

			if (layerWeight == 0 || layerWeight == 1)
				StartCoroutine(IniciarTransicao());
		}
	}

	IEnumerator IniciarTransicao()
	{
		// De 0 até 1 em 5 segundos
		// 

		float tempo = duracaoTransicao / (fpsTransicao * duracaoTransicao);
		float layerWeight = animator.GetLayerWeight(layerIndex);
		float layerWeightInicial = layerWeight == 0 ? 1 : 0;

		float valor = (tempo / duracaoTransicao) * (layerWeight == 0 ? 1 : -1);

		while (true)
		{
			layerWeight = Mathf.Clamp(layerWeight + valor, 0, 1);

			animator.SetLayerWeight(layerIndex, layerWeight);

			if (layerWeight == 0 || layerWeight == 1)
				break;

			yield return new WaitForSeconds(tempo);
		}
	}
}

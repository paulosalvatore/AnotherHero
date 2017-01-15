using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JogadorMovimento : MonoBehaviour
{
	public bool movimentacaoBaseadaCamera;
	public float velocidadeAndando;
	public float velocidadeCorrendo;
	private bool correndo;
	private bool movimentoLiberado = false;
	private float h = 0f;
	private float v = 0f;
	private float hAnterior = 0f;
	private float vAnterior = 0f;
	private Vector3 posicaoAnterior;
	internal Vector2 modificadores = new Vector2(1f, 1f);
	private int contadorModificadores = 0;
	private int limiteContadorModificadores = 5;

	private new Rigidbody rigidbody;
	private JogadorAnimation jogadorAnimation;

	//private bool exibirArma;
	//private bool exibirLanterna;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		jogadorAnimation = GetComponent<JogadorAnimation>();
	}

	void FixedUpdate()
	{
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		
		if (Camera.main)
		{
			modificadores.x = Camera.main.GetComponent<CameraFixa>().modificadorHorizontal;

			if (modificadores.y != Camera.main.GetComponent<CameraFixa>().modificadorVertical)
			{
				if (Mathf.Abs(v) <= 0.05f)
				{
					if (contadorModificadores == limiteContadorModificadores)
					{
						modificadores.y = Camera.main.GetComponent<CameraFixa>().modificadorVertical;
					}
					else
						contadorModificadores++;
				}
				else
					contadorModificadores = 0;
			}
			else
				contadorModificadores = 0;
		}

		hAnterior = h;
		vAnterior = v;

		correndo = false;
		
		if (Input.GetButton("B Button"))
			correndo = true;

		/*
		if (Input.GetButtonDown("B Button"))
		{
			jogadorAnimation.exibirArma = !jogadorAnimation.exibirArma;
			jogadorAnimation.exibirLanterna = false;
		}

		if (Input.GetButtonDown("X Button"))
		{
			jogadorAnimation.exibirLanterna = !jogadorAnimation.exibirLanterna;
			jogadorAnimation.exibirArma = false;
		}
		*/

		Animar();
		Mover();
	}

	void Mover()
	{
		if (!movimentoLiberado)
		{
			h = 0;
			v = 0;
		}

		h = Mathf.Abs(h) < 0.15f ? 0 : h;
		v = Mathf.Abs(v) < 0.15f ? 0 : v;

		float velocidade = (correndo ? velocidadeCorrendo : velocidadeAndando);
		
		if (h != 0 || v != 0)
		{
			rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			Rotacionar();
			transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
		}
		else
			rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}

	void Animar()
	{
		string animacao = "Idle";

		Vector3 posicaoAtual = transform.position;

		if (Vector3.Distance(posicaoAtual, posicaoAnterior) > 0f)
			animacao = (correndo ? "Run" : "Walk");

		jogadorAnimation.AtualizarAnimacao(animacao);

		posicaoAnterior = posicaoAtual;
	}

	void Rotacionar()
	{
		if (!Camera.main)
			return;

		Transform camera = Camera.main.transform;
		Quaternion quaternion = transform.rotation;
		float valorRotacaoY = transform.rotation.y;

		h *= modificadores.x;
		v *= modificadores.y;

		if (v > 0)
		{
			if (h > 0)
				valorRotacaoY = 45;
			else if (h < 0)
				valorRotacaoY = 305;
			else
				valorRotacaoY = 0;
		}
		else if (v < 0)
		{
			if (h > 0)
				valorRotacaoY = 135;
			else if (h < 0)
				valorRotacaoY = 225;
			else
				valorRotacaoY = 180;
		}
		else
		{
			if (h > 0)
				valorRotacaoY = 90;
			else if (h < 0)
				valorRotacaoY = 270;
		}

		if (movimentacaoBaseadaCamera)
			valorRotacaoY += (camera.eulerAngles.y < 180 ? camera.eulerAngles.y - 180 : camera.eulerAngles.y);

		Quaternion novaRotacao = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, valorRotacaoY, 0), 0.1f);

		transform.rotation = novaRotacao;
	}

	public void AlterarMovimento(bool estado)
	{
		movimentoLiberado = estado;
	}
}

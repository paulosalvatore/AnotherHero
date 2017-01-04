using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JogadorMovimento : MonoBehaviour
{
	public float velocidadeAndando;
	public float velocidadeCorrendo;

	private new Rigidbody rigidbody;
	private JogadorAnimation jogadorAnimation;

	private Vector3 posicaoAnterior;

	public Quaternion novaRotacao;

	private bool correndo;
	private bool exibirArma;

	internal bool mudancaLiberada = true;
	internal GameObject atual;

	internal Vector2 modificadores = new Vector2(1f, 1f);

	private float h = 0f;
	private float v = 0f;

	private float hAnterior = 0f;
	private float vAnterior = 0f;

	private int contadorModificadores = 0;
	private int limiteContadorModificadores = 5;

	private bool falandoComNpc = false;

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

		bool correndo = false;
		
		if (Input.GetButton("Run"))
			correndo = true;

		if (Input.GetKey(KeyCode.LeftControl))
			jogadorAnimation.exibirArma = true;

		Animar(v, correndo);
		Mover(h, v, correndo);
	}

	void Mover(float h, float v, bool correndo)
	{
		if (falandoComNpc)
			return;

		float velocidade = (correndo ? velocidadeCorrendo : velocidadeAndando);
		
		if (h != 0 || v != 0)
		{
			rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			Rotacionar(h, v);
			transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
		}
		else
			rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}

	void Animar(float v, bool correndo)
	{
		string animacao = "Idle";

		Vector3 posicaoAtual = transform.position;

		if (Vector3.Distance(posicaoAtual, posicaoAnterior) > 0f)
			animacao = (correndo ? (exibirArma ? "RunGun" : "Run") : "Walk");

		jogadorAnimation.AtualizarAnimacao(animacao);

		posicaoAnterior = posicaoAtual;
	}

	public bool testarMovimentacaoDiferente;

	void Rotacionar(float h, float v)
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

		if (testarMovimentacaoDiferente)
			valorRotacaoY += (camera.eulerAngles.y < 180 ? camera.eulerAngles.y - 180 : camera.eulerAngles.y);

		novaRotacao = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, valorRotacaoY, 0), 0.1f);

		transform.rotation = novaRotacao;
	}

	public void AlterarFalandoComNpc(bool estado)
	{
		falandoComNpc = estado;
	}

	void OnGUI()
	{
		string texto = "";
		texto += string.Format("h: {0}\n", h);
		texto += string.Format("v: {0}\n", v);
		GUI.Label(new Rect(10, 10, 100, 2000), texto);
	}
}

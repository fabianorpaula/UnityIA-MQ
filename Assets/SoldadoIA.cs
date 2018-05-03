using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldadoIA : MonoBehaviour {

    /// <summary>
    /// Dados
    /// </summary>
    public int MeuLife = 0;
    public int MeuDano = 0;
    public int MeuVisao = 0;
    public int MeuAlcance = 0;
    public int MeuVelocidade = 0;


    //OS ESTADOS
    //Descansar - Ronda - Segue - Ataque
    public enum Ordens { Descansa, Ronda, Segue, Ataque, Morto };
    //GameObjesc
    //Olho para fazer campo de visão
    public GameObject Olho;
    //Pontos de deslocamento
    public GameObject PontoA;
    public GameObject PontoB;
    //Inimigo para receber
    private GameObject Inimigo;
    //Disparo
    //prefab bala
    public GameObject capsula;
    //Ponto de Tiro
    public GameObject pontodetiro;
    //Caminhos
    public List<GameObject> Caminhos;
    public int destino_c = 0;
    /// <summary>
    /// As ordens
    /// </summary>
    public Ordens minhas_ordens;
    //Ações Animadas
    private Actions Acoes;
    /// <summary>
    /// Dano
    private Dano dano;
    /// </summary>


    public bool pos = false;
    //Para soldado Andar
    private NavMeshAgent Soldado;

    //Tempo para ficar parado ou andar
    public float tempo = 0;
    //Tempo para atirar
    public float tempo_at = 0;

    
    

	// Use this for initialization
	void Start () {
        int total_pos = Caminhos.Count - 1;
        destino_c = Random.Range(0, total_pos);



        transform.position = Caminhos[destino_c].transform.position;


        minhas_ordens = Ordens.Descansa;
        Soldado = GetComponent<NavMeshAgent>();

        ///Chamar Aram
        GetComponent<PlayerController>().SetArsenal("Rifle");

        Acoes = GetComponent<Actions>();
        //Acoes.Stay();
        //Acoes.Aiming();

        ///Dano
        dano = GetComponent<Dano>();
    }
	
	// Update is called once per frame
	void Update () {
        CumprirOrdens();
        if(dano.InformarStatus())
        {
            minhas_ordens = Ordens.Morto;
        }
	}

    void CumprirOrdens()
    {
        if(minhas_ordens == Ordens.Descansa)
        {
            //Para Buscar inimigo
            Buscar();
            //Tempo parado
            tempo += Time.deltaTime;
            Soldado.speed = 0;
            if (tempo > 10)
            {
                tempo = 0;
                minhas_ordens = Ordens.Ronda;
                //Correr
                Acoes.Run();
            }
            
        }
        if(minhas_ordens == Ordens.Ronda)
        {
            //Para buscar inimigo
            Buscar();
            //Tempo andando
            Soldado.speed = 5 + MeuVelocidade;
            tempo += Time.deltaTime;
            if(tempo > 30)
            {
                tempo = 0;
                minhas_ordens = Ordens.Descansa;
                //Parar
                Acoes.Stay();
            }
            
            
                int destinof = destino_c + 1;
                // transform.position = Vector3.MoveTowards(this.transform.position, PontoA.transform.position, 0.5f);
                Soldado.SetDestination(Caminhos[destinof].transform.position);
                if (Vector3.Distance(transform.position, Caminhos[destinof].transform.position) < 2)
                {
                    destino_c = destino_c + 1;
                    if(destino_c >= Caminhos.Count-1)
                    {
                        destino_c = 0;
                    }
                }
            
        }
        if(minhas_ordens == Ordens.Segue)
        {
            Soldado.speed = 5 + MeuVelocidade;
            Soldado.SetDestination(Inimigo.transform.position);
            if(Vector3.Distance(transform.position, Inimigo.transform.position) < 5)
            {
                minhas_ordens = Ordens.Ataque;
                Acoes.Aiming();
                
            }
            
        }


        if(minhas_ordens == Ordens.Ataque)
        {
            Soldado.speed = 0;
            transform.LookAt(Inimigo.transform.position);
            if (Vector3.Distance(transform.position, Inimigo.transform.position) > (6+MeuAlcance))
            {
                minhas_ordens = Ordens.Segue;
                Acoes.Run();

            }else
            {
                tempo_at += Time.deltaTime;
                if (tempo_at > 1)
                {
                    Atirar();
                    tempo_at = 0;
                }
            }

            if (Inimigo.GetComponent<Dano>().InformarStatus())
            {
                minhas_ordens = Ordens.Ronda;
                Acoes.Run();
                Destroy(Inimigo, 5f);
            }
            

        }

        if (minhas_ordens == Ordens.Morto)
        {
            Soldado.speed = 0;
        }

        }



    ////Visão -> PARADO - RONDA
    void Buscar()
    {
        if(Olho.GetComponent<Visao>().Avistou() == true)
        {
            minhas_ordens = Ordens.Segue;
            Inimigo = Olho.GetComponent<Visao>().Inimigo;
        }


    }

    void Atirar()
    {
        
        Acoes.Attack();
        GameObject Bala = Instantiate(capsula, pontodetiro.transform.position, Quaternion.identity);
        Bala.GetComponent<Rigidbody>().AddForce(pontodetiro.transform.forward*150);
        Bala.GetComponent<Tiro>().Dano = 1 + MeuDano;
        Destroy(Bala, 5f);
        
    }



    }

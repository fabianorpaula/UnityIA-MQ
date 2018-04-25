using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldadoIA : MonoBehaviour {

    //OS ESTADOS
    //Descansar - Ronda - Segue - Ataque
    public enum Ordens { Descansa, Ronda, Segue, Ataque };
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

    /// <summary>
    /// As ordens
    /// </summary>
    public Ordens minhas_ordens;
    //Ações Animadas
    private Actions Acoes;


    public bool pos = false;
    //Para soldado Andar
    private NavMeshAgent Soldado;

    //Tempo para ficar parado ou andar
    public float tempo = 0;
    //Tempo para atirar
    public float tempo_at = 0;

    
    

	// Use this for initialization
	void Start () {
        float posx = Random.Range(5, 300);
        float posy = 0.55f;
        float posz = Random.Range(5, 300);

        transform.position = new Vector3(posx, posy, posz);


        minhas_ordens = Ordens.Descansa;
        Soldado = GetComponent<NavMeshAgent>();

        ///Chamar Aram
        GetComponent<PlayerController>().SetArsenal("Rifle");

        Acoes = GetComponent<Actions>();
        //Acoes.Stay();
        //Acoes.Aiming();
    }
	
	// Update is called once per frame
	void Update () {
        CumprirOrdens();
        
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
            Soldado.speed = 5;
            tempo += Time.deltaTime;
            if(tempo > 30)
            {
                tempo = 0;
                minhas_ordens = Ordens.Descansa;
                //Parar
                Acoes.Stay();
            }


            if (pos)
            {
                // transform.position = Vector3.MoveTowards(this.transform.position, PontoA.transform.position, 0.5f);
                Soldado.SetDestination(PontoA.transform.position);
                if (Vector3.Distance(transform.position, PontoA.transform.position) < 2)
                {
                    pos = false;
                }
            }else
            {
                Soldado.SetDestination(PontoB.transform.position);
                if (Vector3.Distance(transform.position, PontoB.transform.position) < 2)
                {
                    pos = true;
                }
            }
           
            
        }
        if(minhas_ordens == Ordens.Segue)
        {
            Soldado.speed = 5;
            Soldado.SetDestination(Inimigo.transform.position);
            if(Vector3.Distance(transform.position, Inimigo.transform.position) < 5)
            {
                minhas_ordens = Ordens.Ataque;
                Acoes.Aiming();
                
            }
            //Acoes.Attack();
            
        }


        if(minhas_ordens == Ordens.Ataque)
        {
            Soldado.speed = 0;
            transform.LookAt(Inimigo.transform.position);
            if (Vector3.Distance(transform.position, Inimigo.transform.position) > 6)
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
            Destroy(Bala, 5f);
        
    }



    }

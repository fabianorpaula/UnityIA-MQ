using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldadoIA : MonoBehaviour {


    public enum Ordens { Descansa, Ronda, Segue, Ataque };
    public GameObject Olho;
    public Ordens minhas_ordens;
    public GameObject PontoA;
    public GameObject PontoB;
    public bool pos = false;
    private NavMeshAgent Soldado;
    public float tempo = 0;
    private GameObject Inimigo;
    private Actions Acoes;

	// Use this for initialization
	void Start () {
        minhas_ordens = Ordens.Descansa;
        Soldado = GetComponent<NavMeshAgent>();
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
            Buscar();
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
            Buscar();
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
            if (Vector3.Distance(transform.position, Inimigo.transform.position) > 6)
            {
                minhas_ordens = Ordens.Segue;
            }
        }

    }



    ////Visão
    void Buscar()
    {
        if(Olho.GetComponent<Visao>().Avistou() == true)
        {
            minhas_ordens = Ordens.Segue;
            Inimigo = Olho.GetComponent<Visao>().Inimigo;
        }


    }



    }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldadoIA : MonoBehaviour {


    public enum Ordens { Descansa, Ronda, Segue };
    public GameObject Olho;
    public Ordens minhas_ordens;
    public GameObject PontoA;
    public GameObject PontoB;
    public bool pos = false;
    private NavMeshAgent Soldado;
    public float tempo = 0;
    private GameObject Inimigo;
	// Use this for initialization
	void Start () {
        minhas_ordens = Ordens.Descansa;
        Soldado = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        CumprirOrdens();
        Avistou();
	}

    void CumprirOrdens()
    {
        if(minhas_ordens == Ordens.Descansa)
        {
            tempo += Time.deltaTime;
            Soldado.speed = 0;
            if (tempo > 10)
            {
                tempo = 0;
                minhas_ordens = Ordens.Ronda;
            }
        }
        if(minhas_ordens == Ordens.Ronda)
        {
            Soldado.speed = 5;
            tempo += Time.deltaTime;
            if(tempo > 30)
            {
                tempo = 0;
                minhas_ordens = Ordens.Descansa;
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
        }

    }



    ////Visão
    void Avistou()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(Olho.transform.position, transform.forward, out hit, 10.0f))
        {
            Debug.DrawLine(Olho.transform.position, hit.point);
            if (hit.collider.gameObject.tag == "Soldado")
            {
                Debug.Log("Avistou");
                Inimigo = hit.collider.gameObject;
                minhas_ordens = Ordens.Segue;
            }
        }
    }



    }

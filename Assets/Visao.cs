using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visao : MonoBehaviour {

    /// <summary>
    /// Game Object Empty Na Frente da Cabeça do Jogador, com uma animação de movimentação
    /// </summary>


    public GameObject Inimigo;
    public GameObject Soldado;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

    }
    public bool Avistou()
    {
        RaycastHit hit;
        int alcance = 10 + Soldado.GetComponent<SoldadoIA>().MeuVisao;
        if (Physics.Raycast(transform.position, transform.forward, out hit, alcance))
        {
            //Debug.DrawLine(transform.position, hit.point, Color.red);
            //Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Soldado")
            {
                
                Inimigo = hit.collider.gameObject;
                //Informar que avistou algo
                return true;
            }
            else
            {
                return false;
            }
        }else
        {
            return false;
        }
    }
}

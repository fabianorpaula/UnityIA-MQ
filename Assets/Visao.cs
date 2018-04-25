using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visao : MonoBehaviour {

    /// <summary>
    /// Game Object Empty Na Frente da Cabeça do Jogador, com uma animação de movimentação
    /// </summary>


    public GameObject Inimigo;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

    }
    public bool Avistou()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.Log(hit.collider.gameObject.tag);
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

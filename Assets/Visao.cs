using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visao : MonoBehaviour {

    public GameObject Inimigo;
    //private bool avistou = false;

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
            if (hit.collider.gameObject.tag == "Soldado")
            {
                Debug.Log("Avistou");
                Inimigo = hit.collider.gameObject;
                //minhas_ordens = Ordens.Segue;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano : MonoBehaviour {
    //Vida
    int life = 10;
    bool morto = false;
    //Ações Animadas
    private Actions Acoes;
    // Use this for initialization
    void Start () {
        Acoes = GetComponent<Actions>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        
        //Debug.Log(col.gameObject.tag);
        if(col.gameObject.tag == "Bala" && morto == false)
        {
            
                life--;
                Destroy(col.gameObject);
                Acoes.Damage();
                if (life == 0)
                {

                    //Debug.Log("Morreu");
                    Acoes.Death();
                    morto = true;
                GetComponent<BoxCollider>().enabled = false;
                }
            
        }

        
    }

    public bool InformarStatus()
    {
        return morto;
    }

}

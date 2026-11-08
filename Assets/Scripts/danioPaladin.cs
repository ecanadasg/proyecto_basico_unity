using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class danioPaladin : MonoBehaviour
{
    public AudioSource sourceEspada;
    public AudioClip golpeAEnemigo;
    public int danioAtaque = 35;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemigo"))
        {
            other.GetComponent<LogicaEnemigo>().vida -= danioAtaque;   
            other.GetComponent<Animator>().SetBool("Impact", true);
            sourceEspada.clip = golpeAEnemigo;
            sourceEspada.Play();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class danioEnemigo : MonoBehaviour
{
    public AudioSource sourceManoEnemigo;
    public AudioClip golpeEscudo;
    public AudioClip golpeAPlayer;
    public int danioAtaque = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Escudo"))
        {
            this.GetComponent<Collider>().enabled = false;
            GameObject.Find("Paladin WProp J Nordstrom").GetComponent<Animator>().SetBool("ImpactoEscudo", true);
            sourceManoEnemigo.clip = golpeEscudo;
            sourceManoEnemigo.Play();

        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<LogicaPaladin>().vida -= danioAtaque;
            other.GetComponent<Animator>().SetBool("Impact", true);
            sourceManoEnemigo.clip = golpeAPlayer;
            sourceManoEnemigo.Play();

        }
    }
}

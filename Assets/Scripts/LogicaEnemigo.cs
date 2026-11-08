using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaEnemigo : MonoBehaviour
{

    private Animator anim;
    public float VelMovimiento = 2.5f;
    public float VelRotacion = 300.0f;
    public float crono = 0f;
    private float cronoMuerte = 0f;
    public float grados = 45f;
    public int vida = 100;
    public Slider sliderVida;
    public Transform target;
    public GameObject player;
    private Vector3 direccion;
    private Quaternion rotation;
    public GameObject punio;
    public AudioSource source;
    public AudioClip muerteZombie;
    
    



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        sliderVida.GetComponent<Slider>().value = vida;

        if(player.GetComponent<LogicaPaladin>().vida <= 0)
        {
            anim.SetBool("Perseguir", false);
            anim.SetBool("Atacar", false);
            anim.SetBool("Deambular", true);
            anim.SetBool("Death", false);
            punio.GetComponent<Collider>().enabled = false;

            crono += Time.deltaTime;
            VelMovimiento = 2.5f;
            transform.Translate(0, 0, Time.deltaTime * VelMovimiento);

            if ((crono > 4) && (crono < 5))
            {
                transform.Rotate(0, 90 * Time.deltaTime, 0);
            }

            if (crono >= 5)
            {
                reiniciarCrono();
            }

            
        }
        else
        {
            if (vida <= 0)
            {
                source.Stop();
                source.clip = muerteZombie;
                source.loop = false;
                source.playOnAwake = false;
                source.Play();
                anim.SetBool("Perseguir", false);
                anim.SetBool("Atacar", false);
                anim.SetBool("Deambular", false);
                anim.SetBool("Death", true);
                GetComponent<AudioSource>().Stop();
                cronoMuerte += Time.deltaTime;

                if(cronoMuerte > 7)
                {
                    Destroy(gameObject);
                }
                
                


            }
            else
            {

                if (anim.GetBool("Deambulando") == true) //deambulando
                {
                    crono += Time.deltaTime;
                    transform.Translate(0, 0, Time.deltaTime * VelMovimiento);

                    if ((crono > 4) && (crono < 5))
                    {
                        transform.Rotate(0, 90 * Time.deltaTime, 0);
                    }

                    if (crono >= 5)
                    {
                        reiniciarCrono();
                    }

                    if (Vector3.Distance(this.transform.position, target.position) <= 6)
                    {
                        anim.SetBool("Deambulando", false);
                        anim.SetBool("Perseguir", true);

                    }
                }
                else
                {

                    if (Vector3.Distance(this.transform.position, target.position) <= 1.5) //ataque
                    {
                        anim.SetBool("Perseguir", false);
                        anim.SetBool("Atacar", true);

                    }
                    else if (anim.GetBool("Perseguir") == true) //persecución
                    {

                        VelMovimiento = 4.5f;
                        transform.Translate(0, 0, Time.deltaTime * VelMovimiento);

                        direccion = target.position - transform.position; //obtenemos el vector resultante de la resta de las 2 posiciones para saber a que dirección tiene que mirar
                        rotation = Quaternion.LookRotation(direccion);  //LookRotation nos proporciona la rotación necesaria para que el enemigo mire en la dirección del vector
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, VelRotacion * Time.deltaTime); //aplicamos esa rotación al enemigo

                    }
                }

            }
        }
        

        



    }

    private void OnCollisionEnter(Collision collision)
    {
            if(collision.gameObject.CompareTag("Player"))
            {
            this.vida -= 10;
            }
    }


    void reiniciarCrono()
    {
        crono = 0f;
    }

  
    void finalizarAtaque()
    {
        if (Vector3.Distance(this.transform.position, target.position) > 1.5) //si al finalizar el ataque el jugador se ha separado, volvemos a la persecución, sino seguimos atacando
        {
            anim.SetBool("Atacar", false);
            anim.SetBool("Perseguir", true);
           
        }
    }

    void finImpacto()
    {
        anim.SetBool("Impact", false);
        anim.SetBool("Atacar", false);
        anim.SetBool("Perseguir", true);
    }

    void activarColliderAtaque()
    {
        punio.GetComponent<Collider>().enabled = true;
    }

    void desactivarColliderAtaque()
    {
        punio.GetComponent<Collider>().enabled = false;
    }

    

}

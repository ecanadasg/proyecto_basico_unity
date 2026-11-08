using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicaPaladin : MonoBehaviour
{
    //jugador
    public int vida = 100;
    public float x, y;
    public float jumpForce;
    public float VelMovimiento = 5.0f;
    public float VelRotacion = 200.0f;
    public float rotMouseX;
    public float rotX;
    public float maxAngle = 45f;
    public float minAngle = -45f;

    public GameObject espada;
    public AudioClip silbidoEspada;
    public GameObject escudo;
    public Slider sliderVida;
    private Animator anim;
    private Rigidbody rigid;
    public TextMeshProUGUI numberVida;


    
    
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        sliderVida.GetComponent<Slider>().value = vida;
        numberVida.text = vida.ToString(); 

        if(vida <= 0)
        {
            anim.SetBool("Defender", false);
            anim.SetBool("Atacar", false);
            anim.SetBool("OnGround", false);
            anim.SetBool("Saltando", false);
            anim.SetBool("Death", true);
        }
        else
        {

            
            //para la animación de salto
            if (anim.GetBool("onGround") == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetBool("Saltando", true);
                    rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }

            }



            //para atacar
            if ((Input.GetMouseButtonDown(0) == true) && (anim.GetBool("Atacar") == false) && (anim.GetBool("Defender") == false))
            {

                anim.SetBool("Atacar", true);

                //para que la animación empiece desde el principio
                anim.Play("Stable Sword Outward Slash", -1, 0f);
                espada.GetComponent<AudioSource>().clip = silbidoEspada;
                espada.GetComponent<AudioSource>().Play();


            }

            //para defenderse
            if ((Input.GetMouseButton(1) == true) && (anim.GetBool("Atacar") == false))
            {

                anim.SetBool("Defender", true);
                escudo.GetComponent<Collider>().enabled = true;

            }
            else
            {
                anim.SetBool("Defender", false);
                escudo.GetComponent<Collider>().enabled = false;
            }


            //movimiento del personaje
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
            transform.Translate(x * Time.deltaTime * VelMovimiento, 0, 0);
            transform.Translate(0, 0, y * Time.deltaTime * VelMovimiento);
            anim.SetFloat("VelX", x);
            anim.SetFloat("VelY", y);


            //rotación de la camara y personaje
            rotMouseX = Input.GetAxis("Mouse X");

            rotX = rotMouseX * Time.deltaTime * VelRotacion;
            transform.Rotate(0, rotX, 0);



        
        }






    }

  
    //se refieren al trigger del GameObject triggerPies
    private void OnTriggerStay(Collider other)
    {
        anim.SetBool("onGround", true);
        anim.SetBool("Saltando", false);
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("onGround", false);
        

    }

    void finImpacto()
    {
        anim.SetBool("Impact", false);
    }

    void finImpactoEscudo()
    {
        anim.SetBool("ImpactoEscudo", false);
    }

    void finalizarAtaque()
    {
        
        anim.SetBool("Atacar", false);
        
    }

    void activarColliderAtaque()
    {
        espada.GetComponent<Collider>().enabled = true;
    }

    void desactivarColliderAtaque()
    {
        espada.GetComponent<Collider>().enabled = false;
    }






}

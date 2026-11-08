using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ItemsVidas : MonoBehaviour
{

    public GameObject player;
    private Camera mainCamera;
    public int puntosSalud;
    private Vector3 direccion;
    private Quaternion rotation;
    private float VelRotacion = 200f;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        direccion = mainCamera.GetComponent<Transform>().position - transform.position; //obtenemos el vector resultante de la resta de las 2 posiciones para saber a que dirección tiene que mirar
        rotation = Quaternion.LookRotation(direccion);  //LookRotation nos proporciona la rotación necesaria para que el enemigo mire en la dirección del vector
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, VelRotacion * Time.deltaTime); //aplicamos esa rotación al enemigo
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("Player")) && (player.GetComponent<LogicaPaladin>().vida < 100))
        {
            if(player.GetComponent<LogicaPaladin>().vida <= 90)
            {
                player.GetComponent<LogicaPaladin>().vida += puntosSalud;
                Destroy(gameObject);
            }
            else
            {
                player.GetComponent<LogicaPaladin>().vida = 100;
                Destroy(gameObject);
            }
            
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderVidaEnemigo : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cameraMain;
    private Vector3 direccion;
    private Quaternion rotation;
    private float VelRotacion = 200f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direccion = cameraMain.GetComponent<Transform>().position - transform.position; //obtenemos el vector resultante de la resta de las 2 posiciones para saber a que dirección tiene que mirar
        rotation = Quaternion.LookRotation(direccion);  //LookRotation nos proporciona la rotación necesaria para que el enemigo mire en la dirección del vector
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, VelRotacion * Time.deltaTime); //aplicamos esa rotación al slider de la vida del enemigo
    }
}


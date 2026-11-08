using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FinDelJuego : MonoBehaviour
{
    public Slider slider;
    public GameObject panelVictoria;
    public GameObject panelMision;
    public GameObject panelDerrota;
    public GameObject botonVolverAJugar;
    public GameObject botonSalir;

    private GameObject[] cantidadEnemigos;
    private GameObject player;

    private float crono = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        crono += Time.deltaTime;
        if((crono > 2f)&&(crono < 7f))
        {
            panelMision.SetActive(true);
        }
        else
        {
            panelMision.SetActive(false);
        }

        cantidadEnemigos = GameObject.FindGameObjectsWithTag("Enemigo");

        if(cantidadEnemigos.Length == 0)
        {
            panelVictoria.SetActive(true);
            botonVolverAJugar.SetActive(true);
            botonSalir.SetActive(true);
            player.GetComponent<Animator>().SetBool("Celebration", true);
            player.GetComponent<LogicaPaladin>().enabled = false;

        }

        if(player.GetComponent<LogicaPaladin>().vida <= 0) 
        {
            panelDerrota.SetActive(true);
            botonVolverAJugar.SetActive(true);
            botonSalir.SetActive(true);
        }
    }

    public void volverAJugar()
    {
        SceneManager.LoadScene(0);
    }
    public void salirDelJuego()
    {
        Application.Quit();
    }
}

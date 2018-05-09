using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuracion_boton_movimiento : MonoBehaviour {

    public void Mostrar_ocultar_elementos_canvas()
    {
        Rerencias_hijos_canvas_menu canvas_menu = GameObject.Find("Canvas Menu").GetComponent<Rerencias_hijos_canvas_menu>();
        //ocultar
        canvas_menu.Panel.SetActive(false);
        canvas_menu.Button_Seleccionar_Movimiento.SetActive(false);
        canvas_menu.Button_Grabar_Movimientol.SetActive(false);
        canvas_menu.Grabacion.SetActive(false);
        canvas_menu.Lista_de_movimientos.SetActive(false);
        canvas_menu.Scrollbar.SetActive(false);
        canvas_menu.Menu.SetActive(true);
        canvas_menu.Menu_off.SetActive(false);
        canvas_menu.Exit.SetActive(false);
        canvas_menu.titulo_movimiento.SetActive(false);

        //mostrar
        ref_hijos canvas_iteracion = GameObject.Find("Canvas iteración").GetComponent<ref_hijos>();
        canvas_iteracion.button_Grabar.SetActive(false);
        canvas_iteracion.button_Cambiar_movimiento.SetActive(true);
        canvas_iteracion.button_Comparar.SetActive(true);
        canvas_iteracion.button_Play.SetActive(true);

          

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

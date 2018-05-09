using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivarBotonAnalisis : MonoBehaviour {

    public GameObject boton_analizar;

    public Registrar_Movimientos movimientos;
	// Use this for initialization


    public void activar_boton()
    {
        if(movimientos.elementos() >1)
        this.boton_analizar.GetComponent<Button>().interactable = true;
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

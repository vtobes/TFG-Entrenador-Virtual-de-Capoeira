using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rerencias_hijos_canvas_menu : MonoBehaviour {
    public GameObject _Panel;

    public GameObject Panel
    {
        get {return _Panel; }
    }
    public GameObject _Button_Grabar_Movimientol;

    public GameObject Button_Grabar_Movimientol
    {
        get { return _Button_Grabar_Movimientol; }
    }
    public GameObject _Button_Seleccionar_Movimiento;

    public GameObject Button_Seleccionar_Movimiento
    {
        get { return _Button_Seleccionar_Movimiento; }
    }
    public GameObject _Grabacion;

    public GameObject Grabacion
    {
        get { return _Grabacion; }
    }
    public GameObject _Scrollbar;

    public GameObject Scrollbar
    {
        get { return _Scrollbar; }
    }
    public GameObject _Lista_de_movimientos;

    public GameObject Lista_de_movimientos
    {
        get { return _Lista_de_movimientos; }
    }
    public GameObject _Menu;

    public GameObject Menu
    {
        get { return _Menu; }
    }
    public GameObject _Menu_off;

    public GameObject Menu_off
    {
        get { return _Menu_off; }
    }


	public GameObject _Menu_Principal;

	public GameObject Menu_Principal{
	
		get{ return _Menu_Principal;}
	}

    public GameObject _Exit;

    public GameObject Exit
    {
        get { return _Exit; }
    }


    public GameObject _titulo_movimiento;

    public GameObject titulo_movimiento
    {
        get { return _titulo_movimiento; }
    }


    // Use this for initialization


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comprobación_funcional : MonoBehaviour {

    public GameObject boton_grabar;
    private bool _profesor;

    public bool profesor
    {
        get { return _profesor; }
        set { _profesor = value; }
    }

    public void mostrat_boton_grabar()
    {
        if (_profesor)
            boton_grabar.SetActive(true);
        else
            boton_grabar.SetActive(false);
    }
 
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

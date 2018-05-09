using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posicion_inicial : MonoBehaviour {

    Vector3 _posicion_incial;
    Quaternion _rotación_incial;
    public Quaternion rotación_incial
    {
        get { return _rotación_incial; }
    }

    public Vector3 posicion_incial
    {
        get {return _posicion_incial; }
    }
	// Use this for initialization

    

	void Start () {
        _posicion_incial=transform.position;
        _rotación_incial = transform.rotation;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

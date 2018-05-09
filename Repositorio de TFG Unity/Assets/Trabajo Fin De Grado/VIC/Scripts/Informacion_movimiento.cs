using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Informacion_movimiento : MonoBehaviour {
    public string Movimiento = "";
    

    public void SetMovimiento(string name)
    {
        Movimiento = name;
    } 

    private KinectRecorderPlayer recorder = null;
    

    public void ChangeFile()
    {
        recorder = KinectRecorderPlayer.Instance;
        //recorder.filePath = "Assets/Resources/Movimientos/" + Movimiento + ".txt";
        recorder.filePath = Movimiento;
    }

    public void NewFile()
    {
             
        Movimiento = GameObject.Find("Nombre Fichero Grabar").GetComponent<Text>().text;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

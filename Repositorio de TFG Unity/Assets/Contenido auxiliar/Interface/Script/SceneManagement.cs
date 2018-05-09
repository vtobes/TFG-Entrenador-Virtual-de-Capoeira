using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour {


	public void cambiarScena(string nombre){
		print ("cambiando de escena");
		SceneManager.LoadScene (nombre);
		
	}


	public void salir(){
	
		Application.Quit ();
	}




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

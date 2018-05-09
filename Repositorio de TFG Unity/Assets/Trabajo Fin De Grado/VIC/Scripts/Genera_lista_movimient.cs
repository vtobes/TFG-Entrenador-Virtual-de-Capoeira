using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public class Genera_lista_movimient : MonoBehaviour {
    public GameObject boton_movimiento;

    public void GenerarLista()
    {
        //borramos todos los hijos
        foreach (Transform child in gameObject.transform)
        {

            GameObject.Destroy(child.gameObject);
        }
        //AssetDatabase.Refresh();
        Object[] movimientos = Resources.LoadAll("Movimientos");
        int totalMapCount = movimientos.Length;

        for (int i = 0; i < movimientos.Length; i++)
        {


            GameObject clone = (GameObject)Instantiate(boton_movimiento);
            clone.GetComponent<Informacion_movimiento>().SetMovimiento(movimientos[i].name);
            clone.GetComponentInChildren<UnityEngine.UI.Text>().text = movimientos[i].name;

            clone.transform.SetParent(gameObject.transform);
            
            
           

        }
        Resources.UnloadUnusedAssets();


    }
     

    // Use this for initialization
    void Start () {

        GenerarLista();
    }

    // Update is called once per frame
    void Update () {

    }
   
}

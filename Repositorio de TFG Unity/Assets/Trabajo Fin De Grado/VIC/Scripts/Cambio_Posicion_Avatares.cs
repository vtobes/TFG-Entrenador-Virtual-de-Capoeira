using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cambio_Posicion_Avatares : MonoBehaviour {
    public GameObject Avatar1;
    public GameObject Avatar2;

    public GameObject Avatar1_Material;
    public GameObject Avatar2_Material;

    public Material MaterialOpaco;
    public Material MaterialTransparente;
    public Material MaterialTop;
    public Material MaterialBot;

    // Use this for initialization


    //Cambio de materiales y GameObjects para el analisis
    public GameObject EyesPlayer;
    public GameObject EyesLashesPlayer;
    public GameObject HairPlayer;
    public GameObject TopsPlayer;
    public GameObject BotPlayer;




    public GameObject EyesRecorder;
    public GameObject EyesLashesRecorder;
    public GameObject HairRecorder;
    public GameObject TopsRecorder;
    public GameObject BotRecorder;


    public void CambiarPosiciones()
    {

        //Avatar1.GetComponent<Animation_controller>().enabled = true;

        //Avatar1.GetComponent<Animator>().enabled = false;
        Avatar1.GetComponent<Animator>().Rebind();
        Avatar1.GetComponent<AvatarController>().setinitialPosition = new Vector3(-1.781f, 0.366f, 2.61340f);
        Avatar1.GetComponent<AvatarController>().ResetToInitialPosition();
        Avatar1.GetComponent<AvatarController>().GetInitialRotations();
        Avatar1.GetComponent<huesitos>().Show_bones = true ;
        //ChangeRenderMode(AvatarMaterial1.GetComponent<Renderer>().material, BlendMode.Transparent);
        Avatar1_Material.GetComponent<Renderer>().material = MaterialTransparente;
        EyesPlayer.SetActive(false);
        EyesLashesPlayer.SetActive(false);
        HairPlayer.SetActive(false);
        TopsPlayer.GetComponent<Renderer>().material = MaterialTransparente;
        BotPlayer.GetComponent<Renderer>().material = MaterialTransparente;


    //esqueleto1.transform.position= new Vector3(-1.781f, 0.366f, 2.61340f);


    //Avatar2.GetComponent<Animator>().enabled = false;s
    //Avatar2.GetComponent<Animation_controller>().enabled = false;
    Avatar2.GetComponent<Animator>().Rebind();
        Avatar2.GetComponent<AvatarController>().setinitialPosition = new Vector3(-0.341f, 0.366f, 2.61340f);
        Avatar2.GetComponent<AvatarController>().ResetToInitialPosition();
        Avatar2.GetComponent<AvatarController>().GetInitialRotations();
        Avatar2.GetComponent<huesitos>().Show_bones = true;
        //ChangeRenderMode(AvatarMaterial2.GetComponent<Renderer>().material, BlendMode.Transparent);
        // esqueleto2.transform.position = new Vector3(-0.341f, 0.366f, 2.61340f);
        Avatar2_Material.GetComponent<Renderer>().material = MaterialTransparente;
        EyesRecorder.SetActive(false);
        EyesLashesRecorder.SetActive(false);
        HairRecorder.SetActive(false);
        TopsRecorder.GetComponent<Renderer>().material = MaterialTransparente; ;
        BotRecorder.GetComponent<Renderer>().material = MaterialTransparente; ;


}

public void Posicion_Inicial()
    {

        Avatar1.GetComponent<Animation_controller>().enabled = true;

        Avatar1.GetComponent<Animator>().enabled = true;
        Avatar1.GetComponent<Animator>().Rebind();
        Avatar1.GetComponent<AvatarController>().setinitialPosition = Avatar1.GetComponent<Posicion_inicial>().posicion_incial;
        Avatar1.GetComponent<AvatarController>().ResetToInitialPosition();
        Avatar1.GetComponent<AvatarController>().GetInitialRotations();
        Avatar1.GetComponent<huesitos>().Show_bones = false;
        Avatar1_Material.GetComponent<Renderer>().material = MaterialOpaco;
        EyesPlayer.SetActive(true);
        EyesLashesPlayer.SetActive(true);
        HairPlayer.SetActive(true);
        TopsPlayer.GetComponent<Renderer>().material = MaterialTop;
        BotPlayer.GetComponent<Renderer>().material = MaterialBot;

        Avatar2.GetComponent<Animator>().enabled = true;
        Avatar2.GetComponent<Animation_controller>().enabled = true;
        Avatar2.GetComponent<Animator>().Rebind();
        Avatar2.GetComponent<AvatarController>().setinitialPosition = Avatar2.GetComponent<Posicion_inicial>().posicion_incial;
        Avatar2.GetComponent<AvatarController>().ResetToInitialPosition();
        Avatar2.GetComponent<AvatarController>().GetInitialRotations();
        Avatar2.GetComponent<huesitos>().Show_bones = false;
        Avatar2_Material.GetComponent<Renderer>().material = MaterialOpaco;
        EyesRecorder.SetActive(true);
        EyesLashesRecorder.SetActive(true);
        HairRecorder.SetActive(true);
        TopsRecorder.GetComponent<Renderer>().material = MaterialTop; ;
        BotRecorder.GetComponent<Renderer>().material = MaterialBot; ;


    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}

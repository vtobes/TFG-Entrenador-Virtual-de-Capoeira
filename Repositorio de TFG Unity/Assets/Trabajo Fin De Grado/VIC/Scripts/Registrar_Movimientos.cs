using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Registrar_Movimientos : MonoBehaviour {

    private List<string> list = new List<string>();
    // time variables used for recording and playing
    private long liRelTime = 0;
    private float fStartTime = 0f;
    private float fCurrentTime = 0f;

    private bool _Cambiar_color = true;

    public GameObject texto_pizarra;

    public bool Cambiar_color
    {
        get { return _Cambiar_color; }
        set { _Cambiar_color = value; }
    }

    private int _currentFrame = 0;
    public int currentFrame
    {
        get { return _currentFrame; }
        set { _currentFrame = value; }
    }

    public GameObject cubemanPay;
    public GameObject cubemanRecorder;

    public GameObject AvatarPlay;

    public Text Texto_correccion;

    //Estado en el que se encuentra el movimiento que ha fracasado 
    private int  _Estado_del_moviento_fracasado= 0;

    public int Estado_del_moviento_fracasado
    {
        get { return _Estado_del_moviento_fracasado; }
        set { _Estado_del_moviento_fracasado = value; }
    }
    
    KinectManager manager;
    private bool empezar_registro = true;

    //para resetear los movimientos del usuario registrado
    public void BorrarRegistro()
    {
        _Estado_del_moviento_fracasado = 0;
        list.Clear();
        _currentFrame = 0;
        empezar_registro = true;
        _Cambiar_color = true;
        Texto_correccion.text = "";
    }

    public int elementos()
    {
        return this.list.Count;
    }

    //registra movimientos del usuario
    public void registrarMovimiento()
    {

        if (empezar_registro)
        {
            fStartTime = fCurrentTime = Time.time;
            empezar_registro = false;
        }
        const char delimiter = ',';
        string sBodyFrame = manager.GetBodyFrameData(ref liRelTime, ref fCurrentTime, delimiter);

        if (sBodyFrame != "")
        {
            string sRelTime = string.Format("{0:F3}", (fCurrentTime - fStartTime));

            string FullBodyFrame = sRelTime + "|" + sBodyFrame;



            list.Add(FullBodyFrame);
        }
    }

    public string Obtener_frame_body()
    {
        string line = list[_currentFrame];
        if (_currentFrame < _Estado_del_moviento_fracasado)
            _currentFrame++;

        if((_currentFrame == _Estado_del_moviento_fracasado) && (_Cambiar_color))
        { 
                Mostrar_Errores_Movimiento();
               // _Cambiar_color = false;
        
        }
        return line;
       
    }

    private void Mostrar_Errores_Movimiento()
    {
        int i = 1;
        Gesture_Action mov = Gesture_Action.Instance;
        AvatarPlay.GetComponent<huesitos>().Reset_Color_bones();
        this.texto_pizarra.GetComponent<TextMeshProUGUI>().text  = "";

        List<string> texto = new List<string>();
        while (i < 25)
        {
            Vector3 vectorplay = cubemanPay.GetComponent<CubemanController>().GetBones()[i].transform.localPosition;
            Vector3 vectorcord = cubemanRecorder.GetComponent<CubemanController>().GetBones()[i].transform.localPosition;

            float playx = vectorplay.x;
            float playy = vectorplay.y;
            float playz = vectorplay.z;


            float cordx = vectorcord.x;
            float cordy = vectorcord.y;
            float cordz = vectorcord.z;



            bool inpose = ((playx <= (cordx + mov.Error_Margin)) && (playx >= (cordx - mov.Error_Margin))) &&
                     ((playy <= (cordy + mov.Error_Margin)) && (playy >= (cordy - mov.Error_Margin))) &&
                     ((playz <= (cordz + mov.Error_Margin)) && (playz >= (cordz - mov.Error_Margin)));

            //huesito rojo  y logica de corrección de movimiento
            if (!inpose)

            {
                AvatarPlay.GetComponent<huesitos>().Paint_Bone_red(i);
                string correccion = "\u2022<indent=3.5em>Mueve  <#F74A05>" + BonetoString[i] + "</color> hacia la ";
                //Se identifica si es derecha o izquierda

                if ((playx - cordx) > 0)
                    //moverse a la izquierda
                    correccion += " <#16B42B>derecha </color>  y ";

               
                else
                    //moverse a la derecha
                    correccion += " <color=red>izquierda </color> y ";

                //Se identifica hacia delante o hacia atás

                if ((playz - cordz) > 0)
                    //abajo
                    correccion += "<#10B9F6>atrás </color>";
                else

                    //arriba
                    correccion += "<color=purple>adelante </color>";
                //Se identifica arriba o abajo

                if ((playy - cordy) > 0)
                    //abajo
                    correccion += "acompañado de un movimiento<#FC0EF4> descendente.</color></indent>";
                else
                    //arriba
                    correccion += "acompañado de un movimiento<color=blue> ascendente. </color></indent>";



                Debug.Log(correccion);

                if (!texto.Contains(correccion))
                    texto.Add(correccion);

            }

            i++;
        }

        for (int j = 0; j < texto.Count; j++)
            //Texto_correccion.text += texto[j] + "\n";
            this.texto_pizarra.GetComponent<TextMeshProUGUI>().text += texto[j] + "\n";
        if (texto.Count == 0)
            //Texto_correccion.text = "Error en calibración de movimiento intentelo de nuevo." + "\n";
        this.texto_pizarra.GetComponent<TextMeshProUGUI>().text = "Error en calibración de movimiento intentelo de nuevo." + "\n";
    }
    // Use this for initialization
    void Start () {
        manager = KinectManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
      
	}
    
    protected static readonly Dictionary<int, string> BonetoString = new Dictionary<int, string>
    {
        {0,"la cadera"},
        {1,"el pecho"},
        {2,"el pecho"},
        {3, "el cuello"},
        {4 ,"la cabeza"},

        { 5,"el hombro derecho"},
        { 6 ,"el brazo derecho"},
        { 7,"el brazo derecho"},
        { 8,"el brazo derecho"},


        { 9, "el hombro izquierdo"},
        { 10 ,"el brazo izquierdo"},
        { 11, "el brazo izquierdo"},
        { 12, "el brazo izquierdo"},

        { 13 , "la cadera"},
        { 14,"la rodilla derecha"},


        {15,"el pie derecho"},
        {16,"el pie derecho"},

        { 17 , "la cadera"},
        { 18,"la rodilla izquierda"},


        {19,"el pie izquierdo"},
        {20,"el pie izquierdo"},

        { 21,"la mano derecha"},
        { 22,"la mano derecha"},
        { 23,"la mano izquierda" },
        { 24,"la mano izquierda"},
    };
}

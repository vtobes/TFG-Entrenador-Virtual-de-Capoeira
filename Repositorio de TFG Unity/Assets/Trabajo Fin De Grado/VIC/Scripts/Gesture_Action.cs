using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Text;

public class Gesture_Action : MonoBehaviour {
    private ArrayList movimientos = new ArrayList();
    private static Gesture_Action instance = null;
    [Tooltip("GUI-Text to display information messages.")]
    public GUIText _infoText_Movimiento;

    public GameObject texto_calibrar;

    public GUIText infoText_Movimiento
    {
        get { return _infoText_Movimiento; }
        set { _infoText_Movimiento = value; }
    }

    public enum Dificultad : int { Facil = 0, Media = 1, Dificil = 2 }

    public Dificultad dificultad = Dificultad.Facil;

    public void cambiar_dificultad(int i){
       
        this.dificultad = (Dificultad)i;
        }

    private string sPlayLine = null;
    private float Gest_Time =0f;
    private bool _Comparar = false;

    public bool Comparar
    {
        get{ return this._Comparar; }
        set { this._Comparar = value; }
    }

    private int _Fase_rango;
    public int Fase_rango
    {
        get { return _Fase_rango;  }
        set { _Fase_rango = value; }
    }
    private float _Error_Margin;
    public float Error_Margin
    {
        get{ return this._Error_Margin; }
        set { this._Error_Margin = value; }
    }

    public void cambiar_calibrar_texto(string texto)
    {
        this.texto_calibrar.GetComponent<TextMeshProUGUI>().text = texto;
    }

    /// <summary>
    /// Gets the singleton KinectRecorderPlayer instance.
    /// </summary>
    /// <value>The KinectRecorderPlayer instance.</value>
    public static Gesture_Action Instance
    {
        get
        {
            return instance;
        }
    }
    // Use this for initialization
    public ArrayList Getmovimientos()
    {
        return this.movimientos;
    }

    public float Gesture_Action_Time
    {
        get
        {
            return this.Gest_Time;
        }
    }

    public void LeerMovimiento (string file)
    {


        //// stop playing if there is no file name specified
        //if (file.Length == 0 || !File.Exists(file))
        //{

        //    Debug.LogError("No file to play.");

        //   /* if (infoText != null)
        //    {
        //        infoText.text = "No file to play.";
        //    }
        //    */
        //}
        //else
            this.movimientos.Clear();
            ReadFile(file);

    }

    // reads a line from the file
    private bool ReadFile(string file)
    {

        if (file == null)
            return false;

        // stop playing if there is no file name specified
        //if (file.Length == 0 || !File.Exists(file))
        //{

        //    Debug.LogError("No file to play.");

        //   /* if (infoText != null)
        //    {
        //        infoText.text = "No file to play.";
        //    }
        //    */
        //}



        // open the file and read a line
#if !UNITY_WSA


        TextAsset asset = Resources.Load("movimientos/" + file) as TextAsset;
        // convert string to stream
        byte[] byteArray = Encoding.UTF8.GetBytes(asset.text);
        //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader fileReader = new StreamReader(stream);
#endif

        sPlayLine = fileReader.ReadLine();
       
        while (sPlayLine != null)
        {



            // extract the unity time and the body frame
            char[] delimiters = { '|' };
            string[] sLineParts = sPlayLine.Split(delimiters);

            float.TryParse(sLineParts[0], out this.Gest_Time);

            if (sLineParts.Length >= 2)
            {
                // float.TryParse(sLineParts[0], out fPlayTime);
                sPlayLine = sLineParts[1];
                // fCurrentFrame++;


            }

            //para cada joint 
            char[] delimiter = { ',' };
            string[] alCsvParts = sPlayLine.Split(delimiter);

            // check the id, body count & joint count
            int jointCount = 0;
            int.TryParse(alCsvParts[3], out jointCount);

            Vector3[] movimiento = new Vector3[jointCount];

            int iIndex = 4;
            while (alCsvParts[iIndex] == "0")
            {
                iIndex++;
            }
            iIndex = iIndex + 3;
            // update joints' data
            for (int j = 0; j < jointCount; j++)
            {
              

                if (alCsvParts.Length >= (iIndex + 1))
                {
                   

                    

                 
                        float x = 0f, y = 0f, z = 0f;

                        float.TryParse(alCsvParts[iIndex], out x);
                        float.TryParse(alCsvParts[iIndex + 1], out y);
                        float.TryParse(alCsvParts[iIndex + 2], out z);
                        iIndex += 4;

                    //por el movimiento de espejo
                    int joint = (int)KinectInterop.GetMirrorJoint((KinectInterop.JointType)j);
                    Vector3 current_joint = new Vector3(x, y, z);
                    current_joint.x = -current_joint.x;
                    current_joint.z = -current_joint.z;

                    KinectManager manager = KinectManager.Instance;
                    current_joint = manager.GetKinectToWorldMatrix().MultiplyPoint3x4(current_joint);
                    movimiento[joint] = current_joint;
                    
                    if (j > 0)
                        movimiento[joint] = movimiento[joint] - movimiento[0];
                        //if (joint == 4)
                          //       Debug.Log("Tranformado : "+ movimiento[joint]);
                }
                }



            
            movimientos.Add(movimiento);
            // read a line
            sPlayLine = fileReader.ReadLine();
        }

        // close the file and disable the play mode
        fileReader.Dispose();
        fileReader = null;
    

        return true;
    }

    private void actualizar_Dificultad()
    {
        switch (this.dificultad)
        {
            case Dificultad.Facil:
                {
                    this._Fase_rango = 12;
                    this._Error_Margin = 0.30f;
                }
            break;
            case Dificultad.Media:
                {
                    this._Fase_rango = 8;
                    this._Error_Margin = 0.25f;
                }
            break;
            case Dificultad.Dificil:
                {
                    this.Fase_rango = 4;
                    this.Error_Margin = 0.20f;
                }
            break;
        }
            

    }
    void Awake()
    {
        instance = this;
    }
    void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
        actualizar_Dificultad();

    }

}
//IEnumerator waitFor(float n)
//{
//    yield return new WaitForSeconds(n);
//    Debug.Log("Ya han oasado x segundos");
//}

//StartCoroutine(this.waitFor(2));
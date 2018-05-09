using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;

public class KinectRecorderPlayer : MonoBehaviour 
{
	[Tooltip("Path to the file used to save or play the recorded data.")]
	public string filePath = "Assets/Resources/Movimientos/BodyRecording.txt";
    public string SetFilePath {
        set { filePath =value; }
    }

	[Tooltip("GUI-Text to display information messages.")]
	public GUIText infoText;

	[Tooltip("Whether to start playing the recorded data, right after the scene start.")]
	public bool playAtStart = false;


	// singleton instance of the class
	private static KinectRecorderPlayer instance = null;

    public KinectGestures KG;

    //Para resetear el tiempo
    private bool inicializado  =true;

    private bool _move_finished = false;

    public bool move_finished
    {
        set { _move_finished = value; }
        }

    public GameObject Canvas_iteracion;
    private bool _click_buttón = false;

    public bool click_buttón
    {
        set { _click_buttón = value; }
    }

    private bool _registro_comparacion = false;

    public bool registro_comparacion
    {
        set { _registro_comparacion = value; }
    }
    private bool _Movimiento_registrado_play = false;
    public bool Movimiento_registrado_play
    {
        set { _Movimiento_registrado_play = value; }
    }


    private bool _calibracion = true;
    public bool calibracion
    {
        get { return _calibracion; }
        set { _calibracion = value; }
    }


    //para la posicion de la animacion de analisis
    private bool _posicion_analisis = false;
    public bool posicion_analisis 
    {
        get { return _posicion_analisis; }
        set { _posicion_analisis = value; }
    }

    private Registrar_Movimientos Movimiento_Usuario;

    // whether it is recording or playing saved data at the moment
    private bool isRecording = false;
	private bool isPlaying = false;
    private bool _inpose = false;
    public bool inpose
    {
        get { return _inpose; }
        set { _inpose = value; }
    }

    private bool _vista_previa = true;
    public bool vista_previa 
    {
        get { return _vista_previa; }
        set { _vista_previa = value; }
    }


    //Para manejar los botones de reproducion cuando termina el movimiento
    public UnityEngine.UI.Button play;
    public UnityEngine.UI.Button cambiar_movimiento;


    // reference to the KM
    private KinectManager manager = null;

	// time variables used for recording and playing
	private long liRelTime = 0;
	private float fStartTime = 0f;
	private float fCurrentTime = 0f;
	private int fCurrentFrame = 0;

	// player variables
	private StreamReader fileReader = null;
	private float fPlayTime = 0f;
	private string sPlayLine = string.Empty;


	/// <summary>
	/// Gets the singleton KinectRecorderPlayer instance.
	/// </summary>
	/// <value>The KinectRecorderPlayer instance.</value>
	public static KinectRecorderPlayer Instance
	{
		get
		{
			return instance;
		}
	}

	

	// starts recording
	public bool StartRecording()
	{
		if(isRecording)
			return false;

		isRecording = true;

		// avoid recording an playing at the same time
		if(isPlaying && isRecording)
		{
			CloseFile();
			isPlaying = false;
			
			Debug.Log("Playing stopped.");
		}
		
		// stop recording if there is no file name specified
		if(filePath.Length == 0)
		{
			isRecording = false;

			Debug.LogError("No file to save.");
			if(infoText != null)
			{
				infoText.text = "No file to save.";
			}
		}
		
		if(isRecording)
		{
			Debug.Log("Recording started.");
			if(infoText != null)
			{
				infoText.text = "Recording... Say 'Stop' to stop the recorder.";
			}
			
			// delete the old csv file
			if(filePath.Length > 0 && File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			
			// initialize times
			fStartTime = fCurrentTime = Time.time;
			fCurrentFrame = 0;
		}

		return isRecording;
	}


	// starts playing
	public bool StartPlaying()
	{
		if(isPlaying)
			return false;

		isPlaying = true;
        inicializado = true;
        //para terminar de leer la misma frase
        move_finished = false;

        if (!_vista_previa && !_Movimiento_registrado_play)
        {
            // Movimientos
            Gesture_Action movimiento = Gesture_Action.Instance;
            movimiento.LeerMovimiento(filePath);


            movimiento.Comparar = true;
        }
        


        // avoid recording an playing at the same time
        if (isRecording && isPlaying)
		{
			isRecording = false;
			Debug.Log("Recording stopped.");
		}
		
		// stop playing if there is no file name specified
		//if(filePath.Length == 0 || !File.Exists(filePath))
		//{
		//	isPlaying = false;
		//	Debug.LogError("No file to play.");

		//	if(infoText != null)
		//	{
		//		infoText.text = "No file to play.";
		//	}
		//}
		
		if(isPlaying)
		{
			Debug.Log("Playing started.");
			if(infoText != null)
			{
				infoText.text = "Playing... Say 'Stop' to stop the player.";
			}

			// initialize times
			fStartTime = fCurrentTime = Time.time;
			fCurrentFrame = -1;

            // open the file and read a line
#if !UNITY_WSA
            TextAsset asset = Resources.Load("movimientos/"+filePath) as TextAsset;
            // convert string to stream
            byte[] byteArray = Encoding.UTF8.GetBytes(asset.text);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            MemoryStream stream = new MemoryStream(byteArray);

            // convert stream to string
          
            fileReader = new StreamReader(stream);
#endif
			ReadLineFromFile();
			
			// enable the play mode
			if(manager)
			{
                manager.maxTrackedUsers = 2;

                if (_registro_comparacion)
                {
                    //para reproducir el movimiento con el usuario a la vez para ver errores
                    manager.EnablePlayMode(true);
                    _registro_comparacion=false;
                }
                else
                    // para reproducir la grabacion y usuario captado con la kinect
                    manager.EnableCompareMode(true);
                
            }
		}

		return isPlaying;
	}


	// stops recording or playing
	public void StopRecordingOrPlaying()
	{
		if(isRecording)
		{
			isRecording = false;

			Debug.Log("Recording stopped.");
			if(infoText != null)
			{
				infoText.text = "Recording stopped.";
			}
		}

		if(isPlaying)
		{
			// close the file, if it is playing
			CloseFile();
			isPlaying = false;

            manager.maxTrackedUsers = 1;
            Debug.Log("Playing stopped.");
            Gesture_Action movimiento = Gesture_Action.Instance;
            //se resetea el gesto
            if (_click_buttón)
            {
                Int64 userID = manager ? manager.GetUserIdByIndex(0) : 0;
                manager.ResetGesture(userID, KinectGestures.Gestures.Move);
                inpose = false;

               

                movimiento.Comparar = false;
                movimiento.infoText_Movimiento.text = "";
                movimiento.cambiar_calibrar_texto("");

                _click_buttón = false;

            }

             play.interactable = true;
             cambiar_movimiento.interactable = true;

            //ref_hijos canvas_iteracion = GameObject.Find("Canvas iteración").GetComponent<ref_hijos>();
            
            //GameObject.Find("KinectController").GetComponent<KinectGestures>().SetMostrar_Mensaje_Calibrar(true);
            _calibracion = true;

            Canvas_iteracion.GetComponent<ref_hijos>().button_Comparar.SetActive(true);
            Canvas_iteracion.GetComponent<ref_hijos>().button_Cancelar_Comparar.SetActive(false);


            if (infoText != null)
			{
				infoText.text = "Playing stopped.";
			}

            
            _Movimiento_registrado_play = false;

        }

		if(infoText != null)
		{
			infoText.text = "Say: 'Record' to start the recorder, or 'Play' to start the player.";
		}
	}

	// returns if file recording is in progress at the moment
	public bool IsRecording()
	{
		return isRecording;
	}

	// returns if file-play is in progress at the moment
	public bool IsPlaying()
	{
		return isPlaying;
	}
	

	// ----- end of public functions -----
	
	void Awake()
	{
		instance = this;
	}

	void Start()
	{
        //Time.captureFramerate = 4;



        //lectura del  y relleno de vector movimiento

        Movimiento_Usuario = GameObject.Find("Registro Movimiento Usuario").GetComponent<Registrar_Movimientos>();



        // 





        if (infoText != null)
		{
			infoText.text = "Say: 'Record' to start the recorder, or 'Play' to start the player.";
		}

		if(!manager)
		{
			manager = KinectManager.Instance;
		}
		else
		{
			Debug.Log("KinectManager not found, probably not initialized.");

			if(infoText != null)
			{
				infoText.text = "KinectManager not found, probably not initialized.";
			}
		}
        
		if(playAtStart)
		{
			StartPlaying();
		}
	}

	void Update () 
	{
		if(isRecording)
		{
			// save the body frame, if any
			if(manager && manager.IsInitialized())
			{
				const char delimiter = ',';
				string sBodyFrame = manager.GetBodyFrameData(ref liRelTime, ref fCurrentTime, delimiter);


                if (sBodyFrame.Length > 0)
                {
#if !UNITY_WSA
                    using (StreamWriter writer = File.AppendText(filePath))
                    {
                        string sRelTime = string.Format("{0:F3}", (fCurrentTime - fStartTime));
                        //if (fCurrentFrame % 30 == 0)
                        //{
                        writer.WriteLine(sRelTime + "|" + sBodyFrame);

                        if (infoText != null)
                        {
                            infoText.text = string.Format("Recording @ {0}s., frame {1}. Say 'Stop' to stop the player.", sRelTime, fCurrentFrame);

                        }
                        //}
                        fCurrentFrame++;
                    }
#else
					string sRelTime = string.Format("{0:F3}", (fCurrentTime - fStartTime));
					Debug.Log(sRelTime + "|" + sBodyFrame);
#endif
                }
                else

                {// infoText.text = "No user kinect detected to record"; }}
                }
			}
		}

		if(isPlaying)
		{
			// wait for the right time
			fCurrentTime = Time.time;
			float fRelTime = fCurrentTime - fStartTime;


            //vista previa
            if (_vista_previa)
            {
                if (sPlayLine != null && fRelTime >= fPlayTime)
                {

                    // then play the line
                    if (manager && sPlayLine.Length > 0)
                    {
                        //Para Reproducir una grabacion y el usuario actual al mismo tiempo
                        manager.SetBodyFrameDataCompare(sPlayLine);

                    }
                    this.ReadLineFromFile();

                }
            }

            // Reproducir Movimiento registrados de usuario

            else if (_Movimiento_registrado_play)
            {
                
                if (sPlayLine != null && fRelTime >= fPlayTime)
                {
                    // then play the line
                    if (manager && sPlayLine.Length > 0)
                    {
                 
                        manager.SetBodyFrameData(sPlayLine);
                    }

                    this.ReadLineFromRegistro();
                }

            }

            //comparando moviento
            else {
                if (this._inpose)
            {
                if (inicializado)
                {
                    fStartTime = fCurrentTime = Time.time;
                    inicializado = false;
                }
                if (sPlayLine != null && fRelTime >= fPlayTime)
                {

                    // then play the line
                    if (manager && sPlayLine.Length > 0)
                    {
                        //Para Reproducir una grabacion y el usuario actual al mismo tiempo
                        manager.SetBodyFrameDataCompare(sPlayLine);

                         //se registra el movimiento para hacer despues la comparación
                         //se registrar el player y la del record
                         
                         if(!_calibracion)
                         Movimiento_Usuario.registrarMovimiento();

                        }
                    this.ReadLineFromFile();

                }
            }
            else
            {
                // then play the line
                if (manager && sPlayLine.Length > 0)
                {
                    if (_move_finished)
                        sPlayLine = null;
                    //manager.SetBodyFrameData(sPlayLine);
                    manager.SetBodyFrameDataCompare(sPlayLine);
                }

            }
                // and read the next line


           }

            if (sPlayLine == null)
			    {
				// finish playing, if we reached the EOF
				StopRecordingOrPlaying();
			    }
		}

      
    }

	void OnDestroy()
	{
		// don't forget to release the resources
		CloseFile();
		isRecording = isPlaying = false;
	}

	// reads a line from the file
	private bool ReadLineFromFile()
	{
		if(fileReader == null)
			return false;

		// read a line
		sPlayLine = fileReader.ReadLine();
		if(sPlayLine == null)
			return false;

		// extract the unity time and the body frame
		char[] delimiters = { '|' };
		string[] sLineParts = sPlayLine.Split(delimiters);

		if(sLineParts.Length >= 2)
		{
			float.TryParse(sLineParts[0], out fPlayTime);
			sPlayLine = sLineParts[1];
			fCurrentFrame++;

			if(infoText != null)
			{
				infoText.text = string.Format("Playing @ {0:F3}s., frame {1}. Say 'Stop' to stop the player.", fPlayTime, fCurrentFrame);
			}

			return true;
		}

		return false;
	}


    // reads a line from the file
    private bool ReadLineFromRegistro()
    {
        if (fileReader == null)
            return false;

        // read a line
        sPlayLine =  Movimiento_Usuario.Obtener_frame_body();
        if (sPlayLine == null)
            return false;

        // extract the unity time and the body frame
        char[] delimiters = { '|' };
        string[] sLineParts = sPlayLine.Split(delimiters);

        if (sLineParts.Length >= 2)
        {
            float.TryParse(sLineParts[0], out fPlayTime);
            sPlayLine = sLineParts[1];
            fCurrentFrame++;

            if (infoText != null)
            {
                infoText.text = string.Format("Playing @ {0:F3}s., frame {1}. Say 'Stop' to stop the player.", fPlayTime, fCurrentFrame);
            }

            return true;
        }

        return false;
    }
    // close the file and disable the play mode
    private void CloseFile()
	{
		// close the file
		if(fileReader != null)
		{
			fileReader.Dispose();
			fileReader = null;
		}

		// disable the play mode
		if(manager)
		{
			manager.EnableCompareMode(false);
            manager.EnablePlayMode(false);
        }
    }

}

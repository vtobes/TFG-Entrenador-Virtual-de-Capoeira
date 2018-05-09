using UnityEngine;
using System.Collections;

public class KinectPlayerController : MonoBehaviour 
{
	private SpeechManager speechManager;
	private KinectRecorderPlayer saverPlayer;
    private  bool _Play = false;

    public bool Play
    {
        get { return _Play; }
        set { _Play = value; }
    }
    public bool _Record = false;


    public bool Record
    {
        get { return _Record; }
        set { _Record = value; }
    }

    void Start()
	{
		saverPlayer = KinectRecorderPlayer.Instance;
	}

	void Update () 
	{

		// alternatively, use the keyboard
		//if(Input.GetButtonDown("Jump"))  // start or stop recording
         if (_Record)  // start or stop recording
            {
            _Record = false;

            if (saverPlayer)
			{
				if(!saverPlayer.IsRecording())
				{
					saverPlayer.StartRecording();
				}
				else
				{
					saverPlayer.StopRecordingOrPlaying();
				}
			}
		}

        //if (Input.GetButtonDown("Fire1"))  // start or stop playing
         if (_Play)  // start or stop playing
            {
            _Play = false;
            if (saverPlayer)
            {
                if (!saverPlayer.IsPlaying())
                {
                    saverPlayer.StartPlaying();
                }
                else
                {
                    saverPlayer.StopRecordingOrPlaying();
                }
            }
        }

    }

}

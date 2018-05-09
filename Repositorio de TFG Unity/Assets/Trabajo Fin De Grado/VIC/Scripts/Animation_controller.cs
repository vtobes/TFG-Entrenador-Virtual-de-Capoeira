using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animation_controller : MonoBehaviour {

    private Animator animator;
    public int playerIndex;
    // Use this for initialization
    public bool AvatarRecorded;
    private bool restart_animation = true;

    public void Stop_animator(){
        animator.enabled = false;
        restart_animation = true;
        }

    public void Play_animator()
    {
        animator.enabled = true;
        if (restart_animation)
        {
            animator.Rebind();
            restart_animation = false;
        }


       // animator.Play("Jump");

    }

	void Start () {
        animator = gameObject.GetComponent < Animator>();
	}



    // Update is called once per frame
    void Update()
    {
        KinectManager manager = KinectManager.Instance;
        Int64 userID = 0;
        if (AvatarRecorded)
        {
            userID = manager.GetUserIdRecorded();
        }
        else
        {
            // get 1st player
            userID = manager ? manager.GetUserNormalId() : 0;
            //if (manager.IsRecordedUser(userID))
            //  userID = 0;
        }


        if (userID <= 0)
        {
            Play_animator();
            
        }
        else
        {
            Stop_animator();
        }
    }
 
}

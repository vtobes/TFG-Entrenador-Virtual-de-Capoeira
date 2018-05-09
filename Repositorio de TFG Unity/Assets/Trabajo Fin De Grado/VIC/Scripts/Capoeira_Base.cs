using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capoeira_Base : StateMachineBehaviour {
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!KinectRecorderPlayer.Instance.posicion_analisis)
        { 
        animator.transform.position = GameObject.Find("Avatar player").GetComponent<Posicion_inicial>().posicion_incial;
        
        }
        else
        {
            animator.transform.position =  new Vector3(-0.781f, 0.366f, 2.61340f) ;
            
        }


        animator.transform.rotation = GameObject.Find("Avatar player").GetComponent<Posicion_inicial>().rotación_incial;


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.transform.position = GameObject.Find("Avatar player").GetComponent<Posicion_inicial>().posicion_incial;
        animator.transform.rotation = GameObject.Find("Avatar player").GetComponent<Posicion_inicial>().rotación_incial;
       
        //animator.transform.parent.position = animator.transform.position;
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    //}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}

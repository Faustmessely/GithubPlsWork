using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationActivity : StateMachineBehaviour {

    [SerializeField]
    string _triggerActivated;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetBool(_triggerActivated, true);
    }
    
}

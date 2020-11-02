using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnExit : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.transform.parent)
        {
            if (animator.transform.parent.name != "GeneratedLevel")
            {
                Destroy(animator.transform.parent.gameObject, stateInfo.length);
            }
            else
            {
                Destroy(animator.gameObject, stateInfo.length);
            }
        }
        else
        {
            Destroy(animator.gameObject, stateInfo.length);
        }
        
    }
}

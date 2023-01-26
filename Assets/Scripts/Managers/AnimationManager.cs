using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    #region Variables
 
    #endregion

    #region Referencias
    public Animator animControll;
    #endregion



    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        animControll.applyRootMotion = isInteracting;
        animControll.SetBool("isInteracting", isInteracting);
        animControll.CrossFade(targetAnim, 0.2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

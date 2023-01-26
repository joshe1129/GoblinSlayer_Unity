using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : AnimationManager
{
    #region Variables



    #endregion

    #region Referencias

    EnemyManager enemyManager;

    #endregion



    private void Awake()
    {
        animControll = GetComponent<Animator>();
        enemyManager = GetComponentInParent<EnemyManager>();
    }

    private void OnAnimatorMove()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        float delta = Time.deltaTime;
        enemyManager.enemyRigidbody.drag = 0;
        Vector3 deltaPosition = animControll.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyManager.enemyRigidbody.velocity = velocity;
      
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

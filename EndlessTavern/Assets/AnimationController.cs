﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    int animationDelay = 0;
    
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        StartCoroutine(playAnimation());
    }

   
    public IEnumerator playAnimation()
    {
        yield return new WaitForSeconds(animationDelay);
        animator.speed = 1;
        animator.enabled = true;

    }
    
}

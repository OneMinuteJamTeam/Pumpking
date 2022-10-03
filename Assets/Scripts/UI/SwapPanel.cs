using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SwapPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Image backgroundImg;

    private Animator _animator;

    private bool _animationPlayed = false;
    
    public void Play()
    {
        if (_animationPlayed)
            return;
        
        _animationPlayed = true;
        _animator.SetTrigger("Enter");
    }

    private void Awake()
    {
        _animationPlayed = false;
        _animator = GetComponent<Animator>();
        backgroundImg.color = new Color(backgroundImg.color.r, backgroundImg.color.g, backgroundImg.color.b, 0);
    }

    public void OnSwapAnimationComplete() {
        GameController.Instance.EnablePlayersMovement();
        GameController.Instance.ResumeTimer();
        gameObject.SetActive(false);
    }
}

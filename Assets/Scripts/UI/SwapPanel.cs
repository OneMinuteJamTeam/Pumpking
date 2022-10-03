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
    
    public void Play(Action OnAnimationFinished)
    {
        if (_animationPlayed)
            return;
        
        _animationPlayed = true;
        _animator.SetTrigger("Enter");
        StartCoroutine(COWaitForAnim(4.5f,OnAnimationFinished));
    }

    private void Awake()
    {
        _animationPlayed = false;
        _animator = GetComponent<Animator>();
        backgroundImg.color = new Color(backgroundImg.color.r, backgroundImg.color.g, backgroundImg.color.b, 0);
    }

    private IEnumerator COWaitForAnim(float seconds, Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }
    public void EnablePlayersMovement() {
        GameController.Instance.EnablePlayersMovement();
    }
}

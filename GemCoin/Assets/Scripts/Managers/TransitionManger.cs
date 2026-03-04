using UnityEngine;
using System;
using System.Collections;

public class TransitionManager : SingletonMono<TransitionManager>
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _transitionDelay = 0.2f;
    

    public void PlayTransition(Action onMidPoint)
    {
        StartCoroutine(TransitionRoutine(onMidPoint));
    }

    private IEnumerator TransitionRoutine(Action onMidPoint)
    {
        _animator.SetTrigger("In");

        yield return new WaitForSeconds(_transitionDelay);

        // 암전 시점에 레이어 교체/씬 로드 로직 실행
        onMidPoint?.Invoke();

        _animator.SetTrigger("Out");
    }

    public void PlayTransition(string inout)
    {
        _animator.SetTrigger(inout);
    }
}

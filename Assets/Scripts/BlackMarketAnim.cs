using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMarketAnim : MonoBehaviour
{
    [SerializeField] float timeTillDisable;

    const string BlackMarketAnimation = "BlackMarketAnimation";
    const string NoAnimation = "NoAnimation";
    string currentState;

    Animator anim;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(OnEnableCoroutine());
    }

    IEnumerator OnEnableCoroutine()
    {
        PlayAnimation(BlackMarketAnimation);
        yield return new WaitForSeconds(timeTillDisable);
        PlayAnimation(NoAnimation);
        gameObject.SetActive(false);
    }

    void PlayAnimation(string newState)
    {
        if (newState != currentState)
        {
            currentState = newState;
            anim.Play(currentState);
        }
    }
}

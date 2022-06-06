using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerinaAnimations : MonoBehaviour
{
    public Animator FroggerinaAnimator;

    public enum Emotions
    { 
        None,
        Crying,
        Joy,
        Panicked
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            TalkCrying();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TalkJoy();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TalkPanicked();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Talk();
        }
    }

    public void Death()
    {
        FroggerinaAnimator.SetBool("Dying", true);
        FroggerinaAnimator.SetBool("Despawning", false);
    }
    public void Despawn()
    {
        FroggerinaAnimator.SetBool("Despawning", true);
    }

    public void Idle()
    {
        FroggerinaAnimator.SetBool("Spawning", false);
        FroggerinaAnimator.SetBool("Talking", false);
        FroggerinaAnimator.SetFloat("Blend", 0);
    }

    public void IdleCrying()
    {
        Idle();
        FroggerinaAnimator.SetFloat("Blend", 0.333333f);
    }

    public void IdleJoy()
    {
        Idle();
        FroggerinaAnimator.SetFloat("Blend", 0.666666666f);
    }

    public void IdlePanicked()
    {
        Idle();
        FroggerinaAnimator.SetFloat("Blend", 1);
    }

    public void Spawn()
    {
        FroggerinaAnimator.SetBool("Spawning", true);
    }

    public void Talk()
    {
        FroggerinaAnimator.SetBool("Talking", true);
        FroggerinaAnimator.SetFloat("Blend", 0);
    }

    public void TalkCrying()
    {
        FroggerinaAnimator.SetBool("Talking", true);
        FroggerinaAnimator.SetFloat("Blend", 0.333333f);
    }

    public void TalkJoy()
    {
        FroggerinaAnimator.SetBool("Talking", true);
        FroggerinaAnimator.SetFloat("Blend", 0.666666666f);
    }

    public void TalkPanicked()
    {
        FroggerinaAnimator.SetBool("Talking", true);
        FroggerinaAnimator.SetFloat("Blend", 1);
    }
}

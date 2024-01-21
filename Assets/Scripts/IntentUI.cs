using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IntentUI : MonoBehaviour
{
    public Image intentImage;
    public Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void DisplayIntent( Sprite sprite)
    {
        intentImage.sprite = sprite;
        animator.SetTrigger("Spawn");
    }
    public void IntentFade()
    {
        animator.SetTrigger("Fade");
    }
}

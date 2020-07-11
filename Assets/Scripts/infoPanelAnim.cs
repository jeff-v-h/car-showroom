using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoPanelAnim : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlaySlide()
    {
        anim.Play("infoAnim");
    }

    public void CloseSlide()
    {
        anim.Play("infoAnimReverse");
    }
}

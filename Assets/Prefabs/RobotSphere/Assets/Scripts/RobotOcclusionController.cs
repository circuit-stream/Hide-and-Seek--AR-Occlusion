using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotOcclusionController : MonoBehaviour
{
    private PlacedObject placedObject;
    private Animator animator;
    
    void Awake()
    {
        placedObject = GetComponent<PlacedObject>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (!animator.GetBool("Open_Anim"))
        {
            animator.SetBool("Open_Anim", true);
        }        
    }

    void Update()
    {
        if (placedObject.Selected)
        {
            animator.SetBool("Open_Anim", false);           
        }
        else
        {
            animator.SetBool("Open_Anim", true);
        }
    }
}

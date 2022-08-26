using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankPalette : MonoBehaviour
{
    public Animator[] paletteAnimator;
    Vector3 lastPosition;


    private void Update()
    {
        if(this.transform.position != lastPosition)
        {
            foreach(Animator anim in paletteAnimator)
            {
                anim.SetBool("isMoving", true);
            }
        }
        else
        {
            foreach(Animator anim in paletteAnimator)
            {
                anim.SetBool("isMoving", false);
            }
        }

        lastPosition = this.transform.position;
    }
}

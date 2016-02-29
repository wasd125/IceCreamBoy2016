using UnityEngine;
using System.Collections;

public class IndicatorHolder : MonoBehaviour {

    public Animator childAnim;
    public SpriteRenderer childRender;

    public void ShowIndicator()
    {
        childAnim.SetTrigger("ShowIndicator");
    }
}

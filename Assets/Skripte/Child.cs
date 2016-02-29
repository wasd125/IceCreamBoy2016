using UnityEngine;
using System.Collections;

public class Child : MonoBehaviour {



    private Animator anim;
    private Animator smallAnim;

    public void MoveIn()
    {
        anim.SetTrigger("MoveIn");
        smallAnim.SetTrigger("MoveIn");
    }


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    public void InitChild(Animator anim)
    {
        smallAnim = anim;
    }

	// Update is called once per frame
	void Update () {
	
	}
}

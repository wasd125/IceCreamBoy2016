using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {

    public void DestroyMe()
    {
        Destroy(gameObject.transform.parent.gameObject);
        Destroy(gameObject);      
    }
}

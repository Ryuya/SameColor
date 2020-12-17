using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEffect : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void Remove()
    {
        Destroy(this.gameObject);
    }
}

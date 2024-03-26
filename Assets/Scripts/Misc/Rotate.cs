using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField] float rotateTime;
    [Header("(Optional)")]
    [SerializeField] Transform rotateTarget;

    // Start is called before the first frame update
    void Start()
    {
        if(rotateTarget == null)
            rotateTarget = this.transform;
        
        LeanTween.rotateAroundLocal(rotateTarget.gameObject, Vector3.up, 360, rotateTime).setRepeat(-1);
    }

}

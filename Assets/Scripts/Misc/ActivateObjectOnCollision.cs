using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectOnCollision : MonoBehaviour
{

    [Header("Target Object must exist in scene")]
    [SerializeField] GameObject targetObject;

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Player")
        {
            targetObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}

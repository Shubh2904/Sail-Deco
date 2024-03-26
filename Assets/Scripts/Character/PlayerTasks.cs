using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTasks : MonoBehaviour
{
    [SerializeField] InteractRange interactRangeObj;
    
    
    [HideInInspector] public InteractRange interactRange;

    // Start is called before the first frame update
    void Start()
    {
        interactRange = Instantiate(interactRangeObj, transform.position, Quaternion.identity);
        
        interactRange.setTarget(this.transform);

        GM.interactRange = interactRange;
    }

}

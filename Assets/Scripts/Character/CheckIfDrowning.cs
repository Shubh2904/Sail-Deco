using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfDrowning : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator checkIfDrowning()
    {
        for(;;)
        {
            if(transform.position.y < -5)
                GM.singleton.resetGame();

            yield return new WaitForSeconds(2f);
        }
    }

    void OnEnable()
    {
        StartCoroutine(checkIfDrowning());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

}

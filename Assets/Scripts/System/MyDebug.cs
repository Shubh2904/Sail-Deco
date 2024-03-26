using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyDebug : MonoBehaviour
{
    [SerializeField] TMP_Text debugPrompt;
    [SerializeField] GameObject sea;

    string originalText;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        originalText = debugPrompt.text;

        for(;;)
        {
            int fps = (int) (1/Time.deltaTime);

            debugPrompt.text = System.String.Format(originalText, fps);

            yield return new WaitForSeconds(1f);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            if(sea.activeInHierarchy)
                sea.SetActive(false);
            else 
                sea.SetActive(true);

        if(Input.GetKeyDown(KeyCode.Tab))
            GM.singleton.resetGame();
    }
}

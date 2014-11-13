using UnityEngine;
using System.Collections;

public class jsHUDComponent : MonoBehaviour
{

    public jsPlayerCharacter PC;

    void Awake()
    {
        //
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        //
        if (playerObject != null)
            PC = playerObject.GetComponent<jsPlayerCharacter>();
    }
    
    void OnGUI() 
    {
        GUI.Box(new Rect(10, 10, 100, 60), "Debug Menu");
    
        if(GUI.Button(new Rect(20,40,80,20), "Retry")) 
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        GUI.Box(new Rect(10, 80, 100, 40), "Water Amount \n" + PC.waterAmount.ToString());
    }
}

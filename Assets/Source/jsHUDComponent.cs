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
        GUI.Box(new Rect(10, 10, 100, 80), "Debug Menu");
   

        if (GUI.Button(new Rect(20, 40, 80, 20), "Level 1"))
        {
            Application.LoadLevel(0);
        }

        if (GUI.Button(new Rect(20, 60, 80, 20), "Level 2"))
        {
            Application.LoadLevel(1);
        }

        GUI.Box(new Rect(10, 100, 100, 40), "Water Amount \n" + PC.waterAmount.ToString());
    }
}

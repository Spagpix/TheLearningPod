using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public int easyDifficulty = 0;
    public int mediumDifficulty = 1;
    public int hardDifficulty = 2;
    public int currentDifficulty;
   public bool hasMessageSaid;
   
   

   

    public void Update()
    {
       
        {
            if (currentDifficulty == easyDifficulty) //&& hasMessageSaid == false)
            {
                Debug.Log("You selected is easy");
                //hasMessageSaid = true;

            }

            else if (currentDifficulty == mediumDifficulty)// && hasMessageSaid == false)
            {
                Debug.Log("You selected medium");
                //hasMessageSaid = true;
            }
            else if (currentDifficulty == hardDifficulty)// && hasMessageSaid == false)
            {
                Debug.Log("You selected hard");
                //hasMessageSaid = true;
            }
            else 
            {
                Debug.Log("Please select a level before continuing");
               
            }
            //   else if()
        }
    }
}

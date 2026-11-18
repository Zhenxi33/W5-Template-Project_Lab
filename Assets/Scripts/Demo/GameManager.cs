using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public void onButtonClick(GameObject buttonObject)
    {
        Debug.Log(buttonObject.name + " clicked.");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveButton : MonoBehaviour
{
    public GameObject screenToTurnOn;
    // Start is called before the first frame update
    public void TurnOn()
    {
        screenToTurnOn.SetActive(true);
    }

    public void TurnOff()
    {
        screenToTurnOn.SetActive(false);       
    }

}

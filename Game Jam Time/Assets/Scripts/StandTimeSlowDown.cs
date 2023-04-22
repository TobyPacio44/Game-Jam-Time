using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandTimeSlowDown : MonoBehaviour
{
    public Movment mov;
    public bool CanSlowDown;
    private void Update()
    {
        if (CanSlowDown == false)
        {
            Time.timeScale = mov.IsStanding;
        }
    }


}

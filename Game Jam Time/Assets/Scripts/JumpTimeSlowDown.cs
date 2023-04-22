using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTimeSlowDown : MonoBehaviour
{
    public float slowDownFactor = 0.5f;
    public float slowDownDuration = 2f;
    bool isSlowMotionActive = false;
    float originalTimeScale;
    public  StandTimeSlowDown sl;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isSlowMotionActive)
            {
                StartCoroutine(SlowMotionCoroutine());
                if (sl != null)
                {
                    sl.CanSlowDown = true;
                }
            }
        }
    }

    IEnumerator SlowMotionCoroutine()
    {
        originalTimeScale = Time.timeScale;
        Time.timeScale = slowDownFactor;
        isSlowMotionActive = true;
        yield return new WaitForSecondsRealtime(slowDownDuration);
        Time.timeScale = originalTimeScale;
        isSlowMotionActive = false;
        if (sl != null)
        {
            sl.CanSlowDown = false;
        }
    }
}

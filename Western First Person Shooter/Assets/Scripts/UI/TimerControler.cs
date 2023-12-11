using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerControler : MonoBehaviour
{
    [SerializeField]
    int levelTimeLimitInSeconds;
    float currentTimeInLevel = 0;

    Slider timer;

    public delegate void Timer();
    public static event Timer TimerEnded;
    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Slider>();
        timer.maxValue = levelTimeLimitInSeconds;

    }

    // Update is called once per frame
    void Update()
    {
        currentTimeInLevel += Time.deltaTime;
        timer.value = currentTimeInLevel;
        if(currentTimeInLevel >= levelTimeLimitInSeconds)
        {
            TimerEnded?.Invoke();
        }
    }
}

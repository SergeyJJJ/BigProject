using UnityEngine;

public static class TimerController
{
    // Decrement timer by Time.deltaTime value.
    public static void DecrementByDeltaTime(ref float timer)
    {
        timer -= Time.deltaTime;
    }


    // Increment timer by Time.deltaTime value.
    public static void IncrementByTimeDeltaTime(ref float timer)
    {
        timer += Time.deltaTime;
    }


    // Set timer to zero.
    public static void SetToZero(ref float timer)
    {
        timer = 0f;
    }


    // Set timer to user value.
    public static void SetToValue(ref float timer, float valueToSet)
    {
        timer = valueToSet;
    }
}

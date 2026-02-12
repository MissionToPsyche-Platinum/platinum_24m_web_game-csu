using UnityEngine;

public class Drill : MonoBehaviour
{
    //Production: ALL NUMBERS HERE ARE PLACE HOLDERSU NTIL THE GAME IS BALANCED
    public int amountPerTick = 3;
    public float secondsPerTick = 4f;

    //levels
    public int speedLevel = 1;
    public int amountLevel = 1;

    public void UpgradeSpeed()
    {
        speedLevel++;
        secondsPerTick *= 0.85f; // faster each upgrade
        secondsPerTick = Mathf.Max(0.5f, secondsPerTick); // cap
    }

    public void UpgradeAmount()
    {
        amountLevel++;
        amountPerTick += 2;
    }

}


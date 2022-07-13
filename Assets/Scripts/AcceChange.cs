using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceChange : MonoBehaviour
{
    public void BuyHat(int number)
    {
        UserData.ItemHat = number;
        UserData.Save();
    }
    public void BuyGlobe(int number)
    {
        UserData.ItemGlobe = number;
        UserData.Save();
    }
    public void BuyBoots(int number)
    {
        UserData.ItemShose = number;
        UserData.Save();
    }
}

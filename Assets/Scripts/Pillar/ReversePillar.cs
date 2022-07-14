using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePillar : Pillar
{
    private IEnumerator ChangeReverse()
    {
        while (true)
        {
            yield return null;
            if (PlayerController.Instance.currentPillar == this)
            {

            }
        }
    }
}

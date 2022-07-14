using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcceChange : MonoBehaviour
{
    [SerializeField] HatContainer hatcon;
    [SerializeField] GlobeContainer globecon;
    [SerializeField] BootsContainer bootcon;

    [SerializeField] ScrollRect scrollRect;
    [SerializeField] RectTransform[] contents;

    [SerializeField] Image head;
    [SerializeField] Image LA;
    [SerializeField] Image RA;
    [SerializeField] Image LL;
    [SerializeField] Image RL;
    public void BuyHat(int number)
    {
        UserData.ItemHat = number;
        head.sprite = hatcon.Accessories[UserData.ItemHat].Sprite;
        UserData.Save();
    }
    public void BuyGlobe(int number)
    {
        UserData.ItemGlobe = number;
        LA.sprite = globecon.Accessories[UserData.ItemGlobe].Sprite;
        RA.sprite = globecon.Accessories[UserData.ItemGlobe].Sprite;
        UserData.Save();
    }
    public void BuyBoots(int number)
    {
        UserData.ItemShose = number;
        LL.sprite = bootcon.Accessories[UserData.ItemShose].Sprite;
        RL.sprite = bootcon.Accessories[UserData.ItemShose].Sprite;
        UserData.Save();
    }

    public void ActivePanel(string name)
    {
        foreach(RectTransform a in contents)
        {
            if(a.name == name)
            {
                a.gameObject.SetActive(true);
                scrollRect.content = a;
            }
            else
                a.gameObject.SetActive(false);
        }
        
    }
}

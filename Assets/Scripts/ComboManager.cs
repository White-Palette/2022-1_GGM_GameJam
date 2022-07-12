using UnityEngine;

public class ComboManager : MonoSingleton<ComboManager>
{
    public int Combo { get; private set; }

    public void AddCombo()
    {
        Combo++;
        StartCoroutine(UIManager.Instance.ComboEffect());
    }

    public void ResetCombo()
    {
        Combo = 0;
    }

    public void AddCombo(int combo)
    {
        Combo += combo;
    }
}
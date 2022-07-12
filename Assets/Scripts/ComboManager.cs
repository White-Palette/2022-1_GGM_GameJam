using UnityEngine;

public class ComboManager : MonoSingleton<ComboManager>
{
    public int Combo { get; private set; }
    public int MaxCombo { get; private set; }

    public void AddCombo()
    {
        Combo++;
        StartCoroutine(UIManager.Instance.ComboEffect());
    }

    public void ResetCombo()
    {
        if (Combo > MaxCombo)
        {
            MaxCombo = Combo;
        }
        Combo = 0;
    }

    public void AddCombo(int combo)
    {
        Combo += combo;
    }
}
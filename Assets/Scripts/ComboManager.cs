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
        UpdateMaxCombo();
        Combo = 0;
    }

    public void UpdateMaxCombo()
    {
        if (Combo > MaxCombo)
        {
            MaxCombo = Combo;
        }
    }

    public void AddCombo(int combo)
    {
        Combo += combo;
    }
}
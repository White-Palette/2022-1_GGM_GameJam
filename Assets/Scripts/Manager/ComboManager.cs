using UnityEngine;

public class ComboManager : MonoSingleton<ComboManager>
{
    public int Combo { get; private set; }
    public int MaxCombo { get; private set; }

    private bool _isChaser = false;

    public void AddCombo()
    {
        Combo++;
        StartCoroutine(UIManager.Instance.ComboEffect());
    }

    public void ResetCombo()
    {
        UpdateMaxCombo();

        if (!_isChaser)
        {
            _isChaser = ChaserGenerator.Instance.GenerateChaser();
        }
        else
        {
            ChaserGenerator.Instance.Chaser.AddSpeed();
        }

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
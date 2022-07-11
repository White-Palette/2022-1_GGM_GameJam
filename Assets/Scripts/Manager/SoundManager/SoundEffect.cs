public class SoundEffect
{
    public static void Play(Sound sound)
    {
        if (sound == Sound.None)
            return;
        SoundManager.Instance.PlaySound(sound);
    }
}
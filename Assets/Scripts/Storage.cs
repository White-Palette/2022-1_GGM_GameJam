using UnityEngine;

public static class UserData
{
    public static class Record
    {
        public static float Height { get; set; }
        public static int MaxCombo { get; set; }
    }
    
    public static class Cache
    {
        public static float Height { get; set; }
        public static int MaxCombo { get; set; }
    }

    public static int UserId { get; set; } = -1;
    public static string UserName { get; set; } = "";
    public static string UserToken { get; set; } = "";
    public static float Brightness { get; set; } = 1;

    public static void Save()
    {
        PlayerPrefs.SetFloat("Brightness", Brightness);
        PlayerPrefs.SetInt("UserId", UserId);
        PlayerPrefs.SetString("UserName", UserName);
        PlayerPrefs.SetString("UserToken", UserToken);
        PlayerPrefs.SetFloat("Height", Record.Height);
        PlayerPrefs.SetInt("MaxCombo", Record.MaxCombo);
    }

    public static void Load()
    {
        Brightness = PlayerPrefs.GetFloat("Brightness", 1);
        UserId = PlayerPrefs.GetInt("UserId", -1);
        UserName = PlayerPrefs.GetString("UserName", "");
        UserToken = PlayerPrefs.GetString("UserToken", "");
        Record.Height = PlayerPrefs.GetFloat("Height", 0);
        Record.MaxCombo = PlayerPrefs.GetInt("MaxCombo", 0);
    }

    public static void Clear()
    {
        PlayerPrefs.DeleteKey("Brightness");
        PlayerPrefs.DeleteKey("UserId");
        PlayerPrefs.DeleteKey("UserName");
        PlayerPrefs.DeleteKey("UserToken");
        PlayerPrefs.DeleteKey("Height");
        PlayerPrefs.DeleteKey("MaxCombo");
    }

    static UserData()
    {
        Load();
    }
}

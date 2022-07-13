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

    public static string UserId { get; set; }
    public static string UserName { get; set; }
    public static string UserToken { get; set; }
    public static float Brightness { get; set; }
}

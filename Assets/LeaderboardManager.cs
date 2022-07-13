using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class LeaderboardManager : MonoBehaviour
{
    public class Entry
    {
        [JsonProperty("n")]
        public string Name { get; set; }
        [JsonProperty("h")]
        public float Height { get; set; }
        [JsonProperty("c")]
        public int Combo { get; set; }
    }

    [SerializeField] Transform _entryContainer = null;

    private void Awake()
    {
        StartCoroutine(LoadLeaderboard());
    }

    private IEnumerator LoadLeaderboard()
    {
        var request = new UnityWebRequest("http://141.164.53.243:3002/leaderboard");
        request.method = UnityWebRequest.kHttpVerbGET;
        request.SetRequestHeader("Content-Type", "application/json");
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler);

            var leaderboard = JsonConvert.DeserializeObject<List<Entry>>(request.downloadHandler.text);

            foreach (var entry in leaderboard)
            {
                var entryObject = PoolManager<LeaderboardEntry>.Get(_entryContainer);
                entryObject.Name = entry.Name;
                entryObject.Height = entry.Height;
                entryObject.Combo = entry.Combo;
            }
        }
    }

    public void SubmitRecord(string name, float height, int combo)
    {
        StartCoroutine(SubmitRecordCoroutine(name, height, combo));
    }

    private IEnumerator SubmitRecordCoroutine(string name, float height, int combo)
    {
        var entry = new Entry
        {
            Name = name,
            Height = height,
            Combo = combo
        };
        var request = new UnityWebRequest("http://141.164.53.243:3002/leaderboard");
        request.method = UnityWebRequest.kHttpVerbPOST;
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(entry)));
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Error: " + request.error);
        }
    }
}

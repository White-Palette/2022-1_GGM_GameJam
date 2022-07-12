using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System;
using DG.Tweening;

public class ServerManager : MonoSingleton<ServerManager>
{
    public class Packet
    {
        [JsonProperty("t")]
        public string Type;
        [JsonProperty("p")]
        public string Payload;

        public Packet(string type, string payload)
        {
            Type = type;
            Payload = payload;
        }
    }

    public class RealtimeLeaderboardEntry
    {
        [JsonProperty("i")]
        public int Id;
        [JsonProperty("h")]
        public float Height;

        public RealtimeLeaderboardEntry(int id, float height)
        {
            Id = id;
            Height = height;
        }
    }

    public class RealtimeLeaderboardPacket
    {
        [JsonProperty("l")]
        public List<RealtimeLeaderboardEntry> Leaderboard;

        public RealtimeLeaderboardPacket(List<RealtimeLeaderboardEntry> leaderboard)
        {
            Leaderboard = leaderboard;
        }
    }

    private WebSocket ws;

    public class JoinPacket
    {
        [JsonProperty("i")]
        public int Id;
        [JsonProperty("n")]
        public string Name;
        [JsonProperty("c")]
        public string Color;
    }

    public class LeavePacket
    {
        [JsonProperty("i")]
        public int Id;
    }

    private Queue<Action> taskQueue = new Queue<Action>();

    public Action OnConnected;
    public Action<JoinPacket> OnJoin;
    public Action<LeavePacket> OnLeave;
    public Action<RealtimeLeaderboardPacket> OnLeaderboard;

    private void Awake()
    {
        ws = new WebSocket("ws://localhost:3002/");
        ws.EmitOnPing = true;

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("Connected");
            SendName("Player");
            taskQueue.Enqueue(() =>
            {
                OnConnected?.Invoke();
            });
        };
        ws.OnMessage += (sender, e) =>
        {
            try
            {
                if (e.Data == "" || e.Data == null)
                    return;

                Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);
                switch (packet.Type)
                {
                    case "i":
                        Debug.Log("Id: " + packet.Payload);
                        break;
                    case "li":
                        RealtimeLeaderboardPacket leaderboardPacket = JsonConvert.DeserializeObject<RealtimeLeaderboardPacket>(packet.Payload);
                        taskQueue.Enqueue(() =>
                        {
                            OnLeaderboard?.Invoke(leaderboardPacket);
                        });
                        break;
                    case "j":
                        JoinPacket joinPacket = JsonConvert.DeserializeObject<JoinPacket>(packet.Payload);
                        taskQueue.Enqueue(() =>
                        {
                            OnJoin?.Invoke(joinPacket);
                        });
                        break;
                    case "l":
                        LeavePacket leavePacket = JsonConvert.DeserializeObject<LeavePacket>(packet.Payload);
                        taskQueue.Enqueue(() =>
                        {
                            OnLeave?.Invoke(leavePacket);
                        });
                        break;
                    default:
                        Debug.Log($"Unknown packet type: {packet.Type}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        };
        ws.OnClose += (sender, e) =>
        {
            Debug.Log("Closed");
        };
        ws.OnError += (sender, e) =>
        {
            Debug.Log("Error: " + e.Message);
        };
        ws.Connect();
    }

    public void SendHeight(float height)
    {
        ws.Send(JsonConvert.SerializeObject(new Packet("h", height.ToString())));
    }

    public void SendName(string name)
    {
        ws.Send(JsonConvert.SerializeObject(new Packet("n", name)));
    }

    private void OnDestroy()
    {
        ws.Close();
        DOTween.KillAll();
    }

    private void Update()
    {
        if (taskQueue.Count > 0)
        {
            taskQueue.Dequeue()();
        }
    }
}

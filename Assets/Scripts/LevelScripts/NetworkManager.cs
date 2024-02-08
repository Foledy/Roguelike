using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerSample;
    [SerializeField] private Transform[] _playerSpawns;
    
    private void Start()
    {
        PhotonNetwork.SendRate = 40;
        PhotonNetwork.SerializationRate = 40;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        var options = new RoomOptions()
        {
            MaxPlayers = 4,
            IsVisible = false
        };

        PhotonNetwork.JoinOrCreateRoom("testAB", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        var id = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log($"Current players: {PhotonNetwork.CurrentRoom.PlayerCount}");
        
        if (id > _playerSpawns.Length + 1)
        {
            throw new NullReferenceException($"[Network Manager] No spawn point for player with id = {id}");
        }

        PhotonNetwork.Instantiate(_playerSample.name, _playerSpawns[id - 1].position, Quaternion.identity);
    }
}
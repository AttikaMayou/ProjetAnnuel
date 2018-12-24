using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;

namespace LastToTheGlobe.Scripts
{
    public class PhotonHelloWorldScript : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private Button CreateRoomButton;

        [SerializeField]
        private Button JoinRoomButton;

        [SerializeField]
        private Text welcomeMessageText;

        [SerializeField]
        private PhotonView myPhotonView;

        void Start()
        {
            CreateRoomButton.interactable = false;
            JoinRoomButton.interactable = false;
            PhotonNetwork.ConnectUsingSettings();
        }

        void Update()
        {
            if(PhotonNetwork.IsMasterClient)
            {
                return;
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                myPhotonView.RPC("SayHello", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.ActorNumber);
            }
        }

        public override void OnConnectedToMaster()
        {
            CreateRoomButton.interactable = true;
            JoinRoomButton.interactable = true;

            CreateRoomButton.onClick.AddListener(AskForRoomCreation);
            JoinRoomButton.onClick.AddListener(AskForRoomJoin);
        }

        public void AskForRoomCreation()
        {
            PhotonNetwork.CreateRoom("ABD");
        }

        public void AskForRoomJoin()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedRoom()
        {
            welcomeMessageText.text = PhotonNetwork.IsMasterClient ? "RoomCreated !" : "RoomJoined !";
        }

        [PunRPC]
        public void SayHello(int playerNum)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                welcomeMessageText.text = $"Client {playerNum} said Hello !";
                myPhotonView.RPC("SayHello", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber);
            }
            else
            {
                welcomeMessageText.text = "Server said Hello !";
            }
        }
    }
}
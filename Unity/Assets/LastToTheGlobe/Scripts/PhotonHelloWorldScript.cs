using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;
using UnityEngine.Serialization;

namespace LastToTheGlobe.Scripts
{
    public class PhotonHelloWorldScript : MonoBehaviourPunCallbacks
    {
        [FormerlySerializedAs("CreateRoomButton")] [SerializeField]
        private Button _createRoomButton;

        [FormerlySerializedAs("JoinRoomButton")] [SerializeField]
        private Button _joinRoomButton;

        [FormerlySerializedAs("welcomeMessageText")] [SerializeField]
        private Text _welcomeMessageText;

        [FormerlySerializedAs("myPhotonView")] [SerializeField]
        private PhotonView _myPhotonView;

        void Start()
        {
            _createRoomButton.interactable = false;
            _joinRoomButton.interactable = false;
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
                _myPhotonView.RPC("SayHello", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.ActorNumber);
            }
        }

        public override void OnConnectedToMaster()
        {
            _createRoomButton.interactable = true;
            _joinRoomButton.interactable = true;

            _createRoomButton.onClick.AddListener(AskForRoomCreation);
            _joinRoomButton.onClick.AddListener(AskForRoomJoin);
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
            _welcomeMessageText.text = PhotonNetwork.IsMasterClient ? "RoomCreated !" : "RoomJoined !";
        }

        [PunRPC]
        public void SayHello(int playerNum)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                _welcomeMessageText.text = $"Client {playerNum} said Hello !";
                _myPhotonView.RPC("SayHello", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber);
            }
            else
            {
                _welcomeMessageText.text = "Server said Hello !";
            }
        }
    }
}
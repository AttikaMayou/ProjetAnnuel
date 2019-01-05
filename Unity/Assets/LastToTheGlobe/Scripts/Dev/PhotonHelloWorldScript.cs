using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LastToTheGlobe.Scripts.Dev
{
    public class PhotonHelloWorldScript : MonoBehaviourPunCallbacks
    {
    
    #region Public Variable

        /// <summary>
        /// The PUN LogLevel
        /// </summary>
        public PunLogLevel LogLevel = PunLogLevel.Full;

        [Tooltip("Maximum number of players per room. When room full, new room is created")]
        public byte MaxPlayerPerRoom = 4;
        
    #endregion
        
    #region Private Variables

        [SerializeField] private string _gameVersion = "1";

        /// <summary>
        /// Keep track of current process.
        /// Used for the OnConnectedToMaster() callback.
        /// </summary>
        [SerializeField] private bool _isConnecting;
        
        [FormerlySerializedAs("CreateRoomButton")] [SerializeField]
        private Button _createRoomButton;

        [FormerlySerializedAs("JoinRoomButton")] [SerializeField]
        private Button _joinRoomButton;

        [SerializeField] private Button _connectButton;

        [FormerlySerializedAs("welcomeMessageText")] [SerializeField]
        private Text _welcomeMessageText;

        [FormerlySerializedAs("myPhotonView")] [SerializeField]
        private PhotonView _myPhotonView;
        
    #endregion
       
    #region MonoBehaviour Callbacks

        private void Awake()
        {
            PhotonNetwork.LogLevel = LogLevel;
            
            //TODO: make this false when we finally handle loading properly
            PhotonNetwork.AutomaticallySyncScene = true;
            _createRoomButton.interactable = false;
            _joinRoomButton.interactable = false;
            _connectButton.onClick.AddListener(Connect);
            _connectButton.interactable = true;
        }
        
        private void Start()
        {
//            _createRoomButton.interactable = false;
//            _joinRoomButton.interactable = false;
//            _connectButton.interactable = true;
//            _connectButton.onClick.AddListener(Connect);
        }

        private void Update()
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
        
    #endregion

    #region Public Methods
        
        public void AskForRoomCreation()
        {
            PhotonNetwork.CreateRoom("ABD");
        }

        public void AskForRoomJoin()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public void Connect()
        {
            if (!PhotonNetwork.IsConnected)
            {
                Debug.Log("connecting...");
                PhotonNetwork.ConnectUsingSettings();
            }
            _isConnecting = true;
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
        
    #endregion
        
    #region Photon Callbacks
        
        public override void OnConnectedToMaster()
        {
            _createRoomButton.interactable = true;
            _joinRoomButton.interactable = true;

            _createRoomButton.onClick.AddListener(AskForRoomCreation);
            _joinRoomButton.onClick.AddListener(AskForRoomJoin);
        }

        public override void OnJoinedRoom()
        {
            _welcomeMessageText.text = PhotonNetwork.IsMasterClient ? "RoomCreated !" : "RoomJoined !";
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                PhotonNetwork.LoadLevel("Room for 1");
            }
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogError("OnJoinRoomFailed() was called by PUN. \nCreate a new room.");
            PhotonNetwork.CreateRoom(null, new RoomOptions() {MaxPlayers = MaxPlayerPerRoom}, null);
        }
        
        #endregion
        
    }
}
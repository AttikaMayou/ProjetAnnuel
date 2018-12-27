using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace LastToTheGlobe.Scripts
{
    public class Launcher : MonoBehaviourPun
    {
        #region Public Variables

        /// <summary>
        /// The PUN loglevel
        /// </summary>
        public PunLogLevel LogLevel = PunLogLevel.Informational;

        [Tooltip(
            "The maximum number of players per room. When a room is full, it can't be joined by new players and so new room will be created")]
        public byte MaxPlayersPerRoom = 4;
        
        #endregion 
    
        #region Private Variables

        [SerializeField] private string _gameVersion = "1";

        #endregion
    
        #region MonoBehaviour Callbacks

        private void Awake()
        {
            PhotonNetwork.LogLevel = LogLevel;
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            
        }
    
        #endregion 
    
        #region Public Methods

        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
            }
        }
    
        #endregion
    
        #region Photon.PunBehaviour CallBacks

        public void OnConnectedToServer()
        {
            Debug.Log("DemoAnimator/Launcher: OnConnectedToServer() was called by PUN");
        }

        //TODO: find the not obsolete parameter to put here
        private void OnDisconnectedFromServer() 
        {
            Debug.LogWarning("DemoaAnimator:Launcher: OnDisconnectedFromPhoton() was called bu PUN");
        }
        
        //TODO: find the method that has to be override
        public /*override*/ void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log(
                "DemoAnimator/Launcher: OnPhotonRandomJoinFailed() was called by PUN. No random room available, so e created one. \nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);");

            PhotonNetwork.CreateRoom(null, new RoomOptions() {MaxPlayers = MaxPlayersPerRoom}, null);
        }

//        public override void OnJoinedRoom()
//        {
//            Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() was called by PUN. Now this client is in a room");
//        }
    
        #endregion

    }
}

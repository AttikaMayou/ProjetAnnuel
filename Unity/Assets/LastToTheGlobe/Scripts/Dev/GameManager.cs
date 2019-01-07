using System.Linq;
using System.Security.Cryptography;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LastToTheGlobe.Scripts.Dev
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Public Variable

        /// <summary>
        /// The PUN LogLevel
        /// </summary>
        public PunLogLevel LogLevel = PunLogLevel.Full;

        [Tooltip("Maximum number of players per room. When room is full, new room is created")]
        public byte MaxPlayerPerRoom = 4;
        
        #endregion
        
        #region Private Variables

        [SerializeField] private string _gameVersion = "1";

        /// <summary>
        /// Keep track of current process.
        /// Used for the OnConnectedToMaster() callback.
        /// </summary>
        [SerializeField] private bool _isConnecting;

        [SerializeField] private Button _connectButton;

        [SerializeField] private Text _loadingMessage;

        [SerializeField] private PhotonView _myPhotonView;
        
        #endregion
        
        #region MonoBehaviour Callbacks

        private void Awake()
        {
            PhotonNetwork.LogLevel = LogLevel;
            //TODO : make this false when finally handle loading properly
            PhotonNetwork.AutomaticallySyncScene = true;
            //_connectButton.onClick.AddListener();
            _connectButton.interactable = true;
        }
        
        #endregion
        
        #region Photon Messages

        /// <summary>
        /// Called when the player left the room. We need to load the Launcher Scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            //TODO : handle this properly (not by index)
            //See the SceneSwitcher script in resources
            SceneManager.LoadScene(0);
        }
        
        /*
         * public override void OnPhotonPlayerConnected(PhotonPlayer other)
         * {
         *    Debug.Log('OnPhotonPlayerConnected() " + other.NickName);
         *
         *     if(PhotonNetwork.isMasterClient)
         *     {
         *         Debug.Log("OnPhotonPlayerConnected is MasterClient " + PhotonNetwork.isMasterClient);
         *
         *         LoadArena();
         * 
         *     }
         * }
         *
         *
         * public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
         * {
         *    Debug.Log("OnPhotonPlayerDisconnected() " + other.NickName);
         * 
         *     if(PhotonNetwork.isMasterClient)
         *     {
         *         Debug.Log("OnPhotonPlayerDisconnected isMasterClient " + PhotonNetwork.isMasterClient);
         *
         *         LoadArena();
         * 
         *     }   
         * }
         */
        
        #endregion
        
        #region Photon Callbacks

        
        
        #endregion
        
        #region Public Methods

        public void Connect()
        {
            if (!PhotonNetwork.IsConnected)
            {
                _loadingMessage.text = "Connecting...";
                PhotonNetwork.ConnectUsingSettings();
            }
            _isConnecting = true;
        }
        
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        
        #endregion
        
        #region Private Methods

        private void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to load a level but we are not the Master Client");
            }
            else
            {
                Debug.Log("PhotonNetwork : LoadingLevel : " + PhotonNetwork.CurrentRoom.PlayerCount);
                PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
            }
        }
        
        #endregion

    }
}

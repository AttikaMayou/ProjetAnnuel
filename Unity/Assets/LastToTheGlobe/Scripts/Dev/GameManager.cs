using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LastToTheGlobe.Scripts.Dev
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        
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
        
        #region Public Methods

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

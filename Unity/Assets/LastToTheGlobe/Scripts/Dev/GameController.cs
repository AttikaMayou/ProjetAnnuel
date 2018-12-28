using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LastToTheGlobe.Scripts.Dev
{
    public class GameController : MonoBehaviourPunCallbacks
    {
        #region PhotonMessage

        public override void OnLeftRoom()
        {
            //TODO : handle this properly (not by index)
            SceneManager.LoadScene(0);
        }
        
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("OnPlayerEnteredRoom() " + newPlayer.NickName);

            if (PhotonNetwork.IsMasterClient)
            {
                LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("OnPlayerLeftRoom() " + otherPlayer.NickName);
            
            if (PhotonNetwork.IsMasterClient)
            {
                LoadArena();
            }
            
        }

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
                PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
            }
        }
        
        #endregion
    }
}

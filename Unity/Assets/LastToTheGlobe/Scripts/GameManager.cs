using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LastToTheGlobe.Scripts
{
    public class GameManager : MonoBehaviour
    {
        
        #region Photon Messages

        /// <summary>
        /// Called when the player left the room. We need to load the Launcher Scene.
        /// </summary>
        public void OnLeftRoom()
        {
            //TODO : handle this properly (not by index)
            //See the SceneSwitcher script in ressources
            SceneManager.LoadScene(0);
        }
        
        #endregion
        
        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        
        #endregion

    }
}

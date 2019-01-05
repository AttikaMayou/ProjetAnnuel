using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace LastToTheGlobe.Scripts.Dev
{
    public class DebugButton : MonoBehaviour
    {

        [SerializeField] private Button _btn;

        private void Awake()
        {
            _btn.onClick.AddListener(DebugClick);
        }

        void Start()
        {
            
        }

        void Update()
        {
        
        }

        public void DebugClick()
        {
            Debug.Log("Clicked");
        }
    }
}

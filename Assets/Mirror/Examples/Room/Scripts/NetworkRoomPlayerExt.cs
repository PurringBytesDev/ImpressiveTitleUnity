using TMPro;
using UnityEngine;

namespace Mirror.Examples.NetworkRoom
{
    [AddComponentMenu("")]
    public class NetworkRoomPlayerExt : NetworkRoomPlayer
    {
        public TextMeshProUGUI playerUiTest;
        public override void OnStartClient()
        {
            //Debug.Log($"OnStartClient {gameObject}");
        }

        public override void OnClientEnterRoom()
        {
            //Debug.Log($"OnClientEnterRoom {SceneManager.GetActiveScene().path}");
        }

        public override void OnClientExitRoom()
        {
            //Debug.Log($"OnClientExitRoom {SceneManager.GetActiveScene().path}");
        }

        public override void IndexChanged(int oldIndex, int newIndex)
        {
            //Debug.Log($"IndexChanged {newIndex}");
        }
        public override void CarIndexChanged(int oldIndex, int newIndex)
        {
            Debug.Log($"IndexChanged {newIndex}");
        }

        public override void ReadyStateChanged(bool oldReadyState, bool newReadyState)
        {
            Debug.Log($"ReadyStateChanged {newReadyState}");
        }

        public override void OnGUI()
        {
            base.OnGUI();
        }
        private bool uiNotSet = true;
        private int carIndice = 0;
        public void Update()
        {
            /*
            if(GetComponent<NetworkIdentity>().isOwned)
            {
                Debug.Log("i own this object");
            }
            else
            {
                Debug.Log("i do not own this object");
            }
            */
            if(!GetComponent<NetworkIdentity>().isOwned && uiNotSet)
            {
                var pos = playerUiTest.gameObject.transform.parent.localPosition;
                pos.x += 180f;
                playerUiTest.gameObject.transform.parent.localPosition = pos;

                Debug.Log("debug not me " + playerUiTest.text + " index " + GetComponent<NetworkRoomPlayerExt>().carIndex);
                uiNotSet = false;
            }

            // safeguard, actually need to change direct component call to an only once ref..
            if(GetComponent<NetworkIdentity>().isOwned)
            {
                if (Input.GetKeyUp(KeyCode.KeypadPlus))
                {
                    Debug.Log("car index ? " + carIndice);
                    if (carIndice < 3)
                    {
                        carIndice++;
                    }
                    else if (carIndice == 3)
                    {
                        carIndice = 0;
                    }
                    base.CmdChangeCarIndex(carIndice);
                }
            }

            if (!GetComponent<NetworkIdentity>().isOwned)
            {
                playerUiTest.text = "Not Me " + GetComponent<NetworkRoomPlayerExt>().carIndex;
            }
            if (GetComponent<NetworkIdentity>().isOwned)
            {
                playerUiTest.text = "Car Index : " + carIndice;
            }

        }
        /*
        public void LateUpdate()
        {
            if (!GetComponent<NetworkIdentity>().isOwned && !uiNotSet)
            {
                playerUiTest.text = "Not Me " + GetComponent<NetworkRoomPlayerExt>().carIndice;
            }
            if (GetComponent<NetworkIdentity>().isOwned)
            {
                playerUiTest.text = "Car Index : " + carIndice;
            }
        }
        */
    }
}

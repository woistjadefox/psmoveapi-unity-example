using UnityEngine;

public class TestConnection : MonoBehaviour {

    private System.IntPtr handle;
    private bool initDone = false;

	private void Start () {

        PSMove_Bool init = PSMoveAPI.psmove_init(PSMoveAPI.PSMove_Version.PSMOVE_CURRENT_VERSION);

        if(init == PSMove_Bool.PSMove_True) {

            handle = PSMoveAPI.psmove_connect();

            if(handle == System.IntPtr.Zero || PSMoveAPI.psmove_update_leds(handle) == 0) {
                Debug.LogError("Could not connect to default PSMove controller");
            } else {
                Debug.Log("Connection established to default PSMove controller");
                initDone = true;
                SetLED(Color.magenta);
            }

        } else {
            Debug.LogError("Could not init PSMove API");
        }
    }

    public void SetLED(Color color) {
        PSMoveAPI.psmove_set_leds(handle, (char)(color.r * 255), (char)(color.g * 255), (char)(color.b * 255));
        
    }

    private void Update() {
        if (initDone) {
            PSMoveAPI.psmove_update_leds(handle);
        }
    }

    private void OnApplicationQuit() {
        PSMoveAPI.psmove_disconnect(handle);
    }
}

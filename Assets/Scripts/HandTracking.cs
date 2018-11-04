using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class HandTracking : MonoBehaviour
{
    private void OnEnable()
    {
        MLResult result = MLHands.Start();
        if (!result.IsOk)
        {
            Debug.LogError("Error GesturesExample starting MLHands, disabling script.");
            enabled = false;
        }
        else
        {
            MLHandKeyPose[] poseSet = {MLHandKeyPose.Pinch, MLHandKeyPose.C, MLHandKeyPose.L, MLHandKeyPose.Thumb};
            var status = MLHands.KeyPoseManager.EnableKeyPoses(poseSet, true, true);
            if (!status)
            {
                Debug.LogError("HandTracking failed during a call to enable tracked KeyPoses.\n"
                               + "Disabling HandTracking component.");
                enabled = false;
            }
        }
    }

    private void OnDisable()
    {
        MLHands.Stop();
    }
}
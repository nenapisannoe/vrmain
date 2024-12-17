using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] HingeJoint doorJoint;

    JointLimits doorJointLimits;
    void Start()
    {
        doorJointLimits = doorJoint.limits;
        Lock();
    }

    private void OnEnable() {
        Keyhole.UnlockDoor += Unlock;
    }

        private void OnDisable() {
        Keyhole.UnlockDoor -= Unlock;
    }

    void Lock()
    {
        var newLimits = doorJoint.limits;
        newLimits.min = 0;
        newLimits.max = 0;
        doorJoint.limits = newLimits;
    }

    void Unlock()
    {
        doorJoint.limits = doorJointLimits;
    }

}

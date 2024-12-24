using Unity.Collections;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Hands.ProviderImplementation;

[Preserve]
class TestHandProvider : XRHandSubsystemProvider
{
    public TestHandProvider()
    {
        leftHandJointsTrackingStates = new XRHandJointTrackingState[XRHandJointID.EndMarker.ToIndex()];
        rightHandJointsTrackingStates = new XRHandJointTrackingState[XRHandJointID.EndMarker.ToIndex()];

        System.Array.Fill(leftHandJointsTrackingStates, XRHandJointTrackingState.Pose | XRHandJointTrackingState.HighFidelityPose);
        System.Array.Fill(rightHandJointsTrackingStates, XRHandJointTrackingState.Pose | XRHandJointTrackingState.HighFidelityPose);
    }

    public int numStartCalls { get; private set; }
    public int numStopCalls { get; private set; }
    public int numDestroyCalls { get; private set; }
    public int numGetHandLayoutCalls { get; private set; }
    public int numTryUpdateHandsCalls { get; private set; }
    public XRHandSubsystem.UpdateType mostRecentUpdateType { get; private set; }

    public bool leftHandIsTracked { get; set; } = true;

    public bool rightHandIsTracked { get; set; } = true;

    public XRHandJointTrackingState[] leftHandJointsTrackingStates { get; set; }
    public XRHandJointTrackingState[] rightHandJointsTrackingStates { get; set; }

    public override void Start()
    {
        leftHandIsTracked = true;
        rightHandIsTracked = true;
        ++numStartCalls;
    }

    public override void Stop()
    {
        ++numStopCalls;
    }

    public override void Destroy()
    {
        ++numDestroyCalls;
    }

    public override void GetHandLayout(NativeArray<bool> jointsInLayout)
    {
        ++numGetHandLayoutCalls;
        for (int jointIndex = 0; jointIndex < jointsInLayout.Length; ++jointIndex)
            jointsInLayout[jointIndex] = TestHandData.jointsInLayout[jointIndex];
    }

    public override XRHandSubsystem.UpdateSuccessFlags TryUpdateHands(
        XRHandSubsystem.UpdateType updateType,
        ref Pose leftHandRootPose,
        NativeArray<XRHandJoint> leftHandJoints,
        ref Pose rightHandRootPose,
        NativeArray<XRHandJoint> rightHandJoints)
    {
        mostRecentUpdateType = updateType;
        ++numTryUpdateHandsCalls;

        leftHandRootPose = TestHandData.leftRoot;
        rightHandRootPose = TestHandData.rightRoot;
        for (int jointIndex = 0; jointIndex < TestHandData.jointsInLayout.Length; ++jointIndex)
        {
            if (!TestHandData.jointsInLayout[jointIndex])
                continue;

            XRHandJointTrackingState leftHandTrackingState =
                leftHandIsTracked ? leftHandJointsTrackingStates[jointIndex] : XRHandJointTrackingState.None;

            XRHandJointTrackingState rightHandTrackingState =
                rightHandIsTracked ? rightHandJointsTrackingStates[jointIndex] : XRHandJointTrackingState.None;

            leftHandJoints[jointIndex] = XRHandProviderUtility.CreateJoint(
                Handedness.Left,
                leftHandTrackingState,
                XRHandJointIDUtility.FromIndex(jointIndex),
                TestHandData.leftHand[jointIndex]);

            rightHandJoints[jointIndex] = XRHandProviderUtility.CreateJoint(
                Handedness.Right,
                rightHandTrackingState,
                XRHandJointIDUtility.FromIndex(jointIndex),
                TestHandData.rightHand[jointIndex]);
        }

        var successFlags = XRHandSubsystem.UpdateSuccessFlags.All;

        if (!leftHandIsTracked)
            successFlags &= ~XRHandSubsystem.UpdateSuccessFlags.LeftHandJoints & ~XRHandSubsystem.UpdateSuccessFlags.LeftHandRootPose;

        if (!rightHandIsTracked)
            successFlags &= ~XRHandSubsystem.UpdateSuccessFlags.RightHandJoints & ~XRHandSubsystem.UpdateSuccessFlags.RightHandRootPose;

        return successFlags;
    }

    public static string descriptorId => "Test-Hands";
}

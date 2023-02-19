using Microsoft.MixedReality.Toolkit.Subsystems;
using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes;

public class GestureControl : MonoBehaviour
{
    public GameObject CCW;
    public GameObject CW;

    ROSConnection ros;
    public string topicName = "rotary_cmd";

    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 0.5f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        CCW.SetActive(false);
        CW.SetActive(false);

        // start the ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<RosMessageTypes.Std.Float32Msg>(topicName);
    }

    // Update is called once per frame
    void Update()
    {

        timeElapsed += Time.deltaTime;

        

        if (timeElapsed > publishMessageFrequency)
        {
            var aggregator = XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>();
            bool handRot1 =
                aggregator.TryGetPinchProgress(XRNode.LeftHand,
                out bool isReadyToPinchL,
                out bool isPinchingL,
                out float pinchAmountL);

            bool handRot2 =
                aggregator.TryGetPalmFacingAway(XRNode.LeftHand,
                out bool isPalmFacingAwayL);

            bool handRot3 =
                aggregator.TryGetPinchProgress(XRNode.RightHand,
                out bool isReadyToPinchR,
                out bool isPinchingR,
                out float pinchAmountR);

            bool handRot4 =
                aggregator.TryGetPalmFacingAway(XRNode.RightHand,
                out bool isPalmFacingAwayR);

            RosMessageTypes.Std.Float32Msg rotary_msg;

            CCW.SetActive(false);
            CW.SetActive(false);

            if (isPinchingL)
            {
                
                rotary_msg = new RosMessageTypes.Std.Float32Msg(0.1f);
                CCW.SetActive(true);
                // Finally send the message to server_endpoint.py running in ROS
                ros.Publish(topicName, rotary_msg);
            } else if (isPinchingR)
            {
                
                rotary_msg = new RosMessageTypes.Std.Float32Msg(0.2f);
                CW.SetActive(true);
                // Finally send the message to server_endpoint.py running in ROS
                ros.Publish(topicName, rotary_msg);
            }
            // else if (isPalmFacingAwayR)
            // {
                
            //    rotary_msg = new RosMessageTypes.Std.Float32Msg(0.4f);

                // Finally send the message to server_endpoint.py running in ROS
            //    ros.Publish(topicName, rotary_msg);
            // }





            Debug.Log("msg published");
            timeElapsed = 0;
        }

    }
}

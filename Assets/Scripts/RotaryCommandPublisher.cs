using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes;

public class RotaryCommandPublisher : MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "rotary_cmd";

    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 0.5f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;


    // Start is called before the first frame update
    void Start()
    {
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
            RosMessageTypes.Std.Float32Msg rotary_msg = new RosMessageTypes.Std.Float32Msg(0.1f);

            // Finally send the message to server_endpoint.py running in ROS
            ros.Publish(topicName, rotary_msg);

            Debug.Log("msg published");
            timeElapsed = 0;
        }
    }
}

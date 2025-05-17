using BarRaider.SdTools;
using Newtonsoft.Json;

namespace StreamDeckUnity.Keys
{
    [PluginActionId("com.nicollasr.streamdeckunity.executemethod")]
    public class ExecuteMethod : Key<ExecuteMethodSettings>
    {
        public ExecuteMethod(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
        }

        public override void KeyPressed(KeyPayload payload)
        {
            base.KeyPressed(payload);

            var pid = GetForeground().processId;

            if (IsUnityForeground(pid))
            {
                MessageServer.Send(pid.ToString(), new ExecuteMethodMessage(settings.Component,settings.Method));
            }
        }
    }

    public class ExecuteMethodMessage : Message
    {
        public string Component { get; }
        public string Method { get; }
        public ExecuteMethodMessage(string component, string method)
        {
            Component = component;
            Method = method;
        }
    }

    public class ExecuteMethodSettings : KeySettings
    {
        [JsonProperty("component")]
        public string Component { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
    }
}
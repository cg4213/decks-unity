using BarRaider.SdTools;
using Newtonsoft.Json;

namespace StreamDeckUnity.Keys
{
    [PluginActionId("com.nicollasr.streamdeckunity.executestaticmethod")]
    public class ExecuteStaticMethod : Key<ExecuteStaticMethodSettings>
    {
        public ExecuteStaticMethod(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
        }

        public override void KeyPressed(KeyPayload payload)
        {
            base.KeyPressed(payload);

            var pid = GetForeground().processId;

            if (IsUnityForeground(pid))
            {
                MessageServer.Send(pid.ToString(), new ExecuteStaticMethodMessage(settings.TypeName, settings.MethodName));
            }
        }
    }

    public class ExecuteStaticMethodMessage : Message
    {
        public string TypeName { get; }
        public string MethodName { get; }
        public ExecuteStaticMethodMessage(string typeName, string methodName)
        {
            TypeName = typeName;
            MethodName = methodName;
        }
    }

    public class ExecuteStaticMethodSettings : KeySettings
    {
        [JsonProperty("typename")]
        public string TypeName { get; set; }
        [JsonProperty("methodname")]
        public string MethodName { get; set; }
    }
}
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
                MessageServer.Send(
                    pid.ToString(), 
                    new ExecuteStaticMethodMessage(
                        settings.TypeName, 
                        settings.MethodName,
                        settings.ParamType,
                        settings.ParamValue
                        ));
            }
        }
    }

    public class ExecuteStaticMethodMessage : Message
    {
        public string TypeName { get; }
        public string MethodName { get; }
        public int ParamType { get; }
        public string ParamValue { get; }

        public ExecuteStaticMethodMessage(string typeName, string methodName, int paramType, string paramValue)
        {
            TypeName = typeName;
            MethodName = methodName;
            ParamType = paramType;
            ParamValue = paramValue;
        }
    }

    public class ExecuteStaticMethodSettings : KeySettings
    {
        [JsonProperty("typename")]
        public string TypeName { get; set; }
        [JsonProperty("methodname")]
        public string MethodName { get; set; }
        [JsonProperty("paramtype")]
        public int ParamType { get; set; }
        [JsonProperty("paramvalue")]
        public string ParamValue { get; set; }

    }
}
using UnityEngine;

namespace PlayerActivity
{
    public struct LogData : ISerializableParameter
    {
        public double time;
        public Vector3 position;
        public string eventName;
        public string parameters;

        public void Deserialize(ref ZPackage pkg)
        {
            time = pkg.ReadDouble();
            position = pkg.ReadVector3();
            eventName = pkg.ReadString();
            parameters = pkg.ReadString();
        }

        public void Serialize(ref ZPackage pkg)
        {
            pkg.Write(time);
            pkg.Write(position);
            pkg.Write(eventName);
            pkg.Write(parameters);
        }

        public override string ToString() => $"{position} {eventName} {parameters}";
    }
}

using Ardalis.SmartEnum;

namespace SmartHomeServer.Enums;

public sealed class SensorTypeSmartEnum : SmartEnum<SensorTypeSmartEnum>
{
    public static readonly SensorTypeSmartEnum Relay = new SensorTypeSmartEnum("Relay", 1);
    public static readonly SensorTypeSmartEnum Temp = new SensorTypeSmartEnum("Temperature", 2);
    public static readonly SensorTypeSmartEnum Hum = new SensorTypeSmartEnum("Humidity", 3);
    public static readonly SensorTypeSmartEnum Ldr = new SensorTypeSmartEnum("Ldr", 4);
    public static readonly SensorTypeSmartEnum Water = new SensorTypeSmartEnum("Water", 5);
    public static readonly SensorTypeSmartEnum Pressure = new SensorTypeSmartEnum("Pressure", 6);
    public static readonly SensorTypeSmartEnum Motion = new SensorTypeSmartEnum("Motion", 7);
    public static readonly SensorTypeSmartEnum Gas = new SensorTypeSmartEnum("Gas", 8);
    public static readonly SensorTypeSmartEnum Speed = new SensorTypeSmartEnum("Speed", 9);
    public static readonly SensorTypeSmartEnum Other = new SensorTypeSmartEnum("Other", 0);

    public SensorTypeSmartEnum(string name, int value) : base(name, value)
    {
    }
}

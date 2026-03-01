using DesignPatternsDemo.Observer;

namespace DesignPatternsDemo.Factory;

public static class DisplayFactory
{
    public static Display Create(string type)
    {
        return type.Trim().ToLowerInvariant() switch
        {
            "phone" => new PhoneDisplay(),
            "desktop" => new DesktopDisplay(),
            "controlroom" => new ControlRoomDisplay(),
            "control-room" => new ControlRoomDisplay(),
            _ => throw new ArgumentException($"Unsupported display type: {type}")
        };
    }
}
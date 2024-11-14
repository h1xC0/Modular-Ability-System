namespace Core.Services.ViewLayerService
{
    [Layers]
    public class LayerName
    {
        public const string Default = "Default";
        public const string Screen = "Screen";
        public const string Popup = "Popup";
        public const string Dialog = "Dialog";
        public const string Notification = "Notification";
        public const string Tooltip = "Tooltip";
        // public const string System = "System";
        public const string Debug = "Debug";

        public static readonly string[] Layers =
        {
            Default,
            Screen,
            Popup,
            Dialog,
            Notification,
            Tooltip,
            Debug
        };
    }
}
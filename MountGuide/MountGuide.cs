using Dalamud.Game.Text.SeStringHandling.Payloads;
using ECommons.Configuration;
using ECommons.EzIpcManager;
using ECommons.SimpleGui;
using ECommons.Singletons;
using MountGuide.Services;
using MountGuide.UI;

namespace MountGuide;

public unsafe class MountGuide : IDalamudPlugin
{
    internal static MountGuide P;
    internal Configuration Config;
    public MountGuide(IDalamudPluginInterface pi)
    {
        P = this;
        ECommonsMain.Init(pi, this, Module.DalamudReflector);
        EzConfig.Migrate<Configuration>();
        Config = EzConfig.Init<Configuration>();
        EzConfigGui.Init(new MainWindow());

        EzCmd.Add("/mg", OnChatCommand, "Toggles plugin interface");;
        Svc.Chat.ChatMessage += ChatMessageHandler.Chat_ChatMessage;
        SingletonServiceManager.Initialize(typeof(ServiceManager));
    }

    public void Dispose()
    {
        Svc.Chat.ChatMessage -= ChatMessageHandler.Chat_ChatMessage;
        ECommonsMain.Dispose();
        P = null;
    }

    private void OnChatCommand(string command, string arguments)
    {
        arguments = arguments.Trim();

        if (arguments == string.Empty)
        {
            EzConfigGui.Window.Toggle();
        }
        else if (arguments == "debug")
        {
            Config.Debug = !Config.Debug;
            PluginLog.Information($"Debug: {Config.Debug}");
        }
    }
}

using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using MountGuide.Windows;
using ECommons;

namespace MountGuide;

public sealed class MountGuide : IDalamudPlugin
{
    internal static MountGuide P;
    public MountGuide()
    {
        P = this;
        ECommonsMain
    }

    public void Dispose()
    {
        
    }

}

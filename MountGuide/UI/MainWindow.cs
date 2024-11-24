using Dalamud.Interface.Utility;
using ECommons.SimpleGui;
using System.Collections.Generic;

namespace MountGuide.UI;

public unsafe class MainWindow : ConfigWindow
{
    public MainWindow() : base()
    {
        
    }

    public override void Draw()
    {
        var newGuide = "";
        if (ImGui.InputText("##newGuide", ref newGuide, 50, ImGuiInputTextFlags.EnterReturnsTrue))
        {
            if (newGuide.Length > 0)
            {
                P.Config.Guide = new(newGuide, 0);
                newGuide = "";
            }
        }

        ImGui.Text($"Guide: {P.Config.Guide.Name}");
    }
}

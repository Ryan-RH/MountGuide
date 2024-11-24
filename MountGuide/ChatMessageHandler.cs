using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using MountGuide.Services;

namespace MountGuide;

internal unsafe static class ChatMessageHandler
{
    internal static void Chat_ChatMessage(XivChatType type, int a2, ref SeString sender, ref SeString message, ref bool isHandled)
    {
        var Guide = P.Config.Guide.Name;
        if (Svc.ClientState.LocalPlayer != null && type.EqualsAny(XivChatType.Party, XivChatType.Say, XivChatType.TellIncoming))
        {
            var isMapLink = false;
            var isConductorMessage = (TryDecodeSender(sender, out var s) && Guide == s.Name) || (sender.ToString().Contains(Svc.ClientState.LocalPlayer.Name.ToString())); 
            PluginLog.Information($"Message: {message.ToString()} from {sender}, isConductor = {isConductorMessage}");
            if (isConductorMessage)
            {
                if (message.ToString() == "!r")
                {
                    foreach (var x in Svc.Objects)
                    {
                        if (x.Name.ToString() == s.Name)
                        {
                            ServiceManager.VnavMeshIPC.SimpleMove_PathfindAndMoveTo(x.Position, true);
                        }
                        else if (P.Config.Debug && x.Name.ToString() == Svc.ClientState.LocalPlayer.Name.ToString())
                        {
                            PluginLog.Information(x.Position.ToString());
                        }
                    }
                }
                else
                {
                    foreach (var x in message.Payloads)
                    {
                        if (x is MapLinkPayload m)
                        {
                            isMapLink = true;
                            Svc.GameGui.OpenMapWithMapLink(m);
                            var flag = AgentMap.Instance()->FlagMapMarker;
                            Vector3 coords = new Vector3(flag.XFloat, 1024, flag.YFloat);
                            Vector3 realCoords = ServiceManager.VnavMeshIPC.Query_Mesh_PointOnFloor(coords, true, 0);
                            PluginLog.Information($"({realCoords.X}, {realCoords.Y}, {realCoords.Z})");
                            ServiceManager.VnavMeshIPC.SimpleMove_PathfindAndMoveTo(realCoords, true);
                        }
                    }
                }
            }
        }
    }
}

using ECommons.ChatMethods;
using ECommons.Configuration;

namespace MountGuide;

public class Configuration : IEzConfig
{
    public Sender Guide;
    public bool Debug = false;
}

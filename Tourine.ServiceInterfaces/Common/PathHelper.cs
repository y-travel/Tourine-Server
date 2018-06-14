using System.IO;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Common
{
    public class PathHelper
    {
        public static string GetRootDirectory() => HostContext.AppHost?.VirtualFileSources?.RootDirectory.RealPath ?? "";

        public static string GetAssetsPath() => Path.Combine(GetRootDirectory(), "assets");
    }
}
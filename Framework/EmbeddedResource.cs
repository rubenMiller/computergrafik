using System;
using System.Reflection;
using Zenseless.OpenTK;
using Zenseless.Resources;

namespace Framework;

public static class EmbeddedResource
{
    /// <summary>
    /// Load a texture out of the given embedded resource.
    /// </summary>
    /// <param name="name">The name of the resource that contains an image.</param>
    /// <returns>A Texture2D.</returns>
    public static Texture2D LoadTexture(string name)
    {
        using var stream = resourceDirectory.Resource(name).Open();
        return Texture2DLoader.Load(stream);
    }

    public static IResourceDirectory Directory => resourceDirectory;

    private static readonly IResourceDirectory resourceDirectory = new ShortestMatchResourceDirectory(
        new EmbeddedResourceDirectory(Assembly.GetEntryAssembly() ?? throw new ApplicationException("No entry assembly. Are you calling the code from an unmanaged source?")));
}

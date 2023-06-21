using System;
using System.Collections.Generic;
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
        if (!nameToTexture.TryGetValue(name, out var texture))
        {
            using var stream = resourceDirectory.Resource(name).Open();
            nameToTexture[name] = Texture2DLoader.Load(stream);
            return nameToTexture[name];
        }
        return texture;
    }

    public static IResourceDirectory Directory => resourceDirectory;
    private static Dictionary<string, Texture2D> nameToTexture = new();

    private static readonly IResourceDirectory resourceDirectory = new ShortestMatchResourceDirectory(
        new EmbeddedResourceDirectory(Assembly.GetEntryAssembly() ?? throw new ApplicationException("No entry assembly. Are you calling the code from an unmanaged source?")));
}

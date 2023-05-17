using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Zenseless.Resources;

/// <summary>
/// Class that implements a resource directory on embedded resources
/// </summary>
public class EmbeddedResourceDirectory : IResourceDirectory
{
	/// <summary>
	/// Collects embedded resources of the calling assembly
	/// </summary>
	/// <param name="path">The path prefix to use for accessing embedded resources.</param>
	public EmbeddedResourceDirectory(string path = ""): this(Assembly.GetCallingAssembly(), path)
	{
	}

	/// <summary>
	/// Collects embedded resources of given <see cref="Assembly"/>.
	/// </summary>
	/// <param name="assembly">The <see cref="Assembly"/> in which the resources are stored.</param>
	/// <param name="path">The path prefix to use for accessing embedded resources.</param>
	public EmbeddedResourceDirectory(Assembly assembly, string path = "")
	{
		_assembly = assembly;
		var allNames = _assembly.GetManifestResourceNames();
		_path = string.IsNullOrEmpty(path) ? path : path.TrimEnd('.') + ".";
		_names = allNames.Where(n => n.StartsWith(_path)).Select(n => n[_path.Length..]).ToHashSet();
		if (0 == _names.Count) throw new ArgumentException($"Given path '{path}' does not contain any resources.\nAvailable resoures are {string.Join(',', allNames)}");
	}

	/// <summary>
	/// Enumerates all resources in the directory
	/// </summary>
	/// <returns>A list of resource names</returns>
	public IEnumerable<string> EnumerateResources() => _names;

	/// <summary>
	/// Checks if a resource of the given name exists
	/// </summary>
	/// <param name="name">The name of the resource</param>
	/// <returns><c>true</c> if the specified name is known; otherwise, <c>false</c>.</returns>
	public bool Exists(string name) => _names.Contains(name);

	/// <summary>
	/// Opens the stream with the given name.
	/// </summary>
	/// <param name="name">The name of the resource</param>
	/// <returns></returns>
	public Stream Open(string name)
	{
		var stream = _assembly.GetManifestResourceStream(_path + name);
		if (stream is null)
		{
			var names = string.Join('\n', _names);
			throw new ArgumentException($"Could not find resource '{name}' in resources\n'{names}'");
		}
		return stream;
	}

	/// <summary>
	/// Returns the resource with the given name if it exists, throws an exception otherwise
	/// </summary>
	/// <param name="name">The name of the resource</param>
	/// <returns></returns>
	public IResource Resource(string name)
	{
		if (!Exists(name))
		{
			var names = string.Join('\n', _names);
			throw new ArgumentException($"Could not find resource '{name}' in resources\n'{names}'");
		}
		return new Resource(this, name);
	}

	private readonly Assembly _assembly;
	private readonly string _path;
	private readonly HashSet<string> _names;
}

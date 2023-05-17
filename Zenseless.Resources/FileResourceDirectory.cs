using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zenseless.Resources;

/// <summary>
/// Class that implements a resource directory on the file system
/// </summary>
public class FileResourceDirectory : IResourceDirectory
{
	/// <summary>
	/// Create a new instance
	/// </summary>
	/// <param name="rootDirectory">The root directory.</param>
	/// <param name="recursive">If child directories should also be searched.</param>
	public FileResourceDirectory(string rootDirectory, bool recursive = false)
	{
		RootDirectory = Path.GetFullPath(rootDirectory);
		if (!Directory.Exists(RootDirectory)) throw new ArgumentException($"Could not find root directory '{rootDirectory}' with full path '{RootDirectory}'");
		Recursive = recursive;
	}

	/// <summary>
	/// Enumerates all resources in the directory
	/// </summary>
	/// <returns>A list of resource names</returns>
	public IEnumerable<string> EnumerateResources() => Directory
		.EnumerateFiles(RootDirectory, "*.*", Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
		.Select(filePath => filePath[(RootDirectory.Length + 1)..]);

	/// <summary>
	/// Checks if a resource of the given name exists
	/// </summary>
	/// <param name="name">The name of the resource</param>
	/// <returns><c>true</c> if the specified name is known; otherwise, <c>false</c>.</returns>
	public bool Exists(string name) => File.Exists(GetFileName(name));

	/// <summary>
	/// Opens the stream with the given name.
	/// </summary>
	/// <param name="name">The name of the resource</param>
	/// <returns></returns>
	public Stream Open(string name) => File.OpenRead(GetFileName(name));

	/// <summary>
	/// Returns the resource with the given name if it exists, throws an exception otherwise
	/// </summary>
	/// <param name="name">The name of the resource</param>
	/// <returns></returns>
	public IResource Resource(string name)
	{
		if (!Exists(name)) throw new ArgumentException($"Could not find resource '{name}' in resource directory '{RootDirectory}'");
		return new Resource(this, name);
	}

	/// <summary>
	/// If child directories should also be searched.
	/// </summary>
	public bool Recursive { get; }

	/// <summary>
	/// The root directory.
	/// </summary>
	public string RootDirectory { get; }

	private string GetFileName(string name) => Path.Combine(RootDirectory, name);
}
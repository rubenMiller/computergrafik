using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zenseless.Resources;

/// <summary>
/// A Decorator class that uses shortest match ignore case name resolve for <seealso cref="IResourceDirectory"/> lookups.
/// </summary>
public class ShortestMatchResourceDirectory : IResourceDirectory
{
	/// <summary>
	/// Create a new instance of the decorator for a given resource directory.
	/// </summary>
	/// <param name="resourceDirectory">The <seealso cref="IResourceDirectory"/> to decorate.</param>
	public ShortestMatchResourceDirectory(IResourceDirectory resourceDirectory)
	{
		ResourceDirectory = resourceDirectory;
	}

	/// <summary>
	/// The decorated <seealso cref="IResourceDirectory"/>
	/// </summary>
	public IResourceDirectory ResourceDirectory { get; }

	/// <summary>
	/// Enumerates all resources in the directory
	/// </summary>
	/// <returns>A list of resource names</returns>
	public IEnumerable<string> EnumerateResources() => ResourceDirectory.EnumerateResources();

	/// <summary>
	/// Checks if a resource of the given name exists
	/// </summary>
	/// <param name="name">The name of the resource</param>
	/// <returns><c>true</c> if the specified name is known; otherwise, <c>false</c>.</returns>
	public bool Exists(string name) => Matches(name).Any();

	/// <summary>
	/// Find all resource name matches for a given name.
	/// </summary>
	/// <param name="name">The name to search matches for.</param>
	/// <returns></returns>
	public IEnumerable<string> Matches(string name)
	{
		return EnumerateResources().Where(fullName => fullName.Contains(name, StringComparison.InvariantCultureIgnoreCase));
	}

	/// <summary>
	/// Opens the stream with the given name.
	/// </summary>
	/// <param name="name">The name of the resource</param>
	/// <returns></returns>
	public Stream Open(string name)
	{
		IEnumerable<string> enumerable = Matches(name);
		return ResourceDirectory.Open(enumerable.Single());
	}

	/// <summary>
	/// Returns the resource with the given name if it exists, throws an exception otherwise
	/// </summary>
	/// <param name="name">The name of the resource</param>
	/// <returns></returns>
	public IResource Resource(string name) => ResourceDirectory.Resource(Matches(name).Single());
}

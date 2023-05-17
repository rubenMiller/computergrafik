using System.Collections.Generic;
using System.IO;

namespace Zenseless.Resources;

/// <summary>
/// Interface for resource directories
/// </summary>
public interface IResourceDirectory
{
	/// <summary>
	/// Enumerates all resources in the directory
	/// </summary>
	/// <returns>A list of resource names</returns>
	IEnumerable<string> EnumerateResources();

	/// <summary>
	/// Checks if a resource of the given name exists
	/// </summary>
	/// <param name="name">The name of the resource</param>
	/// <returns><c>true</c> if the specified name is known; otherwise, <c>false</c>.</returns>
	bool Exists(string name);

	/// <summary>
	/// Opens the stream with the given name.
	/// </summary>
	/// <param name="name">The name of the resource</param>
	/// <returns></returns>
	Stream Open(string name);

	/// <summary>
	/// Returns the resource with the given name if it exists, throws an exception otherwise
	/// </summary>
	/// <param name="name">The name of the resource</param>
	/// <returns></returns>
	IResource Resource(string name);
}

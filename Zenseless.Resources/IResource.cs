using System.IO;

namespace Zenseless.Resources;

/// <summary>
/// Interface for resources
/// </summary>
public interface IResource
{
	/// <summary>
	/// The resource directory that contains this resource.
	/// </summary>
	IResourceDirectory Directory { get; }

	/// <summary>
	/// The name of the resource.
	/// </summary>
	string Name { get; }

	/// <summary>
	/// Opens the resource as a stream.
	/// </summary>
	/// <returns></returns>
	Stream Open();
}

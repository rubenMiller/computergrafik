using System.IO;

namespace Zenseless.Resources;

internal class Resource : IResource
{
	/// <summary>
	/// Creates an instance of a resource with the given name and from the given directory
	/// </summary>
	/// <param name="directory"></param>
	/// <param name="name"></param>
	public Resource(IResourceDirectory directory, string name)
	{
		Directory = directory;
		Name = name;
	}

	/// <summary>
	/// The resource directory that contains this resource.
	/// </summary>
	public IResourceDirectory Directory { get; }

	/// <summary>
	/// The name of the resource.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// Opens the resource as a stream.
	/// </summary>
	/// <returns></returns>
	public Stream Open() => Directory.Open(Name);
}

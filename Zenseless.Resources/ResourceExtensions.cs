using System.IO;

namespace Zenseless.Resources;

/// <summary>
/// Resource extension methods
/// </summary>
public static class ResourceExtensions
{
	/// <summary>
	/// Returns the resource as a string.
	/// </summary>
	/// <param name="resource">The resource directory.</param>
	/// <returns>A <c>string</c></returns>
	public static string AsString(this IResource resource)
	{
		using var stream = resource.Open();
		using var t = new StreamReader(stream);
		return t.ReadToEnd();
	}

	/// <summary>
	/// Returns the resource as a <c>byte[]</c>
	/// </summary>
	/// <param name="resource">The resource directory.</param>
	/// <returns>A <c>byte[]</c></returns>
	public static byte[] AsByteArray(this IResource resource)
	{
		using var stream = resource.Open();
		var buffer = new byte[stream.Length];
		stream.Read(buffer);
		return buffer;
	}
}
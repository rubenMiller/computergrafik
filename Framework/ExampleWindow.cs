using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Reflection;

namespace Framework;

public static class ExampleWindow
{
	public static GameWindow Create()
	{
		// window with immediate mode rendering enabled
		var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability });
		// set window to halve monitor size
		var info = Monitors.GetMonitorFromWindow(window);
		window.Size = new Vector2i(info.HorizontalResolution, info.VerticalResolution) / 2;
		window.VSync = VSyncMode.On;

		return window;
	}
}

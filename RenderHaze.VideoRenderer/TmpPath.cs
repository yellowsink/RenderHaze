using System;
using System.IO;

namespace RenderHaze.VideoRenderer
{
	public static class TmpPath
	{
		// taken from https://github.com/yellowsink/AudioSync/blob/master/AudioSync.Client.Backend/OSDefaults.cs

		public static string DefaultWindowsTempLocation
			=> Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Temp");

		public const string DefaultUnixTempLocation = "/tmp";

		// ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
		public static string DefaultTempLocation => Environment.OSVersion.Platform switch
		{
			PlatformID.Win32S       => DefaultWindowsTempLocation,
			PlatformID.Win32Windows => DefaultWindowsTempLocation,
			PlatformID.Win32NT      => DefaultWindowsTempLocation,
			PlatformID.WinCE        => DefaultWindowsTempLocation,
			PlatformID.Unix         => DefaultUnixTempLocation,
			PlatformID.MacOSX       => DefaultUnixTempLocation,
			_                       => throw new ArgumentOutOfRangeException()
		};
	}
}
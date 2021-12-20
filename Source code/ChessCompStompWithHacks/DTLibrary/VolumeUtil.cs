
namespace DTLibrary
{
	using System;

	public class VolumeUtil
	{
		public static int GetVolumeSmoothed(
			int elapsedMicrosPerFrame,
			int currentVolume,
			int desiredVolume)
		{
			int maxChangePerFrame = elapsedMicrosPerFrame / 5000;
			if (maxChangePerFrame <= 0)
				maxChangePerFrame = 1;

			if (Math.Abs(desiredVolume - currentVolume) <= maxChangePerFrame)
				return desiredVolume;
			else if (desiredVolume > currentVolume)
				return currentVolume + maxChangePerFrame;
			else
				return currentVolume - maxChangePerFrame;
		}
	}
}

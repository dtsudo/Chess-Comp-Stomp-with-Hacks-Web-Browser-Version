
namespace DTLibrary
{
	public class VersionInfo
	{
		public string Version { get; private set; }
		public string AlphanumericVersionGuid { get; private set; }

		public VersionInfo(
			string version,
			string alphanumericVersionGuid)
		{
			this.Version = version;
			this.AlphanumericVersionGuid = alphanumericVersionGuid;
		}
	}
}


namespace DTLibrary
{
	public interface IFileIO
	{
		void PersistData(int fileId, VersionInfo versionInfo, ByteList data);
		ByteList FetchData(int fileId, VersionInfo versionInfo);
	}
}

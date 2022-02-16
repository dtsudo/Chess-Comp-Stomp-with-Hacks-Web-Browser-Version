
namespace DTLibrary
{
	public interface IFileIO
	{
		void PersistData(int fileId, ByteList data);
		ByteList FetchData(int fileId);
	}
}

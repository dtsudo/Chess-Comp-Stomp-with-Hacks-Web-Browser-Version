
namespace ChessCompStompWithHacks
{
	using Bridge;
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class BridgeFileIO : IFileIO
	{
		public BridgeFileIO()
		{
			Script.Eval(@"
				window.BridgeFileIOJavascript = ((function () {
					'use strict';
					
					var persistData = function (fileName, base64String) {
						try {
							localStorage.setItem(fileName, base64String);
						} catch (error) {
							// do nothing
						}
					};
										
					var fetchData = function (fileName) {
						try {
							var value = localStorage.getItem(fileName);
							return value;
						} catch (error) {
							return null;
						}
					};
					
					var hasData = function (fileName) {
						try {
							var value = localStorage.getItem(fileName);
							return value !== null;
						} catch (error) {
							return false;
						}
					};
										
					return {
						persistData: persistData,
						fetchData: fetchData,
						hasData: hasData
					};
				})());
			");
		}
		
		private string GetFileName(int fileId, VersionInfo versionInfo)
		{
			string alphanumericVersionGuid = versionInfo.AlphanumericVersionGuid;
			return "guid" + alphanumericVersionGuid + "_file" + fileId.ToStringCultureInvariant();
		}
				
		public void PersistData(int fileId, VersionInfo versionInfo, ByteList data)
		{
			List<byte> list = new List<byte>();
			
			ByteList.Iterator iterator = data.GetIterator();
			
			while (true)
			{
				if (!iterator.HasNextByte())
					break;
				
				list.Add(iterator.TryPop());
			}
			
			byte[] array = new byte[list.Count];
			for (int i = 0; i < array.Length; i++)
				array[i] = list[i];
			
			string base64String = Convert.ToBase64String(array);
			
			Script.Call("window.BridgeFileIOJavascript.persistData", this.GetFileName(fileId: fileId, versionInfo: versionInfo), base64String);
		}
		
		public ByteList FetchData(int fileId, VersionInfo versionInfo)
		{
			string fileName = this.GetFileName(fileId: fileId, versionInfo: versionInfo);
			
			bool hasData = Script.Eval<bool>("window.BridgeFileIOJavascript.hasData('" + fileName + "')");
			
			if (!hasData)
				return null;
			
			string result = Script.Eval<string>("window.BridgeFileIOJavascript.fetchData('" + fileName + "')");
			
			if (result == null)
				return null;
			
			try
			{
				byte[] array = Convert.FromBase64String(result);
				ByteList.Builder byteList = new ByteList.Builder();
				
				for (int i = 0; i < array.Length; i++)
					byteList.Add(array[i]);
				
				return byteList.ToByteList();
			}
			catch (Exception)
			{
			}
			
			return null;
		}
	}
}

//This is a wrapper for Unrar.dll

using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

namespace CarbonComic
{
	public class Unrar : IDisposable
	{
		public enum OpenMode
		{
			List,
			Extract
		}

		private enum RarError : uint
		{
			EndOfArchive = 10u,
			InsufficientMemory,
			BadData,
			BadArchive,
			UnknownFormat,
			OpenError,
			CreateError,
			CloseError,
			ReadError,
			WriteError,
			BufferTooSmall,
			UnknownError
		}

		private enum Operation : uint
		{
			Skip,
			Test,
			Extract
		}

		private enum VolumeMessage : uint
		{
			Ask,
			Notify
		}

		[Flags]
		private enum ArchiveFlags : uint
		{
			Volume = 0x1,
			CommentPresent = 0x2,
			Lock = 0x4,
			SolidArchive = 0x8,
			NewNamingScheme = 0x10,
			AuthenticityPresent = 0x20,
			RecoveryRecordPresent = 0x40,
			EncryptedHeaders = 0x80,
			FirstVolume = 0x100
		}

		private enum CallbackMessages : uint
		{
			VolumeChange,
			ProcessData,
			NeedPassword
		}

		private struct RARHeaderData
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string ArcName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string FileName;

			public uint Flags;

			public uint PackSize;

			public uint UnpSize;

			public uint HostOS;

			public uint FileCRC;

			public uint FileTime;

			public uint UnpVer;

			public uint Method;

			public uint FileAttr;

			[MarshalAs(UnmanagedType.LPStr)]
			public string CmtBuf;

			public uint CmtBufSize;

			public uint CmtSize;

			public uint CmtState;

			public void Initialize()
			{
				CmtBuf = new string('\0', 65536);
				CmtBufSize = 65536u;
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct RARHeaderDataEx
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			public string ArcName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
			public string ArcNameW;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			public string FileName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
			public string FileNameW;

			public uint Flags;

			public uint PackSize;

			public uint PackSizeHigh;

			public uint UnpSize;

			public uint UnpSizeHigh;

			public uint HostOS;

			public uint FileCRC;

			public uint FileTime;

			public uint UnpVer;

			public uint Method;

			public uint FileAttr;

			[MarshalAs(UnmanagedType.LPStr)]
			public string CmtBuf;

			public uint CmtBufSize;

			public uint CmtSize;

			public uint CmtState;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
			public uint[] Reserved;

			public void Initialize()
			{
				CmtBuf = new string('\0', 65536);
				CmtBufSize = 65536u;
			}
		}

		public struct RAROpenArchiveData
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string ArcName;

			public uint OpenMode;

			public uint OpenResult;

			[MarshalAs(UnmanagedType.LPStr)]
			public string CmtBuf;

			public uint CmtBufSize;

			public uint CmtSize;

			public uint CmtState;

			public void Initialize()
			{
				CmtBuf = new string('\0', 65536);
				CmtBufSize = 65536u;
			}
		}

		public struct RAROpenArchiveDataEx
		{
			[MarshalAs(UnmanagedType.LPStr)]
			public string ArcName;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string ArcNameW;

			public uint OpenMode;

			public uint OpenResult;

			[MarshalAs(UnmanagedType.LPStr)]
			public string CmtBuf;

			public uint CmtBufSize;

			public uint CmtSize;

			public uint CmtState;

			public uint Flags;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public uint[] Reserved;

			public void Initialize()
			{
				CmtBuf = new string('\0', 65536);
				CmtBufSize = 65536u;
				Reserved = new uint[32];
			}
		}

		private delegate int UNRARCallback(uint msg, int UserData, IntPtr p1, int p2);

		private string archivePathName = string.Empty;

		private IntPtr archiveHandle = new IntPtr(0);

		private bool retrieveComment = true;

		private string password = string.Empty;

		private string comment = string.Empty;

		private ArchiveFlags archiveFlags;

		private RARHeaderDataEx header = default(RARHeaderDataEx);

		private string destinationPath = string.Empty;

		private RARFileInfo currentFile;

		private UNRARCallback callback;

		public string ArchivePathName
		{
			get
			{
				return archivePathName;
			}
			set
			{
				archivePathName = value;
			}
		}

		public string Comment
		{
			get
			{
				return comment;
			}
		}

		public RARFileInfo CurrentFile
		{
			get
			{
				return currentFile;
			}
		}

		public string DestinationPath
		{
			get
			{
				return destinationPath;
			}
			set
			{
				destinationPath = value;
			}
		}

		public string Password
		{
			get
			{
				return password;
			}
			set
			{
				password = value;
				if (archiveHandle != IntPtr.Zero)
				{
					RARSetPassword(archiveHandle, value);
				}
			}
		}

		public event DataAvailableHandler DataAvailable;

		public event ExtractionProgressHandler ExtractionProgress;

		public event MissingVolumeHandler MissingVolume;

		public event NewFileHandler NewFile;

		public event NewVolumeHandler NewVolume;

		public event PasswordRequiredHandler PasswordRequired;

		[DllImport("CarbonComic\\unrar.dll")]
		private static extern IntPtr RAROpenArchive(ref RAROpenArchiveData archiveData);

		[DllImport("CarbonComic\\unrar.dll")]
		private static extern IntPtr RAROpenArchiveEx(ref RAROpenArchiveDataEx archiveData);

		[DllImport("CarbonComic\\unrar.dll")]
		private static extern int RARCloseArchive(IntPtr hArcData);

		[DllImport("CarbonComic\\unrar.dll")]
		private static extern int RARReadHeader(IntPtr hArcData, ref RARHeaderData headerData);

		[DllImport("CarbonComic\\unrar.dll")]
		private static extern int RARReadHeaderEx(IntPtr hArcData, ref RARHeaderDataEx headerData);

		[DllImport("CarbonComic\\unrar.dll")]
		private static extern int RARProcessFile(IntPtr hArcData, int operation, [MarshalAs(UnmanagedType.LPStr)] string destPath, [MarshalAs(UnmanagedType.LPStr)] string destName);

		[DllImport("CarbonComic\\unrar.dll")]
		private static extern void RARSetCallback(IntPtr hArcData, UNRARCallback callback, int userData);

		[DllImport("CarbonComic\\unrar.dll")]
		private static extern void RARSetPassword(IntPtr hArcData, [MarshalAs(UnmanagedType.LPStr)] string password);

		public Unrar()
		{
			callback = RARCallback;
		}

		public Unrar(string archivePathName)
			: this()
		{
			this.archivePathName = archivePathName;
		}

		~Unrar()
		{
			if (archiveHandle != IntPtr.Zero)
			{
				RARCloseArchive(archiveHandle);
				archiveHandle = IntPtr.Zero;
			}
		}

		public void Dispose()
		{
			if (archiveHandle != IntPtr.Zero)
			{
				RARCloseArchive(archiveHandle);
				archiveHandle = IntPtr.Zero;
			}
		}

		public void Close()
		{
			if (!(archiveHandle == IntPtr.Zero))
			{
				int num = RARCloseArchive(archiveHandle);
				if (num != 0)
				{
					ProcessFileError(num);
				}
				else
				{
					archiveHandle = IntPtr.Zero;
				}
			}
		}

		public void Open()
		{
			if (ArchivePathName.Length == 0)
			{
				throw new IOException("Archive name has not been set.");
			}
			Open(ArchivePathName, OpenMode.Extract);
		}

		public void Open(OpenMode openMode)
		{
			if (ArchivePathName.Length == 0)
			{
				throw new IOException("Archive name has not been set.");
			}
			Open(ArchivePathName, openMode);
		}

		public void Open(string archivePathName, OpenMode openMode)
		{
			IntPtr zero = IntPtr.Zero;
			if (archiveHandle != IntPtr.Zero)
			{
				Close();
			}
			ArchivePathName = archivePathName;
			RAROpenArchiveDataEx archiveData = default(RAROpenArchiveDataEx);
			archiveData.Initialize();
			archiveData.ArcName = this.archivePathName + "\0";
			archiveData.ArcNameW = this.archivePathName + "\0";
			archiveData.OpenMode = (uint)openMode;
			if (retrieveComment)
			{
				archiveData.CmtBuf = new string('\0', 65536);
				archiveData.CmtBufSize = 65536u;
			}
			else
			{
				archiveData.CmtBuf = null;
				archiveData.CmtBufSize = 0u;
			}
			zero = RAROpenArchiveEx(ref archiveData);
			if (archiveData.OpenResult != 0)
			{
				switch (archiveData.OpenResult)
				{
				case 11u:
					throw new OutOfMemoryException("Insufficient memory to perform operation.");
                    break;
				case 12u:
					throw new IOException("Archive header broken");
                    break;
				case 13u:
					throw new IOException("File is not a valid archive.");
                    break;
				case 15u:
					throw new IOException("File could not be opened.");
                    break;
				}
			}
			archiveHandle = zero;
			archiveFlags = (ArchiveFlags)archiveData.Flags;
			RARSetCallback(archiveHandle, callback, GetHashCode());
			if (archiveData.CmtState == 1)
			{
				comment = archiveData.CmtBuf.ToString();
			}
			if (password.Length != 0)
			{
				RARSetPassword(archiveHandle, password);
			}
			OnNewVolume(this.archivePathName);
		}

		public bool ReadHeader()
		{
			if (!(archiveHandle == IntPtr.Zero))
			{
				header = default(RARHeaderDataEx);
				header.Initialize();
				currentFile = null;
				switch (RARReadHeaderEx(archiveHandle, ref header))
				{
				case 10:
					return false;
				case 12:
					throw new IOException("Archive data is corrupt.");
				default:
					if ((header.Flags & 1) != 0 && currentFile != null)
					{
						currentFile.ContinuedFromPrevious = true;
					}
					else
					{
						currentFile = new RARFileInfo();
						currentFile.FileName = header.FileNameW.ToString();
						if ((header.Flags & 2) != 0)
						{
							currentFile.ContinuedOnNext = true;
						}
						if (header.PackSizeHigh != 0)
						{
							currentFile.PackedSize = header.PackSizeHigh * 4294967296L + header.PackSize;
						}
						else
						{
							currentFile.PackedSize = header.PackSize;
						}
						if (header.UnpSizeHigh != 0)
						{
							currentFile.UnpackedSize = header.UnpSizeHigh * 4294967296L + header.UnpSize;
						}
						else
						{
							currentFile.UnpackedSize = header.UnpSize;
						}
						currentFile.HostOS = (int)header.HostOS;
						currentFile.FileCRC = header.FileCRC;
						currentFile.FileTime = FromMSDOSTime(header.FileTime);
						currentFile.VersionToUnpack = (int)header.UnpVer;
						currentFile.Method = (int)header.Method;
						currentFile.FileAttributes = (int)header.FileAttr;
						currentFile.BytesExtracted = 0L;
						if ((header.Flags & 0xE0) == 224)
						{
							currentFile.IsDirectory = true;
						}
						OnNewFile();
					}
					return true;
				}
			}
			throw new IOException("Archive is not open.");
		}

		public string[] ListFiles()
		{
			ArrayList arrayList = new ArrayList();
			while (ReadHeader())
			{
				if (!currentFile.IsDirectory)
				{
					arrayList.Add(currentFile.FileName);
				}
				Skip();
			}
			string[] array = new string[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		public void Skip()
		{
			int num = RARProcessFile(archiveHandle, 0, string.Empty, string.Empty);
			if (num != 0)
			{
				ProcessFileError(num);
			}
		}

		public void Test()
		{
			int num = RARProcessFile(archiveHandle, 1, string.Empty, string.Empty);
			if (num != 0)
			{
				ProcessFileError(num);
			}
		}

		public void Extract()
		{
			Extract(destinationPath, string.Empty);
		}

		public void Extract(string destinationName)
		{
			Extract(string.Empty, destinationName);
		}

		public void ExtractToDirectory(string destinationPath)
		{
			Extract(destinationPath, string.Empty);
		}

		private void Extract(string destinationPath, string destinationName)
		{
			int num = RARProcessFile(archiveHandle, 2, destinationPath, destinationName);
			if (num != 0)
			{
				ProcessFileError(num);
			}
		}

		private DateTime FromMSDOSTime(uint dosTime)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			ushort num7 = (ushort)((uint)((int)dosTime & -65536) >> 16);
			ushort num8 = (ushort)(dosTime & 0xFFFF);
			num3 = ((num7 & 0xFE00) >> 9) + 1980;
			num2 = (num7 & 0x1E0) >> 5;
			num = (num7 & 0x1F);
			num5 = (num8 & 0xF800) >> 11;
			num6 = (num8 & 0x7E0) >> 5;
			num4 = (num8 & 0x1F) << 1;
			return new DateTime(num3, num2, num, num5, num6, num4);
		}

		private void ProcessFileError(int result)
		{
			switch (result)
			{
			case 14:
				throw new OutOfMemoryException("Unknown archive format.");
			case 12:
				throw new IOException("File CRC Error");
			case 13:
				throw new IOException("File is not a valid archive.");
			case 15:
				throw new IOException("File could not be opened.");
			case 16:
				throw new IOException("File could not be created.");
			case 17:
				throw new IOException("File close error.");
			case 18:
				throw new IOException("File read error.");
			case 19:
				throw new IOException("File write error.");
			}
		}

		private int RARCallback(uint msg, int UserData, IntPtr p1, int p2)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			int result = -1;
			switch (msg)
			{
			case 0u:
				empty = Marshal.PtrToStringAnsi(p1);
				switch (p2)
				{
				case 1:
					result = OnNewVolume(empty);
					break;
				case 0:
					empty2 = OnMissingVolume(empty);
					if (empty2.Length == 0)
					{
						result = -1;
					}
					else
					{
						if (empty2 != empty)
						{
							for (int i = 0; i < empty2.Length; i++)
							{
								Marshal.WriteByte(p1, i, (byte)empty2[i]);
							}
							Marshal.WriteByte(p1, empty2.Length, 0);
						}
						result = 1;
					}
					break;
				}
				break;
			case 1u:
				result = OnDataAvailable(p1, p2);
				break;
			case 2u:
				result = OnPasswordRequired(p1, p2);
				break;
			}
			return result;
		}

		protected virtual void OnNewFile()
		{
			if (this.NewFile != null)
			{
				NewFileEventArgs e = new NewFileEventArgs(currentFile);
				this.NewFile(this, e);
			}
		}

		protected virtual int OnPasswordRequired(IntPtr p1, int p2)
		{
			int result = -1;
			if (this.PasswordRequired == null)
			{
				throw new IOException("Password is required for extraction.");
			}
			PasswordRequiredEventArgs passwordRequiredEventArgs = new PasswordRequiredEventArgs();
			this.PasswordRequired(this, passwordRequiredEventArgs);
			if (passwordRequiredEventArgs.ContinueOperation && passwordRequiredEventArgs.Password.Length > 0)
			{
				for (int i = 0; i < passwordRequiredEventArgs.Password.Length && i < p2; i++)
				{
					Marshal.WriteByte(p1, i, (byte)passwordRequiredEventArgs.Password[i]);
				}
				Marshal.WriteByte(p1, passwordRequiredEventArgs.Password.Length, 0);
				result = 1;
			}
			return result;
		}

		protected virtual int OnDataAvailable(IntPtr p1, int p2)
		{
			int result = 1;
			if (currentFile != null)
			{
				currentFile.BytesExtracted += p2;
			}
			if (this.DataAvailable != null)
			{
				byte[] array = new byte[p2];
				Marshal.Copy(p1, array, 0, p2);
				DataAvailableEventArgs dataAvailableEventArgs = new DataAvailableEventArgs(array);
				this.DataAvailable(this, dataAvailableEventArgs);
				if (!dataAvailableEventArgs.ContinueOperation)
				{
					result = -1;
				}
			}
			if (this.ExtractionProgress != null && currentFile != null)
			{
				ExtractionProgressEventArgs extractionProgressEventArgs = new ExtractionProgressEventArgs();
				extractionProgressEventArgs.FileName = currentFile.FileName;
				extractionProgressEventArgs.FileSize = currentFile.UnpackedSize;
				extractionProgressEventArgs.BytesExtracted = currentFile.BytesExtracted;
				extractionProgressEventArgs.PercentComplete = currentFile.PercentComplete;
				this.ExtractionProgress(this, extractionProgressEventArgs);
				if (!extractionProgressEventArgs.ContinueOperation)
				{
					result = -1;
				}
			}
			return result;
		}

		protected virtual int OnNewVolume(string volume)
		{
			int result = 1;
			if (this.NewVolume != null)
			{
				NewVolumeEventArgs newVolumeEventArgs = new NewVolumeEventArgs(volume);
				this.NewVolume(this, newVolumeEventArgs);
				if (!newVolumeEventArgs.ContinueOperation)
				{
					result = -1;
				}
			}
			return result;
		}

		protected virtual string OnMissingVolume(string volume)
		{
			string result = string.Empty;
			if (this.MissingVolume != null)
			{
				MissingVolumeEventArgs missingVolumeEventArgs = new MissingVolumeEventArgs(volume);
				this.MissingVolume(this, missingVolumeEventArgs);
				if (missingVolumeEventArgs.ContinueOperation)
				{
					result = missingVolumeEventArgs.VolumeName;
				}
			}
			return result;
		}
	}
}

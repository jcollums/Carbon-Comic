using CarbonComic.Properties;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CarbonComic
{
	/// <summary>
	/// This class contains global variables, structures, and functions
	/// </summary>
	public sealed class CC
	{
		//For storing auto-tag settings
		public struct AutoTagSettings
		{
			public string Pattern;
			public int[] Matches;  //Keeps track of which matches relate to which fields
			public bool usePublisher;
			public string Publisher;
			public bool useGroup;
			public string Group;
			public bool useSeries;
			public string Series;
			public bool useVolume;
			public int Volume;
			public bool usePlot;
			public string Plot;
		}

		//Types of issues
		public enum IssueType
		{
			Normal,
			Annual,
			Special
		}

		//Statuses that would be reflected by a special icon in the issue list
		public enum IssueStatus
		{
			Normal = -1,
			Missing,
			Marked
		}

		//Types of Series
		public enum SeriesType
		{
			All = -1,
			Normal,
			Limited,
			Oneshot
		}

		public static ArrayList Issues = new ArrayList();       //stores loaded issues
		public static ArrayList Groups = new ArrayList();       //stores loaded groups
		public static ArrayList Series = new ArrayList();       //stores loaded series
		public static ArrayList Publishers = new ArrayList();   //stores loaded publishers
		public static ArrayList Readlists = new ArrayList();    //stores loaded readlists
		public static ArrayList ImportFiles = new ArrayList();  //stores files to be imported
		public static AutoTagSettings AutoTag;

		public static SQL SQL = new SQL();  //Used to interact with the database


		/// <summary>
		/// This is mostly used to replace the filepath of an issue whenever files are reorganized
		/// </summary>
		/// <param name="field">Field name</param>
		/// <param name="find">What to replace</param>
		/// <param name="replace">What to replace with</param>
		/// <returns></returns>
		public static string SQLReplaceLeft(string field, string find, string replace)
		{
			find += "\\";
			replace += "\\";
			return "IIF(MID(" + field + ",1," + find.Length + ")='" + find + "','" + replace + "' + MID(" + field + "," + (find.Length + 1) + "), " + field + ")";
		}

		/// <summary>
		/// This creates a string array out of an ArrayList so that it can be used in queries
		/// </summary>
		/// <param name="list">The ArrayList to stringify</param>
		/// <returns></returns>
		public static string[] StringList(ArrayList list)
		{
			string[] array = new string[list.Count];
			list.CopyTo(array);
			return array;
		}

		public static ComicPublisher FindPublisherByID(int ID, ArrayList list)
		{
			foreach (ComicPublisher item in list)
			{
				if (item.ID == ID)
				{
					return item;
				}
			}
			return null;
		}

		public static ComicGroup FindGroupByID(int ID, ArrayList list)
		{
			foreach (ComicGroup item in list)
			{
				if (item.ID == ID)
				{
					return item;
				}
			}
			return null;
		}

		public static ComicSeries FindSeriesByID(int ID, ArrayList list)
		{
			foreach (ComicSeries item in list)
			{
				if (item.ID == ID)
				{
					return item;
				}
			}
			return null;
		}

		//This opens a document with its default program
		public static bool ExecuteFile(string FilePath)
		{
			Process process = new Process();
			process.StartInfo.FileName = FilePath;
			process.StartInfo.UseShellExecute = true;
			process.StartInfo.RedirectStandardOutput = false;
			try
			{
				process.Start();
				return true;
			}
			catch
			{
				return false;
			}
		}

		//Basic check if file is an image or not
		public static bool IsImageExt(string FileName)
		{
			string text = null;
			text = Path.GetExtension(FileName).ToLower();
			if ((text == ".jpg") | (text == ".png") | (text == ".gif") | (text == ".jpeg"))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Loads or retrieves the cover thumbnail of an issue
		/// </summary>
		/// <param name="ID">ID of the issue</param>
		/// <param name="Window">Window which contains the ImageList cache</param>
		/// <returns></returns>
		public static Image GetIssueCover(int ID, MainForm Window)
		{
			string text = null;
			Image image = null;
			ImageList issueCovers = Window.IssueCovers;

			//If cover of the issue isn't loaded yet, get its filepath and add it to the ImageList
			if (!issueCovers.Images.ContainsKey(ID.ToString()))
			{
				text = Path.Combine(Application.StartupPath, Settings.Default.CoverDir) + "\\" + ID.ToString() + ".jpg";
				image = (File.Exists(text) ? Image.FromFile(text) : Resources.DefaultCover);
				issueCovers.Images.Add(ID.ToString(), image);
			}
			//Otherwise, return image data from ImageList
			else
			{
				image = issueCovers.Images[ID.ToString()];
			}
			return image;
		}


		//Overloaded function, assume Window is MainForm
		public static Image GetIssueCover(int ID)
		{
			return GetIssueCover(ID, MainForm.Root);
		}

		/// <summary>
		/// Extract a file from a zip archive
		/// </summary>
		/// <param name="ArcFile">Archive to unzip</param>
		/// <param name="ArcEntry">File within the archive to unzip</param>
		/// <param name="targetpath">Where to unzip the file</param>
		public static void unzip(string ArcFile, string ArcEntry, string targetpath)
		{
			ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(ArcFile));
			ZipEntry nextEntry = zipInputStream.GetNextEntry();
			while (true)
			{
				FileStream fileStream = null;
				byte[] array = new byte[2049];
				int num = -1;
				if (nextEntry.Name == ArcEntry)
				{
					fileStream = new FileStream(targetpath, FileMode.Create);
				}
				if (!nextEntry.IsDirectory)
				{
					while (true)
					{
						num = zipInputStream.Read(array, 0, array.Length);
						if (num <= 0)
						{
							break;
						}
						if (nextEntry.Name == ArcEntry)
						{
							fileStream.Write(array, 0, num);
						}
					}
				}
				if (nextEntry.Name == ArcEntry)
				{
					fileStream.Flush();
				}
				if (nextEntry.Name == ArcEntry)
				{
					fileStream.Close();
				}
				if (nextEntry.Name == ArcEntry)
				{
					break;
				}
				nextEntry = zipInputStream.GetNextEntry();
			}
			zipInputStream.Close();
		}

		/// <summary>
		/// Invoke the generic inputbox form
		/// </summary>
		/// <param name="Prompt">Explanation of what to input</param>
		/// <param name="Title">Title of window</param>
		/// <param name="Default">Default entry, if any</param>
		/// <param name="Validation">Handler for validating the input received</param>
		/// <returns></returns>
		public static string InputBox(string Prompt, string Title, string Default, EventHandler Validation)
		{
			InputBox inputBox = new InputBox();
			inputBox.Controls["lblPrompt"].Text = Prompt;
			inputBox.Controls["txtInput"].Text = Default;
			inputBox.Text = Title;
			if (Validation != null)
			{
				inputBox.Controls["cmdOK"].Click += Validation.Invoke;
			}
			inputBox.ShowDialog(MainForm.Root);
			if (inputBox.DialogResult == DialogResult.Cancel)
			{
				return "";
			}
			return inputBox.Controls["txtInput"].Text;
		}

		//Overloaded function, no event handler for validation
		public static string InputBox(string Prompt, string Title, string Default)
		{
			return InputBox(Prompt, Title, Default, null);
		}

		//Convert an image to JPEG
		public static string ConvertJPG(string filename)
		{
			using (Image image = Image.FromFile(filename))
			{
				string text = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + ".jpg");
				image.Save(text, ImageFormat.Jpeg);
				image.Dispose();
				return text;
			}
		}

		//Get the MD5 hash for a file, used for duplicate detection
		public static string md5file(FileStream file)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			mD5CryptoServiceProvider.ComputeHash(file);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] hash = mD5CryptoServiceProvider.Hash;
			foreach (byte b in hash)
			{
				stringBuilder.Append(string.Format("{0:X1}", b));
			}
			return stringBuilder.ToString();
		}

		//Determine if the file is a RAR or ZIP, or something else
		public static string GetArchiveType(string filename)
		{
			//Try to open it as a RAR
			try
			{
				Unrar unrar = new Unrar();
				unrar.Open(filename, Unrar.OpenMode.List);
				unrar.Close();
				return ".cbr";
			}
			catch
			{
				//Try to open it as a ZIP
				try
				{
					ZipFile zipFile = new ZipFile(filename);
					zipFile.Close();
					return ".cbz";
				}
				catch
				{
					//Failed to open as RAR or ZIP, must be something else
					FileInfo fileInfo = new FileInfo(filename);
					string extension = fileInfo.Extension;
					if (extension != ".cbr" && extension != ".cbz")
					{
						return extension;
					}
					//If RAR and ZIP failed to open it, but its extention is CBR or CBZ, then we know that's a lie
					//And return nothing.
				}
			}
			return "";
		}

		/// <summary>
		/// Convert an image to a proportional thumbnail
		/// </summary>
		/// <param name="src">Source image</param>
		/// <param name="dst">Destination of thumbnail</param>
		public static void CreateThumbnail(string src, string dst)
		{
			//Open image
			using (Image image = Image.FromFile(src))
			{
				//Create a blank canvas that matches with user thumbnail settings
				Image image2 = new Bitmap(Settings.Default.ThumbWidth, Settings.Default.ThumbHeight, image.PixelFormat);
				Graphics graphics = Graphics.FromImage(image2);

				//Sets up parameters for a relatively high-quality thumbnail
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				EncoderParameter encoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Settings.Default.ThumbQuality);
				EncoderParameters encoderParameters = new EncoderParameters(1);
				encoderParameters.Param[0] = encoderParameter;

				//Find a JPEG image encoder
				ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
				ImageCodecInfo encoder = null;
				for (int i = 0; i < imageEncoders.Length; i++)
				{
					if (imageEncoders[i].MimeType == "image/jpeg")
					{
						encoder = imageEncoders[i];
					}
				}

				//Find out how much we need to cut off from the sides of the original image to make this proportional
				int num = image.Height * Settings.Default.ThumbWidth / Settings.Default.ThumbHeight;
				int num2 = image.Width - num;
				if (num2 < 0)
				{
					num2 = 0;
				}

				//I honeslty don't remember why this is necessary
				//But I suppose it's comparing a portion of srcRect to the image, then scaling down using the dimensions of destRect
				Rectangle destRect = new Rectangle(0, 0, Settings.Default.ThumbWidth, Settings.Default.ThumbHeight);
				Rectangle srcRect = new Rectangle(num2, 0, num, image.Height);

				//Paint canvas, save and close
				graphics.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
				image2.Save(dst, encoder, encoderParameters);
				image.Dispose();
			}
		}

		//Converts bytes to a string for displaying the size of the library
		public static string ByteToString(double Bytes)
		{
			string text = null;
			Bytes = Bytes / 1024.0 / 1024.0 / 1024.0;
			if (Convert.ToInt16(Bytes) < 1)
			{
				Bytes *= 1024.0;
				text = "MB";
			}
			else
			{
				text = "GB";
			}
			return Bytes.ToString("N2") + " " + text;
		}

		//A function to move a file, making sure to create any directories implied by the dst path
		public static void Rename(string src, string dst)
		{
			if ((src != dst) & File.Exists(src))
			{
				if (!Directory.Exists(Path.GetDirectoryName(dst)))
				{
					int num = 0;
					string text = "";
					string[] array = Path.GetDirectoryName(dst).Split(char.Parse("\\"));
					text = array[0] + "\\";
					for (num = 1; num <= array.Length - 1; num++)
					{
						text = text + array[num] + "\\";
						if (!Directory.Exists(text))
						{
							Directory.CreateDirectory(text);
						}
					}
				}
				try
				{
					if (File.GetAttributes(src) != FileAttributes.Directory)
					{
						File.Move(src, dst);
					}
					else
					{
						Directory.Move(src, dst);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		//Function to make sure that a file name won't contain invalid characters
		public static string URLize(string name)
		{
			name = name.Replace(":", " -");
			name = Regex.Replace(name, "[/\\\\*?\"<>|]", "_");
			return name;
		}
	}
}

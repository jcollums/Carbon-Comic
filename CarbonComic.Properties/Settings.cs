using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CarbonComic.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
	[CompilerGenerated]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default
		{
			get
			{
				return defaultInstance;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		[UserScopedSetting]
		public string LibraryDir
		{
			get
			{
				return (string)this["LibraryDir"];
			}
			set
			{
				this["LibraryDir"] = value;
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("Unsorted")]
		public string UnknownSeries
		{
			get
			{
				return (string)this["UnknownSeries"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("Unsorted")]
		public string UnknownGroup
		{
			get
			{
				return (string)this["UnknownGroup"];
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string RAREditor
		{
			get
			{
				return (string)this["RAREditor"];
			}
			set
			{
				this["RAREditor"] = value;
			}
		}

		[DefaultSettingValue("")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public string ZIPEditor
		{
			get
			{
				return (string)this["ZIPEditor"];
			}
			set
			{
				this["ZIPEditor"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string PDFEditor
		{
			get
			{
				return (string)this["PDFEditor"];
			}
			set
			{
				this["PDFEditor"] = value;
			}
		}

		[DefaultSettingValue("175")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public int ThumbHeight
		{
			get
			{
				return (int)this["ThumbHeight"];
			}
			set
			{
				this["ThumbHeight"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("115")]
		public int ThumbWidth
		{
			get
			{
				return (int)this["ThumbWidth"];
			}
			set
			{
				this["ThumbWidth"] = value;
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("Unsorted")]
		public string UnknownPublisher
		{
			get
			{
				return (string)this["UnknownPublisher"];
			}
		}

		[DefaultSettingValue("100")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public int ThumbQuality
		{
			get
			{
				return (int)this["ThumbQuality"];
			}
			set
			{
				this["ThumbQuality"] = value;
			}
		}

		[DefaultSettingValue("125")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public int SourceSplitWidth
		{
			get
			{
				return (int)this["SourceSplitWidth"];
			}
			set
			{
				this["SourceSplitWidth"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("140")]
		[UserScopedSetting]
		public int ContentSplitHeight
		{
			get
			{
				return (int)this["ContentSplitHeight"];
			}
			set
			{
				this["ContentSplitHeight"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		public bool ThumbGen
		{
			get
			{
				return (bool)this["ThumbGen"];
			}
			set
			{
				this["ThumbGen"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("1")]
		public int ViewMode
		{
			get
			{
				return (int)this["ViewMode"];
			}
			set
			{
				this["ViewMode"] = value;
			}
		}

		[DefaultSettingValue("0.65")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public decimal ThumbRatio
		{
			get
			{
				return (decimal)this["ThumbRatio"];
			}
			set
			{
				this["ThumbRatio"] = value;
			}
		}

		[DefaultSettingValue("0")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public int OrganizeMethod
		{
			get
			{
				return (int)this["OrganizeMethod"];
			}
			set
			{
				this["OrganizeMethod"] = value;
			}
		}

		[DefaultSettingValue("Cover Art")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string CoverDir
		{
			get
			{
				return (string)this["CoverDir"];
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("CarbonLib.mdb")]
		[ApplicationScopedSetting]
		public string DatabaseFile
		{
			get
			{
				return (string)this["DatabaseFile"];
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string> - </string>\r\n</ArrayOfString>")]
		public StringCollection ReplaceKeys
		{
			get
			{
				return (StringCollection)this["ReplaceKeys"];
			}
			set
			{
				this["ReplaceKeys"] = value;
			}
		}

		[DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string>: </string>\r\n</ArrayOfString>")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public StringCollection ReplaceVals
		{
			get
			{
				return (StringCollection)this["ReplaceVals"];
			}
			set
			{
				this["ReplaceVals"] = value;
			}
		}

		[ApplicationScopedSetting]
		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		public bool AutoSeriesType
		{
			get
			{
				return (bool)this["AutoSeriesType"];
			}
		}

		[DefaultSettingValue("12")]
		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		public int AutoSeriesMaxLimited
		{
			get
			{
				return (int)this["AutoSeriesMaxLimited"];
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		public bool FindDuplicates
		{
			get
			{
				return (bool)this["FindDuplicates"];
			}
			set
			{
				this["FindDuplicates"] = value;
			}
		}

		private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
		{
		}

		private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
		{
		}
	}
}

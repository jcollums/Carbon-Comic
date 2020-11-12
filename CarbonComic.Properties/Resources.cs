using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace CarbonComic.Properties
{
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	[DebuggerNonUserCode]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(resourceMan, null))
				{
					ResourceManager resourceManager = resourceMan = new ResourceManager("CarbonComic.Properties.Resources", typeof(Resources).Assembly);
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static Bitmap DefaultCover
		{
			get
			{
				object @object = ResourceManager.GetObject("DefaultCover", resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap StatusStop
		{
			get
			{
				object @object = ResourceManager.GetObject("StatusStop", resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap StatusStopPressed
		{
			get
			{
				object @object = ResourceManager.GetObject("StatusStopPressed", resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap StatusSwitch
		{
			get
			{
				object @object = ResourceManager.GetObject("StatusSwitch", resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap StatusSwitchPressed
		{
			get
			{
				object @object = ResourceManager.GetObject("StatusSwitchPressed", resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal Resources()
		{
		}
	}
}

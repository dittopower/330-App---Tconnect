using System;
using Xamarin.Forms;

namespace Tconnect
{
	// Used in TabbedPageDemoPage & CarouselPageDemoPage.
	public class MyMenuItem
	{
		public MyMenuItem(string name, string page, string image)
		{
			this.Name = name;
			this.Page = page;
			this.Image = image;
		}

		public string Name { private set; get; }

		public string Page { private set; get; }

		public string Image { private set; get; }

		public override string ToString()
		{
			return Name;
		}
	}

}
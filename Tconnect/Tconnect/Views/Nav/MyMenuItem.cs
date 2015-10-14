using System;
using Xamarin.Forms;

namespace Tconnect
{
	// Used in TabbedPageDemoPage & CarouselPageDemoPage.
	class MyMenuItem
	{
		public MyMenuItem(string name, Func<ContentPage> page)
		{
			this.Name = name;
			this.Page = page;
		}

		public string Name { private set; get; }

		public Func<ContentPage> Page { private set; get; }

		public override string ToString()
		{
			return Name;
		}
	}

}
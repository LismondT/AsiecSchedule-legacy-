using ApekSchedule.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApekSchedule.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StyledCollectionView : CollectionView
	{
		ResourceDictionary _resources;

		public StyledCollectionView ()
		{
			InitializeComponent ();
		}

		public void SetTheme(ResourceDictionary theme)
		{
			_resources = theme;
        }

		protected override void OnChildAdded(Element child)
		{
			base.OnChildAdded(child);

			if (_resources == null) return;

			Resources.MergedDictionaries.Clear();
			Resources.MergedDictionaries.Add(_resources);
		}
	}
}		
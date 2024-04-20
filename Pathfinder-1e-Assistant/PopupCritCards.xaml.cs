using CommunityToolkit.Maui.Views;

namespace Pathfinder_1e_Assistant;
 
public partial class PopupCrits : Popup
{
	public PopupCrits(string urlCrit)
	{
		InitializeComponent();
		
		CritCardImage.Source= urlCrit;

	}
}
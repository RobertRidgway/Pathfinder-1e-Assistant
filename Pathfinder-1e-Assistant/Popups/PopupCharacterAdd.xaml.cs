using CommunityToolkit.Maui.Views;
using Pathfinder_1e_Assistant.Databases;
using System.Diagnostics;

namespace Pathfinder_1e_Assistant;
 
public partial class PopupCharacterAdd : Popup
{
   
    public PopupCharacterAdd()
	{
		InitializeComponent();

	}

	async void OnAddButtonClicked(object sender, EventArgs args)
	{
		
		statusMessage.Text = "";
		
		App.CharRepo.AddNewCharacter(newPerson.Text);
		
		statusMessage.Text = App.CharRepo.StatusMessage + "haha";

		var cts = new CancellationTokenSource(2);
		await CloseAsync(true,cts.Token);
	}

    async void OnCancelButtonClicked(object sender, EventArgs args)
    {
        var cts = new CancellationTokenSource(2);
        await CloseAsync(false,cts.Token);
    }
}
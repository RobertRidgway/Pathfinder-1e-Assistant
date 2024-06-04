using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using Pathfinder_1e_Assistant.Databases;
using Pathfinder_1e_Assistant.Lib;
using System.Diagnostics;

namespace Pathfinder_1e_Assistant;
 
public partial class PopupCharacterUpdate : Popup
{
	readonly Character character;
    public PopupCharacterUpdate(int charId)
	{
		InitializeComponent();
		character = App.CharRepo.GetCharacter(charId);
		newPerson.Text = character.CharacterName;

	}

	async void OnUpdateButtonClicked(object sender, EventArgs args)
	{
		
		statusMessage.Text = "";
		App.CharRepo.UpdateCharacter(new Character { Id = character.Id, CharacterName = newPerson.Text });

		statusMessage.Text = App.CharRepo.StatusMessage;
		Debug.WriteLine(statusMessage.Text);
        Thread.Sleep(1000);
		// Close popup if character updated.
		if (statusMessage.Text.StartsWith("Character updated:"))
		{
			var cts = new CancellationTokenSource(2);
			await CloseAsync(true, cts.Token);
		}
	}

	async void OnDeleteButtonClicked(object sender, EventArgs args)
	{
		bool answer = await Shell.Current.DisplayAlert("Confirmation","Are you sure you want to delete this character?", "Yes", "No");

		bool updateToken = false;
		if (answer) 
		{
            App.CharRepo.DeleteCharacter(character);


			// Delete all macros
			MacroRepo macroRepo = new(DatabaseConstants.MacrosRepoPath);
			List<Macro> macroList = macroRepo.GetCharMacros(character.CharacterName);
			foreach (Macro macro in macroList) 
			{
				Debug.WriteLine($"{macro.CharName} {macro.Name}");
				macroRepo.DeleteMacro(macro); }
			updateToken= true;

        }
        var cts = new CancellationTokenSource(2);
        await CloseAsync(updateToken, cts.Token);
    }

    async void OnCancelButtonClicked(object sender, EventArgs args)
    {
        var cts = new CancellationTokenSource(2);
        await CloseAsync(false,cts.Token);
    }
}
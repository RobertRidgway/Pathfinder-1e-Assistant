using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using Pathfinder_1e_Assistant.Databases;
using Pathfinder_1e_Assistant.Lib;

using Pathfinder_1e_Assistant.Popups;
using System.Security.Cryptography;
//using Javax.Sql;

namespace Pathfinder_1e_Assistant
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            UpdateCharacterButtons();
        }

        private void UpdateCharacterButtons() 
        {
            List<Character> characters = App.CharRepo.GetAllCharacters();

            CharacterGrid.Children.Clear();
            CharacterGrid.RowDefinitions.Clear();

            int rowPos = 0;
            foreach (Character character in characters) 
            {
                string charName = character.CharacterName;
                string charId = character.Id.ToString();
                CharacterGrid.AddRowDefinition(new RowDefinition(GridLength.Auto));
                Button charBtn = new() { Text = charName, StyleId= charId  };
                Button editBtn = new() { Text = StyleConstants.settingStr, StyleId = charId };

                charBtn.Clicked += (s, e) => { OnCharacterClicked(s, e); };
                editBtn.Clicked += (s, e) => { OnEditCharacterClicked(s, e); };
                Grid.SetRow(charBtn, rowPos);
                Grid.SetRow(editBtn, rowPos);

                Grid.SetColumn(charBtn, 0);
                Grid.SetColumn(editBtn, 1);

                CharacterGrid.Children.Add(charBtn);
                CharacterGrid.Children.Add(editBtn);

                rowPos++;
            }

        }

        private async void OnAddNewCharacterClicked(object sender, EventArgs e)
        {
            
            var result = await this.ShowPopupAsync(new PopupCharacterAdd(), CancellationToken.None);
            // Run code to update list of characters after character added. 
            if (result is bool boolresult)
            { 
                if (boolresult)
                {
                    UpdateCharacterButtons();
                }
            }
            
        }

        private async void OnEditCharacterClicked(object sender, EventArgs e) 
        { 
            var editBtn = (Button)sender;
            int charId = Convert.ToInt32(editBtn.StyleId);
            var result = await this.ShowPopupAsync(new PopupCharacterUpdate(charId),CancellationToken.None);
            // Run code to update list of characters after character updated or deleted. 
            if (result is bool boolresult)
            {
                if (boolresult)
                {
                    UpdateCharacterButtons();
                }
            }
        }

        


        // Load character, add global accessability and go to character landing page
        private async void OnCharacterClicked(object sender, EventArgs e)
        {
            var charBtn = (Button)sender;
            int charId = Convert.ToInt32(charBtn.StyleId);
            Character character = App.CharRepo.GetCharacter(charId);

            CharacterGlobals.LoadedCharacter = character;
            await Navigation.PushAsync(new CharacterPage());

        }

    }

}

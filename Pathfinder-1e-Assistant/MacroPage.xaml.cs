using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Internals;
using Microsoft.VisualBasic;
using System.Diagnostics;
using Pathfinder_1e_Assistant.Popups;
using System.ComponentModel;
using Pathfinder_1e_Assistant.Databases;
using Pathfinder_1e_Assistant.Lib;

namespace Pathfinder_1e_Assistant
{
    public partial class MacroPage : ContentPage
    {

        public MacroRepo macroRepo = new(DatabaseConstants.MacrosRepoPath);

        public string StatusMessage { get; set; } = string.Empty;

        public MacroPage()
        {
            InitializeComponent();
            Title = $"{CharacterGlobals.LoadedCharacter.CharacterName}'s Macros";
            CritCardsToggle.CheckedChanged += (s, e) => 
            {
                CritHitBtn.IsVisible  = CritCardsToggle.IsChecked;
                CritMissBtn.IsVisible = CritCardsToggle.IsChecked;
            };
            UpdateMacroList();    
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateMacroList();
        }

        private void UpdateMacroList() 
        {

            List<Macro> macroList = macroRepo.GetCharMacros(CharacterGlobals.LoadedCharacter.CharacterName);

            int rowPos = 0;
            int colPos = 0;
            
            MacroGrid.Children.Clear();

            MacroGrid.RowDefinitions.Clear();
            MacroGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));

            foreach (Macro item in macroList) 
            {
                Button btn = new()
                {
                    Text = item.Name,
                    StyleId = $"{item.Id}",
                };
                //btn.Clicked += (s, e) => OnMacroButtonClicked(s, e);
                btn.Clicked += OnMacroButtonClicked;
                Grid.SetColumn(btn, colPos);
                Grid.SetRow(btn, rowPos);

                Button editBtn = new() { Text = StyleConstants.settingStr, StyleId=$"{item.Id}"};
                editBtn.Clicked += OnEditMacroButtonClicked;
                Grid.SetColumn(editBtn, colPos+1);
                Grid.SetRow(editBtn, rowPos);
                //Debug.WriteLine(FindByName(item) + " button?");
                MacroGrid.Children.Add(btn);
                MacroGrid.Children.Add(editBtn);
                
                // Either 0 or 3
                colPos +=3;
                if (colPos > 3) 
                {
                    colPos = 0; 
                    rowPos++;
                    MacroGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
                }  
            }
        }

        private async void OnMacroButtonClicked(object sender, EventArgs e) 
        {
            var obj = (Button)sender;
            int macroId = Convert.ToInt32(obj.StyleId);
            Macro macro = macroRepo.GetMacro(macroId);
            try
            {
                (string[] macroSnippets, int[] critFlags) = DiceParse.MacroParse(macro.MacroText);
                this.ShowPopup(new PopupMacro(macroSnippets, critFlags));
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
                await DisplayAlert("Error", StatusMessage,"Ok");
            }

            

            
        }

        private async void OnAddMacroButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NewMacroPage(macroRepo));
            UpdateMacroList();
        }

        private async void OnEditMacroButtonClicked(object sender, EventArgs e)
        {
            var obj = (Button)sender;
            int macroId = Convert.ToInt32(obj.StyleId);
            Macro macro = macroRepo.GetMacro(macroId);
            
            await Navigation.PushModalAsync(new MacroDetailsPage(macro, macroRepo));
            UpdateMacroList();
        }


        private void OnCritCardClicked(object sender, EventArgs e)
        {
            string cardURL;
            int cardNum = Util.GetRandomInt(1, 52 + 1);
            var obj = (Button)sender;
            bool cardType = (obj.Text == "Critical Hit!");
            if (cardType) { cardURL = $"pf_hit_{cardNum}_cc.jpg"; }
            else { cardURL = $"pf_fumble_{cardNum}_cc.jpg"; }

            this.ShowPopup(new PopupCrits(cardURL));
        }

    }

}

using CommunityToolkit.Maui.Views;

namespace Pathfinder_1e_Assistant
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        Random rnd = new Random();
        public MainPage()
        {
            InitializeComponent();

            

            CritCardsToggle.CheckedChanged += (s, e) =>
            {
                CritHitBtn.IsVisible  = CritCardsToggle.IsChecked;
                CritMissBtn.IsVisible = CritCardsToggle.IsChecked;
            };
                
        }

        private void OnCritHitClicked(object sender, EventArgs e)  { OnCritCardClicked(sender, e, true); }

        private void OnCritMissClicked(object sender, EventArgs e) { OnCritCardClicked(sender, e, false); }

        private void OnCritCardClicked(object sender, EventArgs e, bool cardType)
        { 
            int cardNum = rnd.Next(1, 52 + 1);
            string cardURL;
            if (cardType) { cardURL = $"pf_hit_{cardNum}_cc.jpg)"; }
            else { cardURL = $"pf_fumble_{cardNum}_cc.jpg"; }

            this.ShowPopup(new PopupCrits(cardURL));
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}

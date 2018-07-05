using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
	public partial class MainPage : ContentPage
	{
        public MainPage()
        {
            InitializeComponent();
        }

        void MoveOn_Button_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Alert", "Move on clicked!", "Ok");
        }
    }
}

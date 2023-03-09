using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WalimuV2.DataTemplates
{
 
    [ContentProperty("Child")]
    public partial class LabelWithIcon : ContentView
    {
        public LabelWithIcon()
        {
            InitializeComponent();
        }

        public string ImageUrl 
        {
            get => imgCustomImage.Source.ToString();
            set => imgCustomImage.Source = value;
        }

        public string CustomText 
        {
            get => lblCustomLabel.Text;
            set => lblCustomLabel.Text = value;
        }

        public Color TextColor
        {
            get => lblCustomLabel.TextColor;
            set => lblCustomLabel.TextColor = value;
        }

        public double FontSize
        {
            get => lblCustomLabel.FontSize;
            set => lblCustomLabel.FontSize = value;
        }

        public double ImageSize
        {
         
            set 
            { 
                imgCustomImage.HeightRequest = value;
                imgCustomImage.WidthRequest = value;
            }
        }

    }
}
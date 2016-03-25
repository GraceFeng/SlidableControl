using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CreateSilidableListView
{
    public sealed partial class SlidableControl : UserControl
    {
        private double maxheight;
        private double Y;
        private double finalY;

        public SlidableControl()
        {
            this.InitializeComponent();
            maxheight = Window.Current.Bounds.Height / 3;
            SlidArea.Visibility = Visibility.Collapsed;
        }

        private void SlidRoot_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            SlidArea.Visibility = Visibility.Visible;
            SlidTitleTransform.TranslateY = -maxheight;
        }

        private void SlidRoot_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Y = e.Delta.Translation.Y;
            finalY = SlidTitleTransform.TranslateY + Y;
            if (Y >= 0 && finalY <= 0)
            {
                if (finalY < maxheight)
                    SlidTitleTransform.TranslateY = finalY;
                else
                    SlidTitleTransform.TranslateY = 0;
            }
            else if (Y < 0 && finalY >= -maxheight)
            {
                if (finalY > -maxheight)
                    SlidTitleTransform.TranslateY = finalY;
                else
                {
                    SlidTitleTransform.TranslateY = -maxheight;
                }
            }
        }

        private void SlidRoot_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (finalY <= -maxheight)
            {
                SlidArea.Visibility = Visibility.Collapsed;
                SlidTitleTransform.TranslateY = 0;
            }
        }

        public static readonly DependencyProperty ChildProperty = DependencyProperty.Register("SlidChild", typeof(UIElement), typeof(SlidableControl), new PropertyMetadata(null));

        public UIElement SlidChild
        {
            get { return (UIElement)GetValue(ChildProperty); }
            set { SetValue(ChildProperty, value); }
        }
    }
}
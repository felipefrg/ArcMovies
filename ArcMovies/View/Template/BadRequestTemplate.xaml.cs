using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ArcMovies.View.Template
{
    public partial class BadRequestTemplate : ContentView
    {
        public BadRequestTemplate()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ReloadCommandProperty = BindableProperty.Create(nameof(ReloadCommand)
                                                                                                , typeof(Command)
                                                                                                , typeof(BadRequestTemplate)
                                                                                                , null);

        public Command ReloadCommand
        {
            get { return (Command)GetValue(ReloadCommandProperty); }
            set { SetValue(ReloadCommandProperty, value); }
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            ReloadCommand?.Execute(null);
        }
    }
}

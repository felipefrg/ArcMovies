using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArcMovies.View.Template
{
    public class MovieBaseTemplate : ContentView
    {
        public object SelectCommandParameter
        {
            get { return (object)GetValue(SelectCommandParameterProperty); }
            set { SetValue(SelectCommandParameterProperty, value); }
        }
        public static readonly BindableProperty SelectCommandParameterProperty
            = BindableProperty.Create(
                                    nameof(SelectCommandParameter),
                                    typeof(object),
                                    typeof(MovieBaseTemplate),
                                    null,
                                    BindingMode.TwoWay,
                                    null,
                                    HandleBindingPropertyChangedDelegate
                                                    );

        public ICommand SelectCommand
        {
            get { return (ICommand)GetValue(SelectCommandProperty); }
            set { SetValue(SelectCommandProperty, value); }
        }
        public static readonly BindableProperty SelectCommandProperty
            = BindableProperty.Create(
                                    nameof(SelectCommand),
                                    typeof(ICommand),
                                    typeof(MovieBaseTemplate),
                                    default(Command),
                                    BindingMode.TwoWay,
                                    null,
                                    HandleBindingPropertyChangedDelegate

                );

        static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
        }


        static void HandleBindingPropertyChangedDelegate1(BindableObject bindable, object oldValue, object newValue)
        {
        }


        protected void Handle_Tapped(object sender, System.EventArgs e)
        {
            if (this.SelectCommand != null && SelectCommand.CanExecute(SelectCommandParameter))
            {
                SelectCommand.Execute(SelectCommandParameter);
            }
        }

        public MovieBaseTemplate()
        {
        }
    }
}

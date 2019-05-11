using System.Collections;
using Xamarin.Forms;

namespace ArcMovies.ExtendedControl
{
    public class FlexLayoutExtended : FlexLayout
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
                                                                                                nameof(ItemsSource),
                                                                                                typeof(IEnumerable),
                                                                                                typeof(FlexLayoutExtended),
                                                                                                propertyChanged: OnItemsSourceChanged);
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
                                                                                                nameof(ItemTemplate),
                                                                                                typeof(DataTemplate),
                                                                                                typeof(FlexLayoutExtended));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        static void OnItemsSourceChanged(BindableObject bindable, object oldVal, object newVal)
        {
            var layout = (FlexLayoutExtended)bindable;

            layout.Children.Clear();
            if (newVal is IEnumerable newValue)
            {
                foreach (var item in newValue)
                {
                    layout.Children.Add(layout.CreateChildView(item));
                }
            }
        }

        Xamarin.Forms.View CreateChildView(object item)
        {
            var template = ItemTemplate.CreateContent();

            if (!(template is Xamarin.Forms.View view))
            {
                return null;
            }

            view.BindingContext = item;

            return view;
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArcMovies.ExtendedControl
{
    public class StackLayoutExtended : Grid
    {
        public IEnumerable ItemSource 
        { 
            get {   return (IEnumerable)GetValue(ItemSourceProperty); } 
            set {   SetValue(ItemSourceProperty, value);}
        }
        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(
                                                                                            nameof(ItemSource)
                                                                                            , typeof(IEnumerable)
                                                                                            , typeof(StackLayoutExtended)
                                                                                            , default(IEnumerable<object>)
                                                                                            , BindingMode.TwoWay
                                                                                            , null
                                                                                            , OnItemSourceChanged);

        static void OnItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var stackLayoutExtended = (StackLayoutExtended)bindable;
            if( stackLayoutExtended != null && oldValue != newValue)
            {
                stackLayoutExtended.SetItems();
            }

        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
                                                                                            nameof(SelectedItem)
                                                                                            , typeof(object)
                                                                                            , typeof(StackLayoutExtended)
                                                                                            , null
                                                                                            , BindingMode.TwoWay
                                                                                            , null
                                                                                            , OnSelectedItemChanged
                                                                                            );
        public ICommand SelectedItemCommand
        {
            get { return (ICommand)GetValue(SelectedItemCommandProperty); }
            set { SetValue(SelectedItemCommandProperty, value); }
        }
        public static readonly BindableProperty SelectedItemCommandProperty = BindableProperty.Create(
                                                                                            nameof(SelectedItemCommand)
                                                                                            , typeof(ICommand)
                                                                                            , typeof(StackLayoutExtended)
                                                                                            , null
                                                                                            , BindingMode.TwoWay
                                                                                            );


        static void  OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var stackLayoutExtended = (StackLayoutExtended)bindable;
            if(stackLayoutExtended != null)
            {
                if (newValue != null && oldValue != newValue)
                {
                    //Raise Event
                    stackLayoutExtended.SelectedItemChanged?.Invoke(stackLayoutExtended, EventArgs.Empty);

                    //Raise Command
                    if(stackLayoutExtended.SelectedItemCommand != null
                        && stackLayoutExtended.SelectedItemCommand.CanExecute(newValue))
                    {
                        stackLayoutExtended.SelectedItemCommand.Execute(newValue);
                    }
                }
            }
        }


        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
                                                                                            nameof(ItemTemplate)
                                                                                            , typeof(DataTemplate)
                                                                                            , typeof(StackLayoutExtended)
                                                                                            , default(DataTemplate)
                                                                                            , BindingMode.TwoWay
                                                                                            );
        ScrollView _scrollView { get; set; }
        StackLayout _stackLayout { get; set; }

        public int Spacing { get; set; }
        public StackOrientation Orientation { get; set; }
        public EventHandler SelectedItemChanged { get; set; }

        public StackLayoutExtended()
        {
            this.Spacing = 5;
            this._scrollView = new ScrollView();

            this._stackLayout = new StackLayout();
            this._stackLayout.Padding = this.Padding;
            this._stackLayout.Spacing = this.Spacing;

            this._scrollView.Content = this._stackLayout;
            this.Children.Add(this._scrollView);
        }

        protected virtual void SetItems()
        {
            this._stackLayout.Children.Clear();
            this._stackLayout.Orientation = Orientation;
            this._scrollView.Orientation = Orientation == StackOrientation.Horizontal ?
                                                          ScrollOrientation.Horizontal : ScrollOrientation.Vertical;

            if(ItemSource == null)
            {
                return;
            }

            foreach (var item in ItemSource)
            {
                this._stackLayout.Children.Add(CreateChildView(item));
            }

            SelectedItem = null;
        }

        protected virtual Xamarin.Forms.View CreateChildView(object item)
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

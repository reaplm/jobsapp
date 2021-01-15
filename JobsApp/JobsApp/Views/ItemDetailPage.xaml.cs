using JobsApp.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace JobsApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        private ItemDetailViewModel _viewmodel;
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewmodel = new ItemDetailViewModel();

            CreateLayout();
        }
        private void CreateLayout()
        {
            var mainLayout = new StackLayout();
            var scrollView = new ScrollView();

            //Location/Date views
            var locationIcon = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = "location_20.png",
                VerticalOptions = LayoutOptions.End
            };
            var calendarIcon = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = "calendar_20.png",
                VerticalOptions = LayoutOptions.End
            };
            var locationText = new Label
            {
                VerticalOptions = LayoutOptions.End
            };
            var postedText = new Label
            {
                VerticalOptions = LayoutOptions.End
            };
            locationText.SetBinding(Label.TextProperty, new Binding("Item.Location"));
            postedText.SetBinding(Label.TextProperty, new Binding("Item.Posted"));

            //Location/Date layout
            var locationLayout = new StackLayout
            {

                Children =
                {
                    locationIcon,
                    locationText,
                    calendarIcon,
                    postedText
                }
            };
            locationLayout.Orientation = StackOrientation.Horizontal;

            //Title/Fave btn views
            var titleText = new Label
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontFamily = "Century Gothic",
                FontSize = 24.0
            };
            var favButton = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 30,
            };
            titleText.SetBinding(Label.TextProperty, new Binding("Item.Title"));
            favButton.SetBinding(Image.SourceProperty, new Binding("Item.LikeSource"));

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, "LikeTapped");
            favButton.GestureRecognizers.Add(tapGestureRecognizer);

            //Header Layout
            var headerLayout = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(85) },
                    new RowDefinition { Height = new GridLength(50) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition()
                },
            };
            headerLayout.Children.Add(titleText);
            headerLayout.Children.Add(favButton);
            headerLayout.Children.Add(locationLayout);

            Grid.SetRow(titleText, 0);
            Grid.SetColumn(titleText, 0);
            Grid.SetColumnSpan(titleText, 4);

            Grid.SetRow(favButton, 0);
            Grid.SetColumn(favButton, 4);

            Grid.SetRow(locationLayout, 1);
            Grid.SetColumn(locationLayout, 0);
            Grid.SetColumnSpan(locationLayout, 4);

            //Description views
            var descriptionHeading = new Label { Style = Device.Styles.TitleStyle };
            var descText = new Label
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Style = Device.Styles.BodyStyle

            };

            descriptionHeading.SetBinding(Label.TextProperty, new Binding("DescriptionHeading"));
            descText.SetBinding(Label.TextProperty, new Binding("Item.Description"));


            var summaryLayout = new StackLayout
            {
                Margin = new Thickness(10),
                Children =
                {
                    descriptionHeading,
                    descText
                }
            };

            //Details views
            var qualificationHeading = new Label { Text = "Qualifications", Style = Device.Styles.TitleStyle };
            var qualificationText = new Label { Style = Device.Styles.BodyStyle }; ;
            var competenciesHeading = new Label { Text = "Competencies", Style = Device.Styles.TitleStyle };
            var competenciesText = new Label { Style = Device.Styles.BodyStyle };

            var empTypeText = new Label { Style = Device.Styles.BodyStyle }; 
            var postedDateText = new Label { Style = Device.Styles.BodyStyle }; 
            var closingDateText = new Label { Style = Device.Styles.BodyStyle }; 
            
            var contactsText = new Label();

            //Bindings
            qualificationHeading.SetBinding(Label.TextProperty, new Binding("QualificationsHeading"));
            competenciesHeading.SetBinding(Label.TextProperty, new Binding("CompetenciesHeading"));
            descriptionHeading.SetBinding(Label.TextProperty, new Binding("DescriptionHeading"));
            empTypeText.SetBinding(Label.TextProperty, new Binding("Item.Type"));
            empTypeText.SetBinding(Label.TextProperty, new Binding("Item.Type"));
            postedDateText.SetBinding(Label.TextProperty, new Binding("Item.PostedDateString"));
            closingDateText.SetBinding(Label.TextProperty, new Binding("Item.ClosingDateString"));
            qualificationText.SetBinding(Label.TextProperty, new Binding("Item.QualificationsToString"));
            competenciesText.SetBinding(Label.TextProperty, new Binding("Item.CompetenciesToString"));
            contactsText.SetBinding(Label.TextProperty, new Binding("Item.ContactsToString")); 

             var detailsLayout = new StackLayout
            {
                 Margin = new Thickness(10),
                Children =
                {
                    qualificationHeading,
                    qualificationText,
                    competenciesHeading,
                    competenciesText,
                    empTypeText,
                    closingDateText,
                    contactsText

                }
            };
      

            var HeaderFrame = new Frame
            {
                HasShadow = true,
                Margin = 5
            };
        

            HeaderFrame.Content =headerLayout;

            mainLayout.Children.Add(HeaderFrame);
            mainLayout.Children.Add(summaryLayout);
            mainLayout.Children.Add(detailsLayout);
            scrollView.Content = mainLayout;

            Content = scrollView;
        }
    }
}
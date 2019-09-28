using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Flipper_Extended
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> exchangeCurrencies = new List<string>() { "chaos", "exa" };
        List<String> currencies = new List<string>();
        Dictionary<string, CurrencyInfo> listingDict = new Dictionary<string, CurrencyInfo>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Alteration.IsChecked == true)
            {
                currencies.Add("alt");
            }
            if (Fusing.IsChecked == true)
            {
                currencies.Add("fuse");
            }
            if (Divine.IsChecked == true)
            {
                currencies.Add("divine");
            }
            if (Jeweller.IsChecked == true)
            {
                currencies.Add("jew");
            }
            GetListings listingGetter = new GetListings();
            foreach (string currency in currencies)
            {
                List<List<Result>> listOfList = new List<List<Result>>();
                foreach (string exchangeCurrency in exchangeCurrencies)
                {
                    listOfList.Add(listingGetter.RequestListing(currency, exchangeCurrency));
                    listOfList.Add(listingGetter.RequestListing(exchangeCurrency, currency));
                }
                listingDict.Add(currency, new CurrencyInfo(listOfList[0], listOfList[1], listOfList[2], listOfList[3]));
            }
            MessageBox.Show(listingDict[currencies[0]].chaosBuy.listings[0].pricePerUnit.ToString());
        }
        private double GetPrice(double topPrice, bool selling = true)
        {
            double value = (int.Parse(ChaosAmount.Text) / topPrice);
            if (selling)
            {
                return ((value % 1) == 0) ? value + 1 : Convert.ToInt32(Math.Ceiling(value));
            }
            else
            {
                return ((value % 1) == 0) ? (Convert.ToInt32(Math.Floor(value)) - 1) : Convert.ToInt32(Math.Floor(value));
            }
        }
    }
}

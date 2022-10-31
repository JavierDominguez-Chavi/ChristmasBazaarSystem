using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ChristmasBazaar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.TextBlock_SocksNotAvailable.Visibility = Visibility.Hidden;
            this.TextBlock_MenSweaterNotAvailable.Visibility = Visibility.Hidden;
            this.TextBlock_WomenSweaterNotAvailable.Visibility = Visibility.Hidden;
        }
        private void Label_Gratitude_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateNameAndAddresAreNotEmpty())
            {
                ValidateOrdersAreNotEmpty();
            }
            else
            {
                MessageBox.Show("Para realizar un pedido debe ingresar su nombre y direccion",
                                "Nombre y/o direccion no ingresado", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private Boolean ValidateNameAndAddresAreNotEmpty()
        {
            Boolean validation = false;
            if (String.IsNullOrWhiteSpace(this.TextBox_Name.Text) || String.IsNullOrEmpty(this.TextBox_Addres.Text))
            {
                validation = true;
            }
            return validation;
        }

        private void ValidateOrdersAreNotEmpty()
        {
            bool socksValidation = false;
            bool menSweaterValidation = false;
            bool womenSweaterValidation = false;
            if (!String.IsNullOrWhiteSpace(this.TextBox_ChristmasSocksQuantity.Text))
            {
                socksValidation = ValidateSocksInventory(socksValidation);
            }
            else
            {
                socksValidation = true;
            }
            if (!String.IsNullOrWhiteSpace(this.TextBox_MenChristmasSweaterQuantity.Text))
            {
                 menSweaterValidation = ValidateMenSweaterInventory(menSweaterValidation);
            }
            else
            {
                menSweaterValidation = true;
            }
            if (!String.IsNullOrWhiteSpace(this.TextBox_WomenChristmasSweaterQuantity.Text))
            {
                womenSweaterValidation = ValidateWomenSweaterInventory(womenSweaterValidation);
            }
            else
            {
                womenSweaterValidation = true;
            }
            BuyProcessing(socksValidation, menSweaterValidation, womenSweaterValidation);
        }
        private Boolean ValidateSocksInventory(bool socksValidation)
        {
            this.TextBlock_SocksNotAvailable.Visibility = Visibility.Hidden;
            if (Int32.Parse(this.TextBox_ChristmasSocksQuantity.Text) > 200) //Aqui en lugar de 200 va una variable dinamica que consulta en la base de datos cuanto producto queda
                {
                    this.TextBlock_SocksNotAvailable.Visibility = Visibility.Visible;
                    this.TextBox_ChristmasSocksQuantity.Clear();
                    socksValidation = true;
                }
            
            return socksValidation;
        }

        private Boolean ValidateMenSweaterInventory(bool menSweaterValidation)
        {
            this.TextBlock_MenSweaterNotAvailable.Visibility = Visibility.Hidden;
            if (Int32.Parse(this.TextBox_MenChristmasSweaterQuantity.Text) > 50) //Aqui en lugar de 50 va una variable dinamica que consulta en la base de datos cuanto producto queda
                {
                    this.TextBlock_MenSweaterNotAvailable.Visibility = Visibility.Visible;
                    this.TextBox_MenChristmasSweaterQuantity.Clear();
                    menSweaterValidation = true;
                }
            
            return menSweaterValidation;
        }
        private Boolean ValidateWomenSweaterInventory(bool womenSweaterValidation)
        {
            this.TextBlock_WomenSweaterNotAvailable.Visibility = Visibility.Hidden;
            if (Int32.Parse(this.TextBox_WomenChristmasSweaterQuantity.Text) > 150) //Aqui en lugar de 150 va una variable dinamica que consulta en la base de datos cuanto producto queda
                {
                    this.TextBlock_WomenSweaterNotAvailable.Visibility = Visibility.Visible;
                    this.TextBox_WomenChristmasSweaterQuantity.Clear();
                    womenSweaterValidation = true;
                }
            return womenSweaterValidation;
        }
        private void BuyProcessing(bool socksValidation, bool menSweaterValidation, bool womenSweaterValidation) 
        {
            if (!socksValidation || !menSweaterValidation || !womenSweaterValidation)
            {
                this.TextBlock_SocksNotAvailable.Visibility = Visibility.Hidden;
                this.TextBlock_MenSweaterNotAvailable.Visibility = Visibility.Hidden;
                this.TextBlock_WomenSweaterNotAvailable.Visibility = Visibility.Hidden;
                MessageBox.Show("Paso:p");//Codigo que saca los totales del producto
            }
            else 
            {
                MessageBox.Show("La cantidad de productos solicitados excede o es nula por favor verifique de nuevo",
                                 "Cantidad de productos inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs textCompositionEvent) 
        {
            Regex regex = new Regex("[^0-9]+");
            textCompositionEvent.Handled = regex.IsMatch(textCompositionEvent.Text);
        }
    }
}

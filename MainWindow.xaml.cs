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
        }
        private void Label_Gratitude_Click(object sender, RoutedEventArgs e)
        {
            bool nameAndAddresValidation = ValidateNameAndAddresAreNotEmpty();
            
            if (nameAndAddresValidation && ValidateSocksInventory() && ValidateWomenSweaterInventory() //HAY QUE REFACTORIZAR PARA BAJAR COMPLEJIDAD
            && ValidateMenSweaterInventory() && ValidateOrderIsNotZero())
            {
                MessageBox.Show("Feliz Hallowen Navideño");
            }
            else
            {
                //Acciones
            }
        }
        private Boolean ValidateNameAndAddresAreNotEmpty()
        {
            Boolean validation = true;
            if (String.IsNullOrWhiteSpace(this.TextBox_Name.Text) || String.IsNullOrEmpty(this.TextBox_Addres.Text))
            {
                MessageBox.Show("Para realizar un pedido debe ingresar su nombre y direccion",
                                "Nombre y/o direccion no ingresado", MessageBoxButton.OK, MessageBoxImage.Warning);
                validation = false;
            }
            return validation;
        }

        private Boolean ValidateOrdersAreNotEmpty()
        {
            Boolean validation = true;
            if (String.IsNullOrWhiteSpace(this.TextBox_ChristmasSocksQuantity.Text) && String.IsNullOrWhiteSpace(this.TextBox_MenChristmasSweaterQuantity.Text)
            || String.IsNullOrWhiteSpace(this.TextBox_WomenChristmasSweaterQuantity.Text) )
            {
                MessageBox.Show("Los debe ingresar a cantidad de por lo menos un producto");//Tambien es etiqueta
                validation = false;
            }
            return validation;
        }
        private Boolean ValidateSocksInventory()
        {
            Boolean socksValidation = true;
            if (ValidateOrdersAreNotEmpty())
            {
                if (Int32.Parse(this.TextBox_ChristmasSocksQuantity.Text) > 200) //Aqui en lugar de 200 va una variable dinamica que consulta en la base de datos cuanto producto queda
                {
                    //Cambier este mensaje por un etiqueta que se ponga roja que no sea una ventana emergente
                    MessageBox.Show("El pedido solicitado excede el inventario ");
                    socksValidation = false;
                }
            }
            return socksValidation;
        }

        private Boolean ValidateMenSweaterInventory()
        {
            Boolean menSweaterValidation = true;
            if (ValidateOrdersAreNotEmpty())
            {
                if (Int32.Parse(this.TextBox_MenChristmasSweaterQuantity.Text) > 50) //Aqui en lugar de 50 va una variable dinamica que consulta en la base de datos cuanto producto queda
                {
                    //Cambier este mensaje por un etiqueta que se ponga roja que no sea una ventana emergente
                    MessageBox.Show("El pedido solicitado excede el inventario ");
                    menSweaterValidation = false;
                }
            }
            return menSweaterValidation;
        }


        private Boolean ValidateWomenSweaterInventory()
        {
            Boolean womenSweaterValidation = true;
            if (ValidateOrdersAreNotEmpty())
            {
                if (Int32.Parse(this.TextBox_MenChristmasSweaterQuantity.Text) > 150) //Aqui en lugar de 150 va una variable dinamica que consulta en la base de datos cuanto producto queda
                {
                    //Cambier este mensaje por un etiqueta que se ponga roja que no sea una ventana emergente
                    MessageBox.Show("El pedido solicitado excede el inventario ");
                    womenSweaterValidation = false;
                }
            }
            return womenSweaterValidation;
        }

        private Boolean ValidateOrderIsNotZero()
        {
            Boolean validation = true;
            if (ValidateOrdersAreNotEmpty())
            {
                if (Int32.Parse(this.TextBox_ChristmasSocksQuantity.Text) <= 0 || Int32.Parse(this.TextBox_MenChristmasSweaterQuantity.Text) <= 0
                || Int32.Parse(this.TextBox_WomenChristmasSweaterQuantity.Text) <= 0)
                {
                    MessageBox.Show("El pedido solicitado debe ser mayor a 0");
                    validation = false;
                }
            }
            return validation;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs textCompositionEvent) 
        {
            Regex regex = new Regex("[^0-9]+");
            textCompositionEvent.Handled = regex.IsMatch(textCompositionEvent.Text);
        }
    }
}

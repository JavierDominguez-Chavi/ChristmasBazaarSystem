using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
            try
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
            catch (EndpointNotFoundException enfException)
            {
                MessageBox.Show("No hay conexión con la base de datos");
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

            ProductoManager productoManager = new ProductoManager();
            int sockExistance = productoManager.GetExistenciaById(((int)IdProducts.TINES));
            if (Int32.Parse(this.TextBox_ChristmasSocksQuantity.Text) > sockExistance) //Aqui en lugar de 200 va una variable dinamica que consulta en la base de datos cuanto producto queda
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

            ProductoManager productoManager = new ProductoManager();
            int menSweaterExistance = productoManager.GetExistenciaById(((int)IdProducts.SUETER_HOMBRE));
            if (Int32.Parse(this.TextBox_MenChristmasSweaterQuantity.Text) > menSweaterExistance) //Aqui en lugar de 50 va una variable dinamica que consulta en la base de datos cuanto producto queda
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

            ProductoManager productoManager = new ProductoManager();
            int womenSweaterExistance = productoManager.GetExistenciaById(((int)IdProducts.SUETER_MUJER));
            if (Int32.Parse(this.TextBox_WomenChristmasSweaterQuantity.Text) > womenSweaterExistance) //Aqui en lugar de 150 va una variable dinamica que consulta en la base de datos cuanto producto queda
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

                
                double total = CalculateTotal();
                Pedido pedido = MakePedido(total);
                AddPedido(pedido);
            }
            else 
            {
                MessageBox.Show("La cantidad de productos solicitados excede o es nula por favor verifique de nuevo",
                                 "Cantidad de productos inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private Pedido MakePedido(double total)
        {
            Pedido pedido = new Pedido();
            pedido.fecha = GetDeliveryDate();
            pedido.total = total;
            pedido.nombre = this.TextBox_Name.Text;
            pedido.direccion = this.TextBox_Addres.Text;
            return pedido;
        }

        private double CalculateTotal()
        {
            ProductoManager productoManager = new ProductoManager();
            double socksPrecio = productoManager.GetPrecioById((int)IdProducts.TINES);
            double menSweaterPrecio = productoManager.GetPrecioById((int)IdProducts.SUETER_HOMBRE);
            double womenSweaterPrecio = productoManager.GetPrecioById((int)IdProducts.SUETER_MUJER);
            int sockQuantity = GetQuantity(this.TextBox_ChristmasSocksQuantity.Text);
            int menSweaterQuantity = GetQuantity(this.TextBox_MenChristmasSweaterQuantity.Text);
            int womenSweaterQuantity = GetQuantity(this.TextBox_WomenChristmasSweaterQuantity.Text);
            double subtotal =
                (socksPrecio * sockQuantity)
                + (menSweaterPrecio * menSweaterQuantity)
                + (womenSweaterPrecio * womenSweaterQuantity);
            this.TextBox_SubTotal.Text = subtotal.ToString();

            double discount = GetDiscount(subtotal);
            this.TextBox_Discount.Text = discount.ToString();
            subtotal -= discount;

            double iva = subtotal * 0.16;
            this.TextBox_IVA.Text = iva.ToString();

            double total = subtotal + iva;
            this.TextBox_Total.Text = total.ToString();

            return total;
        }

        private void AddPedido(Pedido pedido)
        {
            PedidosProducto tines = new PedidosProducto();
            tines.cantidad = GetQuantity(this.TextBox_ChristmasSocksQuantity.Text);
            tines.idProducto = (int)IdProducts.TINES;

            PedidosProducto sueterHombre = new PedidosProducto();
            sueterHombre.cantidad = GetQuantity(this.TextBox_MenChristmasSweaterQuantity.Text);
            sueterHombre.idProducto = ((int)IdProducts.SUETER_HOMBRE);

            PedidosProducto sueterMujer = new PedidosProducto();
            sueterMujer.cantidad = GetQuantity(this.TextBox_WomenChristmasSweaterQuantity.Text);
            sueterMujer.idProducto = ((int)IdProducts.SUETER_MUJER);

            pedido.PedidosProductos.Add(tines);
            pedido.PedidosProductos.Add(sueterHombre);
            pedido.PedidosProductos.Add(sueterMujer);

            PedidoManager pedidoManager = new PedidoManager();
            pedidoManager.AddPedido(pedido);
        }

        private DateTime GetDeliveryDate()
        {
            DateTime today = DateTime.Now;
            DayOfWeek dayOfWeek = today.DayOfWeek;
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    today = today.AddDays(5);
                    break;
                case DayOfWeek.Friday:
                case DayOfWeek.Tuesday:
                    today = today.AddDays(4);
                    break;
                case DayOfWeek.Saturday:
                case DayOfWeek.Wednesday:
                    today = today.AddDays(3);
                    break;
                case DayOfWeek.Sunday:
                case DayOfWeek.Thursday:
                    today = today.AddDays(2);
                    break;

            }
            this.TextBox_DateOfDelivery.Text = (today.ToString("dd/MM/yyyy"));
            return today;
        }

        private int GetQuantity(String textBoxInput)
        {
            int quantity = 0;
            if (!String.IsNullOrWhiteSpace(textBoxInput))
            {
                quantity = Int32.Parse(textBoxInput);
            }
            return quantity;
        }

        private double GetDiscount(double subtotal)
        { 
            String discountCode = this.TextBox_DiscountCode.Text;
            double discountModifier = 0;
            switch (discountCode)
            {
                case "DASHER":
                    discountModifier = ((int)DiscountCodes.DASHER);
                    break;
                case "DANCER":
                    discountModifier = ((int)DiscountCodes.DANCER);
                    break;
                case "PRANCER":
                    discountModifier = ((int)DiscountCodes.PRANCER);
                    break;
                case "RUDOLPH":
                    discountModifier = ((int)DiscountCodes.RUDOLPH);
                    break;
            }
            discountModifier = (discountModifier/100);
            return subtotal * discountModifier;
        }



        private void NumberValidationTextBox(object sender, TextCompositionEventArgs textCompositionEvent) 
        {
            Regex regex = new Regex("[^0-9]+");
            textCompositionEvent.Handled = regex.IsMatch(textCompositionEvent.Text);
        }

        private void Button_Next_Click(object sender, RoutedEventArgs e)
        {
            this.TextBox_Addres.Clear();
            this.TextBox_ChristmasSocksQuantity.Clear();
            this.TextBox_DateOfDelivery.Clear();
            this.TextBox_Discount.Clear();
            this.TextBox_DiscountCode.Clear();
            this.TextBox_IVA.Clear();
            this.TextBox_MenChristmasSweaterQuantity.Clear();
            this.TextBox_Name.Clear();
            this.TextBox_SubTotal.Clear();
            this.TextBox_Total.Clear();
            this.TextBox_WomenChristmasSweaterQuantity.Clear();
            
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

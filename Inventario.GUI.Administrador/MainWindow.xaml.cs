using Inventario.BIZ;
using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;
using Inventario.DAL;
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

namespace Inventario.GUI.Administrador
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum accion
        {
            Nuevo, 
            Editar
        }

        IManejadorEmpleados manejdorEmpleados;

        accion accionEmpleados;

        public MainWindow()
        {
            InitializeComponent();

            manejdorEmpleados = new ManejadorEmpleados(new RepositorioDeEmpleados());

            PonerBontonesEmpleadosEnEdicion(false);
            LimpiarCamposDeEmpleados();
            ActualizarTablaEmpleados();
        }

        private void ActualizarTablaEmpleados()
        {
            dtgEmpleados.ItemsSource = null;
            dtgEmpleados.ItemsSource = manejdorEmpleados.Listar;
        }

        private void LimpiarCamposDeEmpleados()
        {
            txbEmpleadosApellidos.Clear();
            txbEmpleadosArea.Clear();
            txbEmpleadosId.Text = "";
            txbEmpleadosNombre.Clear();
        }

        private void PonerBontonesEmpleadosEnEdicion(bool value)
        {
            btnEmpleadosCancelar.IsEnabled = value;
            btnEmpleadosEditar.IsEnabled = !value;
            btnEmpleadosEliminar.IsEnabled = !value;
            btnEmpleadosGuardar.IsEnabled = value;
            btnEmpleadosNuevo.IsEnabled = !value;
        }

        private void btnEmpleadosNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeEmpleados();
            PonerBontonesEmpleadosEnEdicion(true);
            accionEmpleados = accion.Nuevo;

        }

        private void btnEmpleadosGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionEmpleados == accion.Nuevo)
            {
                Empleado emp = new Empleado();
                { 
                    //Ingresar datos
                }
            }
        }

        private void btnEmpleadosCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeEmpleados();
            PonerBontonesEmpleadosEnEdicion(false);
        }

        private void btnEmpleadosEliminar_Click(object sender, RoutedEventArgs e)
        {
            Empleado emp = dtgEmpleados.SelectedItem as Empleado;
            if (emp != null)
            {
                if (MessageBox.Show("¿Desea eliminar al empleado?", "Inventarios", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejdorEmpleados.Eliminar(emp.Id))
                    {
                        MessageBox.Show("Empleado eliminado", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaEmpleados();
                    }
                    else 
                    {
                        MessageBox.Show("No se pudo eliminar el empleado", "Inventarios", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void btnEmpleadosEditar_Click_1(object sender, RoutedEventArgs e)
        {
            Empleado emp = dtgEmpleados.SelectedItem as Empleado;
            if (emp != null)
            {
                txbEmpleadosId.Text = emp.Id;
                txbEmpleadosApellidos.Text = emp.Apellidos;
                txbEmpleadosArea.Text = emp.Area;
                txbEmpleadosNombre.Text = emp.Nombre;

                accionEmpleados = accion.Editar;
                PonerBontonesEmpleadosEnEdicion(true);
            }
        }
    }
}

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

        IManejadorMateriales manejdorMateriales;

        accion accionMateriales;

        public MainWindow()
        {
            InitializeComponent();

            manejdorMateriales = new ManejadorMateriales(new RepositorioDeMaterial());

            PonerBontonesMaterialesEnEdicion(false);
            LimpiarCamposDeMateriales();
            ActualizarTablaMateriales();
        }

        private void ActualizarTablaMateriales()
        {
            dtgMateriales.ItemsSource = null;
            dtgMateriales.ItemsSource = manejdorMateriales.Listar;
        }

        private void LimpiarCamposDeMateriales()
        {
            txbMaterialCategoria.Clear();
            txbMaterialDescripcion.Clear();
            txbMaterialExitencia.Clear();
            txbMaterialId.Text = "";
            txbMaterialNombre.Clear();
        }

        private void PonerBontonesMaterialesEnEdicion(bool value)
        {
            btnMaterialesCancelar.IsEnabled = value;
            btnMaterialesEditar.IsEnabled = !value;
            btnMaterialesEliminar.IsEnabled = !value;
            btnMaterialesGuardar.IsEnabled = value;
            btnMaterialesNuevo.IsEnabled = !value;
        }

        private void btnMaterialesNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeMateriales();
            PonerBontonesMaterialesEnEdicion(true);
            accionMateriales = accion.Nuevo;

        }

        private void btnMaterialesGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionMateriales == accion.Nuevo)
            {
                Material emp = new Material()
                {
                    Nombre = txbMaterialNombre.Text,
                    Categoria = txbMaterialCategoria.Text,
                    Descripcion = txbMaterialDescripcion.Text,
                    Existencia = txbMaterialExitencia.Text

                };
                if (manejdorMateriales.Agregar(emp))
                {
                    MessageBox.Show("Artículo agregado correctamente", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposDeMateriales();
                    ActualizarTablaMateriales();
                    PonerBontonesMaterialesEnEdicion(false);
                }
                else 
                {
                    MessageBox.Show("El Artículo no se pudo agregar", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Material mat = dtgMateriales.SelectedItem as Material;
                mat.Nombre = txbMaterialNombre.Text;
                mat.Categoria = txbMaterialCategoria.Text;
                mat.Descripcion = txbMaterialDescripcion.Text;
                mat.Existencia = txbMaterialExitencia.Text;
                if (manejdorMateriales.Modificar(mat))
                {
                    MessageBox.Show("Artículo actualizado correctamente", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposDeMateriales();
                    ActualizarTablaMateriales();
                    PonerBontonesMaterialesEnEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Artículo no se pudo actualizar", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnMaterialesCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeMateriales();
            PonerBontonesMaterialesEnEdicion(false);
        }

        private void btnMaterialesEliminar_Click(object sender, RoutedEventArgs e)
        {
            Material mat = dtgMateriales.SelectedItem as Material;
            if (mat != null)
            {
                if (MessageBox.Show("¿Desea eliminar al empleado?", "Inventarios", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejdorMateriales.Eliminar(mat.Id))
                    {
                        MessageBox.Show("Artículo eliminado", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaMateriales();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el artículo", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }    
            }     
        }

        private void btnMaterialesEditar_Click(object sender, RoutedEventArgs e)
        {
            Material mat = dtgMateriales.SelectedItem as Material;
            if (mat != null)
            {
                txbMaterialId.Text = mat.Id;
                txbMaterialNombre.Text = mat.Nombre;
                txbMaterialCategoria.Text = mat.Categoria;
                txbMaterialExitencia.Text = mat.Existencia;

                accionMateriales = accion.Editar;
                PonerBontonesMaterialesEnEdicion(true);
            }
        }

    }
}

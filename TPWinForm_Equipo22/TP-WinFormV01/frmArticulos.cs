﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Catalogo;
using Dominio;
using static System.Net.Mime.MediaTypeNames;

namespace TP_WinFormV01
{
    public partial class frmArticulos : Form
    {
        private List<Articulos> listaArticulos;
        private List<Imagenes> listarImagenes;

        public frmArticulos()
        {
            InitializeComponent();
        }

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            cargar();
            cbCampo.Items.Add("Nombre");
            cbCampo.Items.Add("Categoria");
            cbCampo.Items.Add("Marca");
            cbCampo.Items.Add("Precio");

        }
        private void btnFiltro_Click(object sender, EventArgs e)
        {
            ElementosCatalogo articulo = new ElementosCatalogo();
            try
            {
                if (validarFiltro())
                    return;

                string campo = cbCampo.SelectedItem.ToString();
                string criterio = CbCriterio.SelectedItem.ToString();
                string filtro = txtArticulos.Text;
                dgvBuscarArt.DataSource = articulo.filtrar(campo, criterio, filtro);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnListaTdo_Click(object sender, EventArgs e)
        {
            dgvBuscarArt.DataSource = null;
            dgvBuscarArt.DataSource = listaArticulos;
        }
        private void cargar()
        {
            ElementosCatalogo negocio = new ElementosCatalogo();
            CatalogoImagenes imagen = new CatalogoImagenes();
            try
            {
                listaArticulos = negocio.listar();
                dgvBuscarArt.DataSource = listaArticulos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dgvBuscarArt_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBuscarArt.CurrentRow != null)
            {
                CatalogoImagenes imagenes=new CatalogoImagenes();
                Articulos seleccionado = (Articulos)dgvBuscarArt.CurrentRow.DataBoundItem;
                listarImagenes = imagenes.listar(seleccionado);
                cargarImagen(listarImagenes[0].ImagenURL);
            }
        }
        private void cargarImagen(string Url)
        {

            try
            {
                PbxImagen.Load(Url);
            }
            catch (Exception ex)
            {
                PbxImagen.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }
        private bool validarFiltro()
        {
            if (cbCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el campo para filtrar.");
                return true;
            }
            if (CbCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el criterio para filtrar.");
                return true;
            }
            if (cbCampo.SelectedItem.ToString() == "Número")
            {
                if (string.IsNullOrEmpty(txtArticulos.Text))
                {
                    MessageBox.Show("Debes cargar el filtro para numéricos...");
                    return true;
                }
                if (!(soloNumeros(txtArticulos.Text)))
                {
                    MessageBox.Show("Solo nros para filtrar por un campo numérico...");
                    return true;
                }

            }

            return false;
        }
        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }
        private void cbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cbCampo.SelectedItem.ToString();
            if (opcion == "Precio")
            {
                CbCriterio.Items.Clear();
                CbCriterio.Items.Add("Mayor a");
                CbCriterio.Items.Add("Menor a");
                CbCriterio.Items.Add("Igual a");
            }
            else
            {
                CbCriterio.Items.Clear();
                CbCriterio.Items.Add("Comienza con");
                CbCriterio.Items.Add("Termina con");
                CbCriterio.Items.Add("Contiene");
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

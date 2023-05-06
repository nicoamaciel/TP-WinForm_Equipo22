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

namespace TP_WinFormV01
{
    public partial class frmArticulos : Form
    {
        private List<Articulos> listaArticulos;
        public frmArticulos()
        {
            InitializeComponent();
        }

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            ElementosCatalogo art = new ElementosCatalogo();
            listaArticulos = art.listar();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Articulos> filtroArt;

            filtroArt = listaArticulos.FindAll(x => x.Codigo == txtArticulos.Text);
            dgvBuscarArt.DataSource = null;
            dgvBuscarArt.DataSource = filtroArt;
        }

        private void btnListaTdo_Click(object sender, EventArgs e)
        {
            dgvBuscarArt.DataSource = null;
            dgvBuscarArt.DataSource = listaArticulos;
        }

        private void btnFisico_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void eliminar(bool logico = false)
        {
            ElementosCatalogo negocio = new ElementosCatalogo();
            Articulos seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿De verdad querés eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulos)dgvBuscarArt.CurrentRow.DataBoundItem;
                        negocio.eliminar(seleccionado.ID);

                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void cargar()
        {
            ElementosCatalogo negocio = new ElementosCatalogo();
            try
            {
                listaArticulos = negocio.listar();
                dgvBuscarArt.DataSource = listaArticulos;
                //cargarImagen(listaArticulos[0].UrlImagen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
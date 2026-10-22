using System;
using System.Data;
using System.Windows.Forms;

namespace parcial2
{
    public partial class login : Form
    {
        Conexion_base_datos objConexion = new Conexion_base_datos();

        public login()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            objConexion.crearDs();

            if (objConexion.miDS != null && objConexion.miDS.Tables.Contains("usuarios_USSS107526"))
            {
                DataTable dt = objConexion.miDS.Tables["usuarios_USSS107526"];

                string user = txtUsuario.Text.Trim();
                string pass = txtClave.Text.Trim();

                DataRow[] filas = dt.Select($"usuario = '{user}' AND clave = '{pass}'");

                if (filas.Length > 0)
                {
                    MessageBox.Show("Acceso concedido.", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();

                    frm_usuario frm = new frm_usuario();
                    frm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Acceso denegado. Usuario o clave incorrectos.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo conectar a la base de datos o cargar la tabla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
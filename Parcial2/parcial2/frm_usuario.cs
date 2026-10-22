using parcial2;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace parcial2
{
    public partial class frm_usuario : Form
    {
        Conexion_base_datos objConexion = new Conexion_base_datos();
        int posicion = 0;
        string accion = "nuevo";

        public frm_usuario()
        {
            InitializeComponent();
        }

        private void frm_usuario_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            objConexion.crearDs();
            if (objConexion.miDS.Tables["usuarios_USSS107526"].Rows.Count > 0)
            {
                MostrarRegistro(posicion);
            }
        }

        private void MostrarRegistro(int index)
        {
            DataRow fila = objConexion.miDS.Tables["usuarios_USSS107526"].Rows[index];
            txtIdUsuario.Text = fila["idUsuario"].ToString();
            txtUsuario.Text = fila["usuario"].ToString();
            txtClave.Text = fila["clave"].ToString();
            txtConfirmarClave.Text = fila["clave"].ToString();
            txtNombre.Text = fila["nombre"].ToString();
            txtDireccion.Text = fila["direccion"].ToString();
            txtTelefono.Text = fila["telefono"].ToString();
        }

        // Navegación
        private void btnPrimero_Click(object sender, EventArgs e) { posicion = 0; MostrarRegistro(posicion); }
        private void btnAnterior_Click(object sender, EventArgs e) { if (posicion > 0) { posicion--; MostrarRegistro(posicion); } }
        private void btnSiguiente_Click(object sender, EventArgs e) { if (posicion < objConexion.miDS.Tables["usuarios_USSS107526"].Rows.Count - 1) { posicion++; MostrarRegistro(posicion); } }
        private void btnUltimo_Click(object sender, EventArgs e) { posicion = objConexion.miDS.Tables["usuarios_USSS107526"].Rows.Count - 1; MostrarRegistro(posicion); }

        // Guardar con validaciones
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string usr = txtUsuario.Text.Trim();
            string pass = txtClave.Text.Trim();
            string confirmPass = txtConfirmarClave.Text.Trim();

            // 1. Validar longitud del usuario (mínimo 6, máximo 16)
            if (usr.Length < 6 || usr.Length > 16)
            {
                MessageBox.Show("El usuario debe tener entre 6 y 16 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validar confirmación de clave
            if (pass != confirmPass)
            {
                MessageBox.Show("La confirmación de la contraseña no coincide.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Validar que la clave lleve letras y números
            bool tieneLetra = pass.Any(char.IsLetter);
            bool tieneNumero = pass.Any(char.IsDigit);

            if (!tieneLetra || !tieneNumero)
            {
                MessageBox.Show("La contraseña debe incluir al menos una letra y un número.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Guardar en la base de datos
            string[] datos = new string[] {
                txtIdUsuario.Text,
                usr,
                pass,
                txtNombre.Text.Trim(),
                txtDireccion.Text.Trim(),
                txtTelefono.Text.Trim()
            };

            string res = objConexion.mantenimiento_usuarios_USSS107526(datos, accion);
            if (res == "Ok")
            {
                MessageBox.Show("Registro guardado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            accion = "nuevo";
            txtIdUsuario.Clear();
            txtUsuario.Clear();
            txtClave.Clear();
            txtConfirmarClave.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtUsuario.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            accion = "modificar";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de eliminar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string[] datos = new string[] { txtIdUsuario.Text };
                objConexion.mantenimiento_usuarios_USSS107526(datos, "eliminar");
                posicion = 0;
                CargarDatos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpiar todas las cajas de texto
            txtIdUsuario.Clear();
            txtUsuario.Clear();
            txtClave.Clear();
            txtConfirmarClave.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();

            // Resetear la acción a nuevo y enfocar el primer campo
            accion = "nuevo";
            txtUsuario.Focus();
        }
    }
}
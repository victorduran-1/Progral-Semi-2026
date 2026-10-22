using System;
using System.Data;
using System.Data.SqlClient;

namespace parcial2
{
    public class Conexion_base_datos
    {
        public SqlDataAdapter adaptadordatos = new SqlDataAdapter();
        public DataSet miDS = new DataSet();

        // Cadena de conexión para LocalDB
        private string cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Basededatos.mdf;Integrated Security=True;";

        // Método procesar real que ejecuta la sentencia SQL en la BD
        public string procesar(string sql, string accion)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql, conexion))
                    {
                        comando.ExecuteNonQuery();
                    }
                }
                return "Ok";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // Cargar los datos al DataSet
        public void crearDs()
        {
            string sql = "SELECT * FROM usuarios_USSS107526";

            using (SqlDataAdapter daUsuarios = new SqlDataAdapter(sql, cadenaConexion))
            {
                if (miDS == null)
                {
                    miDS = new DataSet();
                }

                if (miDS.Tables.Contains("usuarios_USSS107526"))
                {
                    miDS.Tables["usuarios_USSS107526"].Clear();
                }

                daUsuarios.Fill(miDS, "usuarios_USSS107526");
            }
        }

        // Lógica de mantenimiento
        public string mantenimiento_usuarios_USSS107526(string[] datos, string accion)
        {
            string sql = "";

            if (accion == "nuevo")
            {
                sql = $"INSERT INTO usuarios_USSS107526 (usuario, clave, nombre, direccion, telefono) " +
                      $"VALUES ('{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}')";
            }
            else if (accion == "modificar")
            {
                sql = $"UPDATE usuarios_USSS107526 SET " +
                      $"usuario='{datos[1]}', clave='{datos[2]}', nombre='{datos[3]}', " +
                      $"direccion='{datos[4]}', telefono='{datos[5]}' " +
                      $"WHERE idUsuario={datos[0]}";
            }
            else if (accion == "eliminar")
            {
                sql = $"DELETE FROM usuarios_USSS107526 WHERE idUsuario={datos[0]}";
            }

            return procesar(sql, accion);
        }
    }
}
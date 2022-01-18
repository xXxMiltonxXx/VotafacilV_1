using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace CRUD1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // botones para cerrar, maximizar y minimizar personalizados 
        //Cerrar
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Maximizar y luego de maximizar no sea visible
        // y el boton restaura si 

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;

        }
        // Restaurar se convierta en un boton no sea visible 
        // y el boton Maximizar si 
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;

        }
        //minimizar la ventana
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //usamos la libreria System.Runtime.InteropServices
        //para poder mover la ventana 

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);



        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        
        //Boton Guardar funciones del boton Guardar
        //Guardar en la base de datos
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                //funciones que va a tener a darle click 
                //recabamos en los datos de los controles

                String codigo = txtCodigo.Text;
                String nombre = txtNombre.Text;
                String descripcion = txtDescripcion.Text;
                //hacemos conversion ya que es de tipo double
                Double precio_publico = double.Parse(txtPrecioPublico.Text);
                //Hacemos conversion ya que las exixtencias son de tipo entero int
                //Agregamos condiciones para que no se pueda guardar casilleros vacios
                if (codigo != "" && nombre != "" && descripcion != "" && precio_publico > 0 )
                {
                    //Crear una variable  manejar una insercion
                    //Insercion a la base de datos
                    //insertamos datos a la tabla de productos
                    //y a que columas voy a ingresar
                    //concatenamos
                    string sql = "INSERT INTO productos(codigo, nombre,descripcion, precio_publico) VALUES('" + codigo
                        + "','" + nombre + "','" + descripcion + "','" + precio_publico + "')";
                    // traemos nuestracomexion MySQL
                    //Treamos a la clase Conexionen y su metodo conexion 
                    //Traemos la libreria de MySQL a esta clase para usar sus recursos
                    MySqlConnection conexionBD = Conexion.conexion();
                    //Abrimos la conexión
                    conexionBD.Open();
                    //en la clase conexion esta agrergado el try y catch por si hay errores 
                    //al momento de iniciar secion con el MySQL no es necesario
                    //no es necesario implementar un try y catch 
                    //pero
                    //Usamos try y catch para implementar
                    //Vamos a insertar archivos a las tablas del servidor
                    // evitar errores y que se deje de ejecutar el programa
                    try
                    {
                        //preparamos para que se pueda insertar los datos
                        //insercion el string sql y la conexion conexionBD 
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                        //ejecutamos el comando en Mysql
                        comando.ExecuteNonQuery();
                        //Enviamos un mensaje al usuario 
                        MessageBox.Show("Registro guardado");
                        //llamamos al metodo limpiar
                        limpiar();
                    }
                    //En caso de error o excepcion
                    catch (MySqlException ex)
                    {
                        //Enviamos el mensaje de error
                        MessageBox.Show("Error al guardar:" + ex.Message);
                    }
                    finally
                    {
                        //cerramos la conexion
                        //es necesario cerrarla
                        conexionBD.Close();
                    }
                    //Sino se cumple el if
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos");
                }
                //evitar errores en insertar formatos de escritura incorrectos en algun campo
            }catch(FormatException fex)
            {
                //mensaje de error
                MessageBox.Show("Datos incorrectos: " + fex.Message);
            }





        }
        //Boton Buscar Funciones del boton Buscar 
        //Buscar en la base de datos
        
        
        //Boton Eliminar Funciones del boton Eliminar
        //Eliminar datos de la base de datos
        //Es muy parecido al boton Actualizar
        
        //Botn Limpiar Funciones del boton Limpiar
        //Limpiar datos de 
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Llmanos al metodo limpiar
            limpiar();
        }
        //creamos un metodo private a para el boton limpiar
        private void limpiar()
        {
            txtId.Text = "";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecioPublico.Text = "";     
        }

        //mover la ventana a voluntad
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        
    }
}

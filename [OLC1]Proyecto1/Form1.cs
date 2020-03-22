using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _OLC1_Proyecto1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panel1.AutoScroll = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            expresion--;
            if (images.Count() > expresion && expresion>-1)
            {
                pictureBox1.Image = Picture.ElementAt(expresion);
                pictureBox2.Image = AFD.ElementAt(expresion);

            }
            else
            {
                expresion = images.Count()-1;
                pictureBox1.Image = Picture.ElementAt(expresion);
                pictureBox2.Image = AFD.ElementAt(expresion);
            }
        }


        OpenFileDialog ofd = new OpenFileDialog();
        private void button4_Click(object sender, EventArgs e)
        {
            ofd.Filter = "ER|*.er";
            string nombre = "";
            string nombreArchivo;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string ruta = ofd.FileName;
                nombre = ofd.SafeFileName;
                nombreArchivo = nombre;
                fastColoredTextBox1.Text = File.ReadAllText(ruta, Encoding.Default);
            }
        }
        LinkedList<Image> images;
        LinkedList<Image> AFD;
        LinkedList<Image> Tablas;
        LinkedList<Image> Picture;
        int expresion;
        private void button7_Click(object sender, EventArgs e)
        {
            AnalizadorLexico analizador = new AnalizadorLexico();
            LinkedList<Token> ltokens = analizador.analizar(fastColoredTextBox1.Text);
            LinkedList<Error> lErrores = analizador.getListaErrores();
            ReconecedorConjuntos reconecedorConjuntos = new ReconecedorConjuntos();
            //reconecedorConjuntos.analizar(ltokens);
           // ltokens = reconecedorConjuntos.getTokens();
          ArbolBinario arbolBinario = new ArbolBinario();
            LinkedList<Nodo> nodos = arbolBinario.generarLista(ltokens);
            images = arbolBinario.getImagenes();
            AFD = arbolBinario.getAFD();
            Tablas = arbolBinario.getTablas();
            Picture = images;
            pictureBox1.Image = images.ElementAt(0);
            pictureBox2.Image = AFD.ElementAt(0);
            
            generarXMLTokens(ltokens);
            generarAFN(nodos);
            expresion = 0;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void generarXMLTokens(LinkedList<Token> tokens) {
            string path = "archivo.xml";
            File.WriteAllText(path, "");


            if (File.Exists(path))
            {

                using (StreamWriter file = File.AppendText(path))
                {
                    file.WriteLine("<ListaTokens>");
                    foreach (Token item in tokens)
                    {
                        file.WriteLine(
                        "   <Token>\n"+
                        "       <Nombre>" + item.getToken() + "</Nombre>\n" +
                        "       <Valor>"+item.getValor()+"</Valor>\n" +
                        "       <Fila>" + item.getFila() + "</Fila>\n" +
                        "       <Columna>" + item.getColumna() + "</Columna>\n" +
                        "   </Token>\n");
                        
                    }
                    file.WriteLine("</ListaTokens>");
                    Console.WriteLine("si se modifico");
                    file.Close();
                }
            }
            /*Process p = new Process();
            p.StartInfo.FileName = path;
            p.Start();*/
        }

        private void generarXMLErrores(LinkedList<Error> errors)
        {
            string path = "errores.xml";
            File.WriteAllText(path, "");


            if (File.Exists(path))
            {

                using (StreamWriter file = File.AppendText(path))
                {
                    file.WriteLine("<ListaErrroes>");
                    foreach (Error item in errors)
                    {
                        file.WriteLine(
                        "   <Error>\n" +
                        "       <Valor>" + item.getToken() + "</Valor>\n" +
                        "       <Fila>" + item.getFila() + "</Fila>\n" +
                        "       <Columna>" + item.getColumna() + "</Columna>\n" +
                        "   </Error>\n");

                    }
                    file.WriteLine("</ListaErrores>");
                    Console.WriteLine("si se modifico");
                    file.Close();
                }
            }
            /*Process p = new Process();
            p.StartInfo.FileName = path;
            p.Start();*/
        }

        private void generarAFN(LinkedList<Nodo> nodos)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            expresion++;
            if (images.Count() > expresion) {
                pictureBox1.Image = Picture.ElementAt(expresion);
                pictureBox2.Image = AFD.ElementAt(expresion);
            }
            else
            {
                expresion=0;
                pictureBox1.Image = Picture.ElementAt(expresion);
                pictureBox2.Image = AFD.ElementAt(expresion);
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        bool cambio = true;
        private void button8_Click(object sender, EventArgs e)
        {
            if (cambio)
            {
                Picture = Tablas;
                pictureBox1.Image = Picture.ElementAt(expresion);
                cambio = false;
                groupBox3.Text = "Tabla de Transiciones";

            }
            else {
                Picture = images;
                pictureBox1.Image = Picture.ElementAt(expresion);
                cambio = true; 
                groupBox3.Text = "AFN";

            }

        }
    }


    
}

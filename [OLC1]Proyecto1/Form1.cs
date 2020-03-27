
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
            if (tabControl1.TabCount != 0)
            {
                ofd.Filter = "ER|*.er";
                string nombre = "";
                string nombreArchivo;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string ruta = ofd.FileName;
                    nombre = ofd.SafeFileName;
                    nombreArchivo = nombre;

                    int selectedTab = tabControl1.SelectedIndex;
                    Control ctrl = tabControl1.Controls[selectedTab].Controls[0];

                    tabControl1.Controls[selectedTab].Text = nombre;
                    RichTextBox rtb = ctrl as RichTextBox;
                    rtb.Text = File.ReadAllText(ruta, Encoding.Default);    
                }
            }
            else
            {
                MessageBox.Show("CREE UNA PESTAÑA NUEVA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        LinkedList<Image> images;
        LinkedList<Image> AFD;
        LinkedList<Image> Tablas;
        LinkedList<Image> Picture;
        LinkedList<Conjunto> conjuntos;
        int expresion;
        private void button7_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabCount != 0)
            {
                int selectedTab = tabControl1.SelectedIndex;
                Control ctrl = tabControl1.Controls[selectedTab].Controls[0];
                RichTextBox rtb = ctrl as RichTextBox;

                AnalizadorLexico analizador = new AnalizadorLexico();
                LinkedList<Token> ltokens = analizador.analizar(rtb.Text);
                LinkedList<Error> lErrores = analizador.getListaErrores();
                AnalizadorSintactico analizadorSintactico = new AnalizadorSintactico();
                analizadorSintactico.parsear(ltokens, lErrores);
                lErrores = analizadorSintactico.GetErrors();
                if (lErrores.Count == 0)
                {
                    Console.WriteLine("Jalo al 100");

                    ReconecedorConjuntos reconecedorConjuntos = new ReconecedorConjuntos();
                    conjuntos = reconecedorConjuntos.analizar(ltokens);
                    ltokens = reconecedorConjuntos.getTokens();
                    ArbolBinario arbolBinario = new ArbolBinario();
                    LinkedList<Nodo> nodos = arbolBinario.generarLista(ltokens);
                    images = arbolBinario.getImagenes();
                    AFD = arbolBinario.getAFD();
                    Tablas = arbolBinario.getTablas();
                    Picture = images;
                    pictureBox1.Image = images.ElementAt(0);
                    pictureBox2.Image = AFD.ElementAt(0);
                    ValidarExpresion validarExpresion = new ValidarExpresion();
                    validarExpresion.validarExpresiones(ltokens, ArbolBinario.lMueves, conjuntos, arbolBinario.getFin(), arbolBinario.getNombres());
                    if (validarExpresion.getValidaciones() != "")
                    {
                        richTextBox1.Text = "\n" + validarExpresion.getValidaciones();
                    }
                    else
                    {
                        richTextBox1.Text = "\n\tNo hay lexemas validos";
                    }
                    generarXMLTokens(ltokens);
                    expresion = 0;
                }
                else
                {

                    generarXMLErrores(lErrores);
                    MessageBox.Show("ERROR DE ENTRADA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("CREE UNA PESTAÑA NUEVA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void generarXMLTokens(LinkedList<Token> tokens) {
            string path = "archivo.xml";
            File.WriteAllText(path, "");

            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream("reporte.pdf", FileMode.Create));
            doc.Open();
            iTextSharp.text.Paragraph titulo = new iTextSharp.text.Paragraph();
            titulo.Font = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 18f, iTextSharp.text.BaseColor.ORANGE);
            titulo.Add("REPORTE DE TOKENS");
            doc.Add(titulo);
            doc.Add(new iTextSharp.text.Paragraph("\n"));

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            doc.Add(new iTextSharp.text.Paragraph(""));

            iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(4);

            table.AddCell("Tokens");
            table.AddCell("Lexema");
            table.AddCell("Fila");
            table.AddCell("Columa");



            if (File.Exists(path))
            {

                using (StreamWriter file = File.AppendText(path))
                {
                    file.WriteLine("<ListaTokens>");
                    foreach (Token item in tokens)
                    {
                        table.AddCell(item.getToken());
                        table.AddCell(item.getValor());
                        table.AddCell(item.getFila().ToString());
                        table.AddCell(item.getColumna().ToString());
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
                doc.Add(table);
                doc.Close();
            }
            /*Process p = new Process();
            p.StartInfo.FileName = path;
            p.Start();*/
        }

        private void generarXMLErrores(LinkedList<Error> errors)
        {
            string path = "errores.xml";
            File.WriteAllText(path, "");

            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream("errores.pdf", FileMode.Create));
            doc.Open();
            iTextSharp.text.Paragraph titulo = new iTextSharp.text.Paragraph();
            titulo.Font = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 18f, iTextSharp.text.BaseColor.ORANGE);
            titulo.Add("REPORTE DE ERRORES");
            doc.Add(titulo);
            doc.Add(new iTextSharp.text.Paragraph("\n"));

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            doc.Add(new iTextSharp.text.Paragraph(""));

            iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(4);

            table.AddCell("Error");
            table.AddCell("Fila");
            table.AddCell("Columna");

            if (File.Exists(path))
            {

                using (StreamWriter file = File.AppendText(path))
                {
                    file.WriteLine("<ListaErrores>");
                    foreach (Error item in errors)
                    {
                        table.AddCell(item.getToken());                        
                        table.AddCell(""+item.getFila());
                        table.AddCell("" + item.getColumna());

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
                doc.Add(table);
                doc.Close();
            }
            /*Process p = new Process();
            p.StartInfo.FileName = path;
            p.Start();*/
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

        private void button6_Click(object sender, EventArgs e)
        {

            TabPage page = new TabPage();

            tabControl1.TabPages.Add(page);
            RichTextBox rtb = new RichTextBox();
            rtb.Font = new Font("Courier New", 10, FontStyle.Regular);
            rtb.BorderStyle = BorderStyle.None;
            rtb.Dock = DockStyle.Fill;
            rtb.WordWrap = false;
            rtb.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            tabControl1.SelectTab(page);
            tabControl1.SelectedTab.Controls.Add(rtb);


            int x = (tabControl1.TabCount);
            tabControl1.SelectedTab.Text = "Pestaña " + x;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabCount == 0)
            {
                MessageBox.Show("Click on button1 to create a new tab.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();

                if (folder.ShowDialog() == DialogResult.OK)
                {
                    int selectedTab = tabControl1.SelectedIndex;
                    Control ctrl = tabControl1.Controls[selectedTab].Controls[0];
                    RichTextBox rtb = ctrl as RichTextBox;
                    string cadena = rtb.Text;
                    string ruta = folder.SelectedPath;
                    string nombre = InputKey.InputDialog.mostrar("Ingrese el nombre del archivo");
                    
                    using (StreamWriter file = new StreamWriter(ruta + "\\" + nombre + ".er"))
                    {
                        file.Write(cadena);
                    }
                    Console.WriteLine(ruta);

                    tabControl1.SelectedTab.Text = nombre + ".er";
                    MessageBox.Show("Archivo guardado con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "reporte.pdf";
            p.Start();


            p.StartInfo.FileName = "archivo.xml";
            p.Start();


            p.StartInfo.FileName = "errores.pdf";
            p.Start();


            p.StartInfo.FileName = "errores.xml";
            p.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }


    
}

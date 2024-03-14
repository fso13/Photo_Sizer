// Decompiled with JetBrains decompiler
// Type: Photo_Sizer.Form1
// Assembly: Photo_Sizer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9B3EADE7-A949-4521-8B64-665FCD02EB88
// Assembly location: C:\Users\dimar\OneDrive\Рабочий стол\Photo_Sizer.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Photo_Sizer
{
    public class Form1 : Form
    {
        private ImageFormat im;
        private string raz;
        private int width;
        private int height;
        private int[,] mas = new int[2, 6];
        private string path = (string)null;
        private Bitmap foto;
        private FileInfo info;
        private IContainer components = (IContainer)null;
        private ListBox lb_selected_files;
        private PictureBox pictureBox1;
        private TextBox tb_selected_directory_save;
        private Button bt_start_proccess;
        private Button bt_add_file;
        private Button bt_add_directory;
        private ComboBox cb_size;
        private FolderBrowserDialog folderBrowserDialog1;
        private OpenFileDialog openFileDialog1;
        private FolderBrowserDialog folderBrowserDialog2;
        private ProgressBar progressBar1;
        private ComboBox cb_file_format;
        private NumericUpDown nud_zoom;
        private CheckBox chb_is_size;
        private CheckBox chb_is_zoom;
        private Label label1;
        private Label label2;
        private Button bt_select_directory_save;
        private Label label3;
        private CheckBox chb_is_replace;
        private TextBox tb_file_pattern;
        private NotifyIcon notifyIcon1;

        public Form1()
        {
            this.InitializeComponent();
            this.pictureBox1.AllowDrop = true;
            this.cb_size.SelectedIndex = 0;
            this.cb_file_format.SelectedIndex = 0;
            this.mas[0, 0] = 320;
            this.mas[1, 0] = 200;
            this.mas[0, 1] = 640;
            this.mas[1, 1] = 480;
            this.mas[0, 2] = 800;
            this.mas[1, 2] = 600;
            this.mas[0, 3] = 1024;
            this.mas[1, 3] = 768;
            this.mas[0, 4] = 1200;
            this.mas[1, 4] = 900;
            this.mas[0, 5] = 1600;
            this.mas[1, 5] = 1200;
        }

        internal void AddFilesFromDir(string dir)
        {
            foreach (FileInfo file in new DirectoryInfo(dir).GetFiles("*.*"))
            {
                if (file.Extension.Equals(".jpg") || file.Extension.Equals(".jpe") || file.Extension.Equals(".jpeg") || file.Extension.Equals(".png") || file.Extension.Equals(".tif") || file.Extension.Equals(".tiff") || file.Extension.Equals(".gif") || file.Extension.Equals(".bmp") || file.Extension.Equals(".JPG") || file.Extension.Equals(".JPE") || file.Extension.Equals(".JPEG") || file.Extension.Equals(".PNG") || file.Extension.Equals(".TIF") || file.Extension.Equals(".TIFF") || file.Extension.Equals(".GIF") || file.Extension.Equals(".BMP"))
                {
                    this.lb_selected_files.Items.Add((object)new Form1.FileListItem(file.FullName));

                }
            }
            foreach (DirectoryInfo directoryInfo in new DirectoryInfo(dir).GetDirectories())
            {
                AddFilesFromDir(directoryInfo.FullName);
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (this.lb_selected_files.Items.Count == 0 || this.lb_selected_files.SelectedIndex == -1)
                return;
            ShowImageInfo();
        }

        private void ShowImageInfo()
        {
            this.pictureBox1.ImageLocation = ((Form1.FileListItem)this.lb_selected_files.SelectedItem).fullFilename;
            this.info = new FileInfo(((Form1.FileListItem)this.lb_selected_files.SelectedItem).fullFilename);
            this.foto = new Bitmap(((Form1.FileListItem)this.lb_selected_files.SelectedItem).fullFilename);
            double num1 = Math.Round(Convert.ToDouble(this.info.Length) / 1048576.0, 3);
            Label label2 = this.label2;
            string[] strArray1 = new string[7]
            {
        ((Form1.FileListItem) this.lb_selected_files.SelectedItem).shortFilename,
        "   ( размер ",
        Convert.ToString(num1),
        " МВ )",
        null,
        null,
        null
            };
            string[] strArray2 = strArray1;
            int num2 = this.foto.Width;
            string str1 = num2.ToString();
            strArray2[4] = str1;
            strArray1[5] = "x";
            string[] strArray3 = strArray1;
            num2 = this.foto.Height;
            string str2 = num2.ToString();
            strArray3[6] = str2;
            string str3 = string.Concat(strArray1);
            label2.Text = str3;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            FileInfo fileInfo = new FileInfo(data[0]);
            if (fileInfo.Extension.Equals(".jpg") || fileInfo.Extension.Equals(".jpe") || fileInfo.Extension.Equals(".jpeg") || fileInfo.Extension.Equals(".png") || fileInfo.Extension.Equals(".tif") || fileInfo.Extension.Equals(".tiff") || fileInfo.Extension.Equals(".gif") || fileInfo.Extension.Equals(".bmp") || fileInfo.Extension.Equals(".JPG") || fileInfo.Extension.Equals(".JPE") || fileInfo.Extension.Equals(".JPEG") || fileInfo.Extension.Equals(".PNG") || fileInfo.Extension.Equals(".TIF") || fileInfo.Extension.Equals(".TIFF") || fileInfo.Extension.Equals(".GIF") || fileInfo.Extension.Equals(".BMP"))
                this.lb_selected_files.Items.Add((object)new Form1.FileListItem(data[0]));
            if (fileInfo.Exists)
                return;
            this.AddFilesFromDir(data[0]);
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.path = this.folderBrowserDialog1.SelectedPath;
            this.tb_selected_directory_save.Text = this.path;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "image files (*.jpeg;*.bmp;*.tiff;*.png;*.tif;*.gif;*.ico)|*.jpeg;*.jpg;*.jpe;*.bmp;*.tiff;*.png;*.tif;*.gif;*.JPEG;*.JPG;*.JPE;*.BMP;*.TIFF;*.PNG;*.TIF;*.GIF;*.ico;*.ICO";
            if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.lb_selected_files.Items.Add((object)new Form1.FileListItem(this.openFileDialog1.FileName));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.AddFilesFromDir(this.folderBrowserDialog1.SelectedPath);
        }

        internal void ResizeImage(FileListItem fn)
        {
            Bitmap original = new Bitmap(fn.fullFilename);
            int width;
            int height;
            if ((double)this.width / (double)original.Width < (double)this.height / (double)original.Height)
            {
                width = this.width;
                height = (int)((double)original.Height * ((double)this.width / (double)original.Width));
            }
            else
            {
                height = this.height;
                width = (int)((double)original.Width * ((double)this.height / (double)original.Height));
            }
            Bitmap bitmap = new Bitmap((Image)original, width, height);
            string newPath = renderFileName(fn);
            if(!new DirectoryInfo(Path.GetDirectoryName(newPath)).Exists)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(newPath));
            }
            bitmap.Save(newPath, this.im);
            bitmap.Dispose();
        }

        private string renderFileName(FileListItem fn)
        {
            string newPath = chb_is_replace.Checked ? Path.GetDirectoryName(fn.fullFilename) : this.path;
            string newFileName;
            if (chb_is_replace.Checked)
            {
                newFileName = Path.GetFileNameWithoutExtension(fn.shortFilename);
            }
            else
            {

                newFileName = tb_file_pattern.Text
                    .Replace("{name}", Path.GetFileNameWithoutExtension(fn.shortFilename))
                    .Replace("{datetime}", DateTime.Now.ToString(CultureInfo.GetCultureInfo("ru-RU")))
                    .Replace("{dir}", Directory.GetParent(fn.fullFilename).Name)
                    .Replace("{size}", width.ToString() + "x" + height.ToString())
                    .Replace(":","-")
                    .Replace(".","-")
                    
                    ;
            }

            newPath = newPath + "\\" + newFileName + "." + this.raz;
            return newPath;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.tb_selected_directory_save.Text != "" || chb_is_replace.Checked)
            {
                chb_is_replace.Enabled = false;
                if (this.cb_file_format.SelectedItem.ToString() == "jpeg" || this.cb_file_format.SelectedItem.ToString() == "jpe" || this.cb_file_format.SelectedItem.ToString() == "jpg")
                {
                    this.im = ImageFormat.Jpeg;
                    this.raz = "jpg";
                }
                if (this.cb_file_format.SelectedItem.ToString() == "bmp")
                {
                    this.im = ImageFormat.Bmp;
                    this.raz = "bmp";
                }
                if (this.cb_file_format.SelectedItem.ToString() == "gif")
                {
                    this.im = ImageFormat.Gif;
                    this.raz = "gif";
                }
                if (this.cb_file_format.SelectedItem.ToString() == "tiff" || this.cb_file_format.SelectedItem.ToString() == "tif")
                {
                    this.im = ImageFormat.Tiff;
                    this.raz = "tiff";
                }
                if (this.cb_file_format.SelectedItem.ToString() == "icon")
                {
                    this.im = ImageFormat.Icon;
                    this.raz = "ico";
                }
                this.progressBar1.Value = 0;
                this.progressBar1.Maximum = this.lb_selected_files.Items.Count;
                for (int index = 0; index < this.lb_selected_files.Items.Count; ++index)
                {
                    if (this.chb_is_size.Checked)
                    {
                        this.width = this.mas[0, this.cb_size.SelectedIndex];
                        this.height = this.mas[1, this.cb_size.SelectedIndex];
                    }
                    else
                    {
                        Bitmap bitmap = new Bitmap(((Form1.FileListItem)this.lb_selected_files.Items[index]).fullFilename);
                        this.width = bitmap.Width * Convert.ToInt32(this.nud_zoom.Value) / 100;
                        this.height = bitmap.Height * Convert.ToInt32(this.nud_zoom.Value) / 100;
                    }
                    this.ResizeImage(((Form1.FileListItem)this.lb_selected_files.Items[index]));
                    ++this.progressBar1.Value;
                }
                int num = (int)MessageBox.Show("Операция выполнена,изменено " + this.lb_selected_files.Items.Count.ToString() + " файлов");
                chb_is_replace.Enabled = true;
            }
            else
            {
                int num1 = (int)MessageBox.Show("Выберете путь для сохранения");
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            this.chb_is_zoom.Checked = false;
            this.chb_is_size.Checked = true;
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            this.chb_is_zoom.Checked = true;
            this.chb_is_size.Checked = false;
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete || this.lb_selected_files.SelectedIndex == -1)
                return;
            this.lb_selected_files.Items.RemoveAt(this.lb_selected_files.SelectedIndex);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lb_selected_files = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tb_selected_directory_save = new System.Windows.Forms.TextBox();
            this.bt_start_proccess = new System.Windows.Forms.Button();
            this.bt_add_file = new System.Windows.Forms.Button();
            this.bt_add_directory = new System.Windows.Forms.Button();
            this.cb_size = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cb_file_format = new System.Windows.Forms.ComboBox();
            this.nud_zoom = new System.Windows.Forms.NumericUpDown();
            this.chb_is_size = new System.Windows.Forms.CheckBox();
            this.chb_is_zoom = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_select_directory_save = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.chb_is_replace = new System.Windows.Forms.CheckBox();
            this.tb_file_pattern = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_zoom)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_selected_files
            // 
            this.lb_selected_files.AllowDrop = true;
            this.lb_selected_files.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_selected_files.FormattingEnabled = true;
            this.lb_selected_files.ItemHeight = 16;
            this.lb_selected_files.Location = new System.Drawing.Point(13, 41);
            this.lb_selected_files.Margin = new System.Windows.Forms.Padding(4);
            this.lb_selected_files.Name = "lb_selected_files";
            this.lb_selected_files.Size = new System.Drawing.Size(198, 354);
            this.lb_selected_files.TabIndex = 0;
            this.lb_selected_files.Click += new System.EventHandler(this.listBox1_Click);
            this.lb_selected_files.SelectedIndexChanged += new System.EventHandler(this.lb_selected_files_SelectedIndexChanged);
            this.lb_selected_files.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            this.lb_selected_files.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);
            this.lb_selected_files.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(227, 43);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(421, 305);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tb_selected_directory_save
            // 
            this.tb_selected_directory_save.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_selected_directory_save.Enabled = false;
            this.tb_selected_directory_save.Location = new System.Drawing.Point(405, 373);
            this.tb_selected_directory_save.Margin = new System.Windows.Forms.Padding(4);
            this.tb_selected_directory_save.Name = "tb_selected_directory_save";
            this.tb_selected_directory_save.Size = new System.Drawing.Size(427, 22);
            this.tb_selected_directory_save.TabIndex = 4;
            // 
            // bt_start_proccess
            // 
            this.bt_start_proccess.Location = new System.Drawing.Point(227, 416);
            this.bt_start_proccess.Margin = new System.Windows.Forms.Padding(4);
            this.bt_start_proccess.Name = "bt_start_proccess";
            this.bt_start_proccess.Size = new System.Drawing.Size(163, 28);
            this.bt_start_proccess.TabIndex = 5;
            this.bt_start_proccess.Text = "изменить";
            this.bt_start_proccess.UseVisualStyleBackColor = true;
            this.bt_start_proccess.Click += new System.EventHandler(this.button1_Click);
            // 
            // bt_add_file
            // 
            this.bt_add_file.Location = new System.Drawing.Point(120, 402);
            this.bt_add_file.Margin = new System.Windows.Forms.Padding(4);
            this.bt_add_file.Name = "bt_add_file";
            this.bt_add_file.Size = new System.Drawing.Size(92, 42);
            this.bt_add_file.TabIndex = 6;
            this.bt_add_file.Text = "Добавить файл";
            this.bt_add_file.UseVisualStyleBackColor = true;
            this.bt_add_file.Click += new System.EventHandler(this.button2_Click);
            // 
            // bt_add_directory
            // 
            this.bt_add_directory.Location = new System.Drawing.Point(13, 402);
            this.bt_add_directory.Margin = new System.Windows.Forms.Padding(4);
            this.bt_add_directory.Name = "bt_add_directory";
            this.bt_add_directory.Size = new System.Drawing.Size(99, 42);
            this.bt_add_directory.TabIndex = 7;
            this.bt_add_directory.Text = "Добавить папку";
            this.bt_add_directory.UseVisualStyleBackColor = true;
            this.bt_add_directory.Click += new System.EventHandler(this.button3_Click);
            // 
            // cb_size
            // 
            this.cb_size.FormattingEnabled = true;
            this.cb_size.Items.AddRange(new object[] {
            "320x200",
            "640x480",
            "800x600",
            "1024x768",
            "1200x900",
            "1600x1200"});
            this.cb_size.Location = new System.Drawing.Point(664, 43);
            this.cb_size.Margin = new System.Windows.Forms.Padding(4);
            this.cb_size.Name = "cb_size";
            this.cb_size.Size = new System.Drawing.Size(168, 24);
            this.cb_size.TabIndex = 10;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(405, 415);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(427, 28);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 13;
            // 
            // cb_file_format
            // 
            this.cb_file_format.FormattingEnabled = true;
            this.cb_file_format.Items.AddRange(new object[] {
            "bmp",
            "jpeg",
            "tiff",
            "gif",
            "png",
            "icon"});
            this.cb_file_format.Location = new System.Drawing.Point(664, 207);
            this.cb_file_format.Margin = new System.Windows.Forms.Padding(4);
            this.cb_file_format.Name = "cb_file_format";
            this.cb_file_format.Size = new System.Drawing.Size(168, 24);
            this.cb_file_format.TabIndex = 14;
            // 
            // nud_zoom
            // 
            this.nud_zoom.Location = new System.Drawing.Point(664, 121);
            this.nud_zoom.Margin = new System.Windows.Forms.Padding(4);
            this.nud_zoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_zoom.Name = "nud_zoom";
            this.nud_zoom.Size = new System.Drawing.Size(65, 22);
            this.nud_zoom.TabIndex = 15;
            this.nud_zoom.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // chb_is_size
            // 
            this.chb_is_size.AutoSize = true;
            this.chb_is_size.Checked = true;
            this.chb_is_size.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_is_size.Location = new System.Drawing.Point(664, 15);
            this.chb_is_size.Margin = new System.Windows.Forms.Padding(4);
            this.chb_is_size.Name = "chb_is_size";
            this.chb_is_size.Size = new System.Drawing.Size(103, 20);
            this.chb_is_size.TabIndex = 16;
            this.chb_is_size.Text = "Пропорции";
            this.chb_is_size.UseVisualStyleBackColor = true;
            this.chb_is_size.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // chb_is_zoom
            // 
            this.chb_is_zoom.AutoSize = true;
            this.chb_is_zoom.Location = new System.Drawing.Point(664, 92);
            this.chb_is_zoom.Margin = new System.Windows.Forms.Padding(4);
            this.chb_is_zoom.Name = "chb_is_zoom";
            this.chb_is_zoom.Size = new System.Drawing.Size(95, 20);
            this.chb_is_zoom.TabIndex = 17;
            this.chb_is_zoom.Text = "Проценты";
            this.chb_is_zoom.UseVisualStyleBackColor = true;
            this.chb_is_zoom.Click += new System.EventHandler(this.checkBox2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(661, 175);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Формат выходного файла";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "Информация о файле";
            // 
            // bt_select_directory_save
            // 
            this.bt_select_directory_save.Location = new System.Drawing.Point(227, 370);
            this.bt_select_directory_save.Margin = new System.Windows.Forms.Padding(4);
            this.bt_select_directory_save.Name = "bt_select_directory_save";
            this.bt_select_directory_save.Size = new System.Drawing.Size(163, 27);
            this.bt_select_directory_save.TabIndex = 8;
            this.bt_select_directory_save.Text = "путь для сохранения";
            this.bt_select_directory_save.UseVisualStyleBackColor = true;
            this.bt_select_directory_save.Click += new System.EventHandler(this.button4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 16);
            this.label3.TabIndex = 22;
            this.label3.Text = "Список файлов";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "Fotoizer";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // chb_is_replace
            // 
            this.chb_is_replace.AutoSize = true;
            this.chb_is_replace.Location = new System.Drawing.Point(664, 248);
            this.chb_is_replace.Margin = new System.Windows.Forms.Padding(4);
            this.chb_is_replace.Name = "chb_is_replace";
            this.chb_is_replace.Size = new System.Drawing.Size(140, 20);
            this.chb_is_replace.TabIndex = 23;
            this.chb_is_replace.Text = "Заменить файлы";
            this.chb_is_replace.UseVisualStyleBackColor = true;
            // 
            // tb_file_pattern
            // 
            this.tb_file_pattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_file_pattern.Location = new System.Drawing.Point(664, 290);
            this.tb_file_pattern.Margin = new System.Windows.Forms.Padding(4);
            this.tb_file_pattern.Name = "tb_file_pattern";
            this.tb_file_pattern.Size = new System.Drawing.Size(170, 22);
            this.tb_file_pattern.TabIndex = 24;
            this.tb_file_pattern.Text = "{dir}\\{name}-{datetime}";
            this.tb_file_pattern.TextChanged += new System.EventHandler(this.tb_file_pattern_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(839, 453);
            this.Controls.Add(this.tb_file_pattern);
            this.Controls.Add(this.chb_is_replace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chb_is_zoom);
            this.Controls.Add(this.chb_is_size);
            this.Controls.Add(this.nud_zoom);
            this.Controls.Add(this.cb_file_format);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.cb_size);
            this.Controls.Add(this.bt_select_directory_save);
            this.Controls.Add(this.bt_add_directory);
            this.Controls.Add(this.bt_add_file);
            this.Controls.Add(this.bt_start_proccess);
            this.Controls.Add(this.tb_selected_directory_save);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lb_selected_files);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(857, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Изменение размера картинки";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_zoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal class FileListItem
        {
            internal string fullFilename;
            internal string shortFilename;

            internal FileListItem()
            {
                this.fullFilename = "";
                this.shortFilename = "";
            }

            internal FileListItem(string inFull, string inShort)
            {
                this.fullFilename = inFull;
                this.shortFilename = inShort;
            }

            internal FileListItem(string inFull)
            {
                this.fullFilename = inFull;
                this.shortFilename = Path.GetFileName(inFull);
            }

            public override string ToString() => this.shortFilename;
        }

        private void lb_selected_files_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowImageInfo();
        }

        private void tb_file_pattern_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using System.Diagnostics;

namespace Sort_file
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            string date = Convert.ToString(DateTime.Today).Substring(0, 10);
            InitializeComponent();
            AutoCompleteStringCollection source = new AutoCompleteStringCollection()
            {
                date,
                "Отсортированное",
            };
            textBox1.AutoCompleteCustomSource = source;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.Text = date;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(FBD.SelectedPath);
				Sort_Files(FBD.SelectedPath);
            }
        }

		private void Sort_Files(string path)
		{
            int amount = Convert.ToInt32(textBox2.Text);
			string[] files = Directory.GetFiles(path, "*_*_(*", SearchOption.AllDirectories);
            if (checkBox1.Checked == true)
                path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			for (int i = 0; i < files.Length; i++)
			{
				string file_name = files[i];
                string path_local = Path.Combine(path, textBox1.Text);
				string fileName = Path.GetFileName(file_name);
				string text = file_name.Substring(file_name.IndexOf("_(") - 3, 3);
				if (text.IndexOf("_") != -1)
				{
					text = text.Substring(text.IndexOf("_") + 1);
				}

				path_local = Path.Combine(path_local, text);
				if (!Directory.Exists(path_local))
				{ 
					Directory.CreateDirectory(path_local);
				}
                else if (amount > 0)
                {
                    path_local = check_amount(path_local, amount);
                }

                path_local = Path.Combine(path_local, fileName);
                if (!File.Exists(path_local))
                    File.Copy(file_name, path_local);
			}
            open_folder(Path.Combine(path, textBox1.Text));
        }

        private string check_amount(string path, int amount, int tag = 1)
        {
            if (Directory.GetFiles(path).Length >= amount)
            {
                if (tag != 1)
                    path = path.Replace($"_{tag-1}", $"_{tag}");
                else
                    path += $"_{tag}";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                else
                    path = check_amount(path, amount, tag+1);
            }

            return path;
        }

        private void open_folder(string path)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            }
            );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = Convert.ToString(DateTime.Today);
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + textBox1.Text);
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        
    }
}
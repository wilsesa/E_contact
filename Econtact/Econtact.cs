using Econtact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();

        private void Econtact_Load(object sender, EventArgs e)
        {
            //Carregar dados em DataGridView
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //  Obter o valor dos campos de imput
            c.FirstName = txtBoxFirstName.Text;
            c.LastName = txtBoxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            //Inserindo dados no banco de dados usando o método que criamos no episódio anterior
            bool success = c.Insert(c);
            if (success == true)
            {
                //Inserido com sucesso
                MessageBox.Show("Novo contato inserido com sucesso", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //chame o método para limpar campos aqui
                Clear();
            }
            else
            {
                //Falha ao adicionar contato
                MessageBox.Show("Falha ao adicionar novo contato.tente novamente!", "FALHA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Carregar dados em DataGridView
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }
        //Método para limpar campos
        public void Clear()
        {
            txtBoxContactID.Clear();
            txtBoxFirstName.Clear();
            txtBoxLastName.Clear();
            txtBoxContactNumber.Clear();
            txtBoxAddress.Clear();
            cmbGender.Text = "";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Obter os dados de textbox
            c.ContactID = Convert.ToInt32(txtBoxContactID.Text);
            c.FirstName = txtBoxFirstName.Text;
            c.LastName = txtBoxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;
            //(Update) Atualizar dados no banco de dados
            bool success = c.Update(c);
            if (success == true)
            {
                //Atualizado com sucesso
                MessageBox.Show("O contato foi atualizado com sucesso.", "ATUALIZADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Carregar dados em DataGridView
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //Chamar método Clear
                Clear();
            }
            else
            {
                //Falha ao atualizar
                MessageBox.Show("Falha ao atualizar o contato, tente novamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Obtenha os dados da exibição do DataGridView e carregue-os nas caixas de texto, respectivamente.
            //Identifique a linha no mouse em que clicou
            int rowIndex = e.RowIndex;
            txtBoxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtBoxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtBoxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Chame o método Clear
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Obter dados das caixas de texto
            c.ContactID = Convert.ToInt32(txtBoxContactID.Text);
            bool success = c.Delete(c);
            if (success == true)
            {
                //Excluído com sucesso
                MessageBox.Show("Contato excluído com sucesso");
                //Refresh Data GridView
                //Carregar dados em DataGridView
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //Chame o método Clear
                Clear();
            }
            else
            {
                //Falha ao excluir
                MessageBox.Show("Falha ao excluir o contato. Tente novamente!");
            }
        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            //Obter o valor de tex: box
            string keyword = txtBoxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%"+keyword+"%' OR LastName LIKE '%"+keyword+"%' OR Address LIKE '%"+keyword+"%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}

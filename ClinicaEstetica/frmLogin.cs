using ClinicaEstetica.DLL;
using ClinicaEstetica.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaEstetica
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string senha = txtSenha.Text;

            UsuarioDTO usuarioDTO = new UsuarioDTO();
            UsuarioBLL uSuarioBLL = new UsuarioBLL();

            usuarioDTO = uSuarioBLL.AutenticarUsuario(email, senha);

            if (usuarioDTO != null)
            {
                MessageBox.Show($"Bem vindo(a) {usuarioDTO.Nome}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmUsuario frmUsuario = new frmUsuario();
                frmUsuario.Show();
            }
            else
            {
                MessageBox.Show($"Não foi possivel efetuar o login, tente novamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}

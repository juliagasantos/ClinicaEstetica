using ClinicaEstetica.DAL;
using ClinicaEstetica.DTO;

namespace ClinicaEstetica.DLL
{
    public class UsuarioBLL
    {
        UsuarioDAL usuarioDAL = new UsuarioDAL();

        public UsuarioDTO AutenticarUsuario(string email, string senha)
        {
            return usuarioDAL.Autenticar(email, senha);
        }
    }
}
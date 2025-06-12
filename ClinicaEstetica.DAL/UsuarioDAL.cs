using ClinicaEstetica.DTO;
using System;
using System.Data.SqlClient;

namespace ClinicaEstetica.DAL
{
    public class UsuarioDAL:Conexao
    {
        public UsuarioDTO Autenticar(string email, string senha)
        {
            try
            {
                Conectar();
                command = new SqlCommand("SELECT * FROM Usuario WHERE Email = @Email AND Senha = @Senha;", connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Senha", senha);
                dataReader = command.ExecuteReader();


                UsuarioDTO usuario = null;

                if (dataReader.Read())
                {
                    usuario = new UsuarioDTO();
                    usuario.IdTipoUsuario = int.Parse(dataReader["IdTipoUsuario"].ToString());
                    usuario.Nome = dataReader["Nome"].ToString();
                    usuario.Email = dataReader["Email"].ToString();
                    usuario.Senha = dataReader["Senha"].ToString();
                    usuario.Status = bool.Parse(dataReader["status"].ToString());

                    return usuario;
                }
            }
            catch (Exception error)
            {
                throw new Exception($"Erro: {error.Message}");
            }
            finally
            {
                Desconectar();
            }
        }
    }
}

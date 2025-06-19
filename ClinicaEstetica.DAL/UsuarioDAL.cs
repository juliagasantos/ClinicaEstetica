using ClinicaEstetica.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClinicaEstetica.DAL
{
    public class UsuarioDAL : Conexao
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
                }

                return usuario;
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

        public List<UsuarioDTO> ListarTodos()
        {
            List<UsuarioDTO> usuarios = new List<UsuarioDTO>();

            try
            {
                Conectar();
                string sql = "SELECT * FROM Usuario ORDER BY Nome";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarios.Add(new UsuarioDTO()
                        {
                            IdUsuario = (int)reader["IdUsuario"],
                            Nome = reader["Nome"].ToString(),
                            Email = reader["Email"].ToString(),
                            Senha = reader["Senha"].ToString(),
                            Status = (bool)reader["Status"],
                            IdTipoUsuario = (int)reader["IdTipoUsuario"]
                        });
                    }
                }

            }
            catch (Exception error)
            {
                throw new Exception($"Erro: {error.Message}");
            }

            return usuarios;
        }

        public List<TipoUsuarioDTO> GetTipos()
        {
            try
            {
                Conectar();
                string sql = "SELECT * FROM TipoUsuario;";
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                List<TipoUsuarioDTO> lista = new List<TipoUsuarioDTO>();
                while (dataReader.Read())
                {
                    TipoUsuarioDTO tipoUsuario = new TipoUsuarioDTO();
                    tipoUsuario.IdTipoUsuario = Convert.ToInt32(dataReader["IdTipoUsuario"]);
                    tipoUsuario.Nome = dataReader["Nome"].ToString();
                    lista.Add(tipoUsuario);
                }

                return lista;
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

        public void Create(UsuarioDTO usuario)
        {
            try
            {
                Conectar();
                string sql = "INSERT INTO Usuario (IdTipoUsuario, Nome, Email, Senha, Status) VALUES (@idTipo, @nome, @email, @senha, @status)";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@idTipo", usuario.IdTipoUsuario);
                command.Parameters.AddWithValue("@nome", usuario.Nome);
                command.Parameters.AddWithValue("@email", usuario.Email);
                command.Parameters.AddWithValue("@senha", usuario.Senha);
                command.Parameters.AddWithValue("@status", usuario.Status);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar usuário: {ex.Message}");
            }
        }

        public void Update(UsuarioDTO usuario)
        {
            try
            {
                Conectar();
                string sql = "UPDATE Usuario SET IdTipoUsuario=@idTipo, Nome=@nome, Email=@email, Senha=@senha, Status=@status WHERE IdUsuario=@idUsuario";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@idTipo", usuario.IdTipoUsuario);
                command.Parameters.AddWithValue("@nome", usuario.Nome);
                command.Parameters.AddWithValue("@email", usuario.Email);
                command.Parameters.AddWithValue("@senha", usuario.Senha);
                command.Parameters.AddWithValue("@status", usuario.Status);
                command.Parameters.AddWithValue("@idUsuario", usuario.IdUsuario);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar usuário: {ex.Message}");
            }
        }

        public UsuarioDTO Pesquisar(int id)
        {
            try
            {
                Conectar();
                string sql = "SELECT * FROM Usuario WBERE idUsuario=@idUsuario";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@idUsuario", id);
                dataReader = command.ExecuteReader();
                UsuarioDTO usuarioDTO = null;
                if (dataReader.Read())
                {
                    usuarioDTO = new UsuarioDTO();
                    usuarioDTO.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                    usuarioDTO.Nome = dataReader["Nome"].ToString();
                    usuarioDTO.Email = dataReader["Email"].ToString();
                    usuarioDTO.Senha = dataReader["Senha"].ToString();
                    usuarioDTO.Status = Convert.ToBoolean(dataReader["Status"]);
                    usuarioDTO.IdTipoUsuario = Convert.ToInt32(dataReader["IdTipoUsuario"]);
                }

                return usuarioDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar usuário: {ex.Message}");
            }
            finally
            {
                Desconectar();
            }
        }
        public void Delete(int id)
        {
            SqlTransaction transacao = null;
            try
            {
                Conectar();
                transacao = connection.BeginTransaction();
                //Excluir AgendamentoServico
                using (SqlCommand cmd1 = new SqlCommand(@"DELETE FROM AgendamentoServico 
                                                  WHERE IdAgendamento 
                                                  IN(SELECT IdAgendamento 
                                                  FROM Agendamento WHERE IdUsuarioCadastro = @id)", connection, transacao))
                {
                    cmd1.Parameters.AddWithValue("@id", id);
                    cmd1.ExecuteNonQuery();
                }

                //Excluir Agendamento
                using (SqlCommand cmd2 = new SqlCommand(@"DELETE FROM Agendamento
                                                  WHERE IdUsuarioCadastro = @id", connection, transacao))
                {
                    cmd2.Parameters.AddWithValue("@id", id);
                    cmd2.ExecuteNonQuery();
                }

                //Excluir Usuario
                using (SqlCommand cmd3 = new SqlCommand(@"DELETE FROM Usuario
                                                  WHERE IdUsuario = @id", connection, transacao))
                {
                    cmd3.Parameters.AddWithValue("@id", id);
                    cmd3.ExecuteNonQuery();
                }
            
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao buscar usuário: {ex.Message}");
            }
        }


    }
}

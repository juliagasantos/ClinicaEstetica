using System;
using System.Data.SqlClient;

namespace ClinicaEstetica.DAL
{
    public class Conexao
    {
        protected SqlConnection connection;
        protected SqlCommand command;
        protected SqlDataReader dataReader;

        protected void Conectar()
        {
            try
            {
                connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClinicaEstetica;Integrated Security=True");
                connection.Open();
            }
            catch (Exception error)
            {
                throw new Exception("Erro ao conectar ao banco de dados: " + error.Message);
            }
        }


        protected void Desconectar()
        {
            try
            {
                connection.Close();
            }
            catch (Exception error)
            {
                throw new Exception("Erro ao se desconectar do banco de dados: " + error.Message);
            }
        }




    }
}

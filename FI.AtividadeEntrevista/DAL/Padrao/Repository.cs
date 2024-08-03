using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FI.AtividadeEntrevista.DAL
{
    public abstract class Repository
    {
        private string StringDeConexao
        {
            get
            {
                ConnectionStringSettings conn = ConfigurationManager.ConnectionStrings["BancoDeDados"];
                return conn?.ConnectionString ?? string.Empty;
            }
        }

        protected void Executar(string nomeProcedure, List<SqlParameter> parametros)
        {
            using (SqlConnection conexao = new SqlConnection(StringDeConexao))
            {
                using (SqlCommand comando = new SqlCommand(nomeProcedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddRange(parametros.ToArray());

                    conexao.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        protected DataSet Consultar(string nomeProcedure, List<SqlParameter> parametros)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conexao = new SqlConnection(StringDeConexao))
            {
                using (SqlCommand comando = new SqlCommand(nomeProcedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddRange(parametros.ToArray());

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        conexao.Open();
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }
    }
}

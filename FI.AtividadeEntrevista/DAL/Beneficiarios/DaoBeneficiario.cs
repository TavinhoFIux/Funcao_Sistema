using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Data;

namespace FI.AtividadeEntrevista.DAL
{
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo Beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de Beneficiario</param>
        internal Beneficiario Incluir(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", beneficiario.IdCliente));

            DataSet ds = base.Consultar("FI_SP_IncBeneficiarioV2", parametros);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                return new Beneficiario
                {
                    Id = Convert.ToInt64(row["ID"]),
                    Nome = row["NOME"].ToString(),
                    CPF = row["CPF"].ToString(),
                    IdCliente = Convert.ToInt64(row["IDCLIENTE"])
                };
            }

            return null;
        }

        internal List<Beneficiario> BuscarBeneficiariosPorIdCliente(long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", idCliente));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiariosPorIdCliente", parametros);

            List<Beneficiario> beneficiarios = new List<Beneficiario>();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    beneficiarios.Add(new Beneficiario
                    {
                        Id = Convert.ToInt64(row["ID"]),
                        Nome = row["NOME"].ToString(),
                        CPF = row["CPF"].ToString(),
                        IdCliente = Convert.ToInt64(row["IDCLIENTE"])
                    });
                }
            }

            return beneficiarios;
        }


        internal void Excluir(long id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("Id", id)
            };

            base.Executar("FI_SP_DeleteBeneficiario", parametros);
        }

        internal void Alterar(long id, string nome, string cpf)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("Id", id),
                new System.Data.SqlClient.SqlParameter("Nome", nome),
                new System.Data.SqlClient.SqlParameter("CPF", cpf)
            };

            base.Executar("FI_SP_AltBeneficiario", parametros);
        }
    }
}

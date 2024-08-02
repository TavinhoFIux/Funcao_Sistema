CREATE PROCEDURE [dbo].[FI_SP_VerificarCpfCadastrado]
    @CPF VARCHAR(11),
    @IdCliente BIGINT
AS
BEGIN
    SELECT 
        CASE 
            WHEN EXISTS (
                SELECT 1
                FROM BENEFICIARIOS
                WHERE CPF = @CPF
                  AND IDCLIENTE = @IdCliente
            ) THEN 1
            ELSE 0
        END AS CpfCadastrado;
END
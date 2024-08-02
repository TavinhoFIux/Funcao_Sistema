CREATE PROCEDURE [dbo].[FI_SP_DeleteBeneficiario]
    @Id BIGINT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM BENEFICIARIOS WHERE ID = @Id)
    BEGIN
        DELETE FROM BENEFICIARIOS WHERE ID = @Id;
    END
    ELSE
    BEGIN
        RAISERROR('Beneficiário não encontrado.', 16, 1);
    END
END
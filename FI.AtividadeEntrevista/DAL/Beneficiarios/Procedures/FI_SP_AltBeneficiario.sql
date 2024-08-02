CREATE PROCEDURE FI_SP_AltBeneficiario
    @ID BIGINT,
    @NOME VARCHAR(50),
    @CPF VARCHAR(11)
AS
BEGIN
    -- Verificar se o benefici�rio existe
    IF EXISTS (SELECT 1 FROM BENEFICIARIOS WHERE ID = @ID)
    BEGIN
        -- Atualizar o nome e o CPF do benefici�rio
        UPDATE BENEFICIARIOS
        SET NOME = @NOME,
            CPF = @CPF
        WHERE ID = @ID;
        
        -- Retornar sucesso
        SELECT 'Benefici�rio atualizado com sucesso.' AS Message;
    END
    ELSE
    BEGIN
        -- Retornar erro se o benefici�rio n�o existir
        SELECT 'Benefici�rio n�o encontrado.' AS Message;
    END
END
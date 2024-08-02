CREATE PROCEDURE FI_SP_AltBeneficiario
    @ID BIGINT,
    @NOME VARCHAR(50),
    @CPF VARCHAR(11)
AS
BEGIN
    -- Verificar se o beneficiário existe
    IF EXISTS (SELECT 1 FROM BENEFICIARIOS WHERE ID = @ID)
    BEGIN
        -- Atualizar o nome e o CPF do beneficiário
        UPDATE BENEFICIARIOS
        SET NOME = @NOME,
            CPF = @CPF
        WHERE ID = @ID;
        
        -- Retornar sucesso
        SELECT 'Beneficiário atualizado com sucesso.' AS Message;
    END
    ELSE
    BEGIN
        -- Retornar erro se o beneficiário não existir
        SELECT 'Beneficiário não encontrado.' AS Message;
    END
END
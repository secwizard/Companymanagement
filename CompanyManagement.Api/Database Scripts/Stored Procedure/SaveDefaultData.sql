GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'DefaultData') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE DefaultData
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- ====================================Save=========
CREATE PROCEDURE [dbo].[DefaultData]
	-- Add the parameters for the stored procedure here
	@companyId int,
	@userId UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    --Save Tax name for company--
	if not Exists(select TaxNameId from TaxName where CompanyId=@companyId)
	begin
	insert into TaxName (Tax1Name,Tax2Name,Tax3Name,Tax4Name,Tax5Name,companyid,
	CreatedOnUTC,CreatedBy,UpdatedOnUTC,UpdatedBy)
	select Tax1Name,Tax2Name,Tax3Name,Tax4Name,Tax5Name,@companyId,
	GETUTCDATE(),@userId,GETUTCDATE(),@userId
	from TaxName where CompanyId is null
	end

END
GO

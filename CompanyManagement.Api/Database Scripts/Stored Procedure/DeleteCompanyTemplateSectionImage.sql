GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'DeleteCompanyTemplateSectionImage') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE DeleteCompanyTemplateSectionImage
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteCompanyTemplateSectionImage] 
	-- Add the parameters for the stored procedure here
	@CompanyTemplateSectionImageMappingId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;
	declare @CompanyTemplateSectionId int, @ItemId bigint;

	select @CompanyTemplateSectionId=[CompanyTemplateSectionId], @ItemId=[ItemId] from [dbo].[CompanyTemplateSectionImageMapping] where [CompanyTemplateSectionImageMappingId]=@CompanyTemplateSectionImageMappingId;

	delete [dbo].[CompanyTemplateSectionImageMapping] where [CompanyTemplateSectionImageMappingId]=@CompanyTemplateSectionImageMappingId;

	if not exists (select 1 from [dbo].[CompanyTemplateSectionImageMapping] where [CompanyTemplateSectionId]=@CompanyTemplateSectionId and [ItemId]=@ItemId)
	begin
	delete [dbo].[CompanyTemplateSectionItemMapping] where [CompanyTemplateSectionId]=@CompanyTemplateSectionId and [ItemId]=@ItemId;
	end
END
GO

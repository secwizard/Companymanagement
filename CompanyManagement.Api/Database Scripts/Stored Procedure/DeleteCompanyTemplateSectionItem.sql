GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'DeleteCompanyTemplateSectionItem') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE DeleteCompanyTemplateSectionItem
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteCompanyTemplateSectionItem]
	-- Add the parameters for the stored procedure here
	@CompanyTemplateSectionItemMappingId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;
	declare @CompanyTemplateSectionId int, @ItemId bigint;

	select @CompanyTemplateSectionId=[CompanyTemplateSectionId], @ItemId=[ItemId] from [dbo].[CompanyTemplateSectionItemMapping] where [CompanyTemplateSectionItemMappingId]=@CompanyTemplateSectionItemMappingId;

    delete [dbo].[CompanyTemplateSectionItemMapping] where [CompanyTemplateSectionItemMappingId]=@CompanyTemplateSectionItemMappingId;

	if not exists (select 1 from [dbo].[CompanyTemplateSectionItemMapping] where [CompanyTemplateSectionId]=@CompanyTemplateSectionId and [ItemId]=@ItemId)
	begin
	delete [dbo].[CompanyTemplateSectionImageMapping] where [CompanyTemplateSectionId]=@CompanyTemplateSectionId and [ItemId]=@ItemId;
	end
END
GO

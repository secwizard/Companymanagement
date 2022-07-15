GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SP_CompanyTemplateSectionItemMappingById') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SP_CompanyTemplateSectionItemMappingById
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CompanyTemplateSectionItemMappingById]   --SP_CompanyTemplateSectionItemMappingById  355,4747202
	-- Add the parameters for the stored procedure here
	(

 @CompanyTemplateSectionId int
    )
AS
BEGIN
	SELECT [ItemId] as ItemId
	,[CompanyTemplateSectionItemMappingId] as CompanyTemplateSectionItemMappingId
	 FROM [CompanyTemplateSectionItemMapping] where @CompanyTemplateSectionId= CompanyTemplateSectionId and IsActive = 1
END

GO

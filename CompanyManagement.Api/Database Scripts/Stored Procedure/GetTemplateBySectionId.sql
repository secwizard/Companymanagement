GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetTemplateBySectionId') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetTemplateBySectionId
END
GO
-- =============================================
-- Author:		
-- Create date: 21 SEP 2021
-- Description:	Get ItemId,VariantId
-- =============================================
CREATE PROCEDURE [dbo].[GetTemplateBySectionId]
(
	@SectionId Bigint
)
AS
--DECLARE @SectionId Bigint = 1
BEGIN
	SET NOCOUNT ON;

	SELECT
			ItemId,
			VariantId
	FROM [dbo].[CompanyTemplateSectionItemMapping] c
	WHERE c.CompanyTemplateSectionId = @SectionId
	AND (c.IsActive = 1)
END
GO

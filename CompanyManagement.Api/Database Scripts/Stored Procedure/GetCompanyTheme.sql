GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetCompanyTheme') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetCompanyTheme
END
GO
-- =============================================
-- Author:		Rajib Mukhopadhyay
-- Create date: 02 MAR 2021
-- Description:	Get company Template
-- =============================================
CREATE PROCEDURE [dbo].[GetCompanyTheme]
(
	@CompanyId Bigint
)
AS
--DECLARE @CompanyId Bigint = 1
BEGIN
	SET NOCOUNT ON;

	SELECT
			[ThemeId]
			,[CompanyId]
           ,[ThemeName]
           ,[ExtThemeName]
           ,[ImageRatio]
           ,[NoOfHomePanels]
           ,[Colour]
           ,[MobileHeight]
           ,[DesktopHeight]
           ,[IsDefault]
           ,[IsActive]
           ,[CreatedDate]
           ,[CreatedBy]
           ,[ModifiedDate]
           ,[ModifiedBy]
	FROM [dbo].[Theme] c
	WHERE c.CompanyId = @CompanyId
	AND (c.IsActive = 1)
END
GO

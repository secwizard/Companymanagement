GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetCompanySettings') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetCompanySettings
END
GO
-- =============================================
-- Author:		Rajib Mukhopadhyay
-- Create date: 19 Feb 2021
-- Description:	Get company Settings
-- =============================================
CREATE PROCEDURE [dbo].[GetCompanySettings]
(
	@CompanyId Bigint
	, @SettingType nvarchar(50) = ''
	, @DataText nvarchar(50) = ''
)
AS
--DECLARE @CompanyId Bigint = 1, @SettingType nvarchar(50)='', @DataText nvarchar(50)=''
BEGIN
	SET NOCOUNT ON;

	SELECT
		[CompanySettingId] 
		, [CompanyId]
		, [SettingType]
		, [DataText]
		, [DataValue]
		, [Option1]
		, [Option2]
		, [Option3]
		, [IsActive]
		, [CreatedBy]
	FROM [dbo].[CompanySetting] c
	WHERE c.CompanyId = @CompanyId
	AND (c.IsActive = 1)
	AND (c.SettingType = @SettingType OR ISNULL(@SettingType,'') = '')
	AND (c.[DataText] = @DataText OR ISNULL(@DataText,'') = '')
END
GO

GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetSuggestedCompanyId') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetSuggestedCompanyId
END
GO
-- =============================================
-- Author:		Rajib Mukhopadhyay
-- Create date: 19 Feb 2021
-- Description:	Get company Settings
-- =============================================
CREATE PROCEDURE [dbo].[GetSuggestedCompanyId]
(

	 @Type nvarchar(50) = ''
)
AS
--DECLARE @Type nvarchar(50)= 'B2B'
BEGIN
	SET NOCOUNT ON;
		DECLARE @CompanyId BIGINT= 0;
	SET @CompanyId =(SELECT MAX(CompanyId) 
	FROM
		Company
	WHERE BusinessType = @Type)
	IF(@CompanyId is NULL and @Type ='B2B')
	BEGIN
		SET @CompanyId = 10000
	END
	ELSE IF(@CompanyId is NULL and @Type ='B2C')
	BEGIN
		SET @CompanyId = 0
	END
	SELECT CompanyId = @CompanyId
END
GO

GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetCompanyTemplate') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetCompanyTemplate
END
GO
-- =============================================
-- Author:		Rajib Mukhopadhyay
-- Create date: 02 MAR 2021
-- Description:	Get company Template
-- =============================================
CREATE PROCEDURE [dbo].[GetCompanyTemplate]
(
	@CompanyId Bigint
)
AS
--DECLARE @CompanyId Bigint = 1
BEGIN
	SET NOCOUNT ON;

	SELECT
			[TemplateId]
		   ,[CompanyId]
           ,[TemplateType]
           ,[Name]
           ,[Title]
           ,[HTMLData]
           ,[IsActive]
           ,[CreatedDate]
           ,[CreatedBy]
           ,[ModifiedDate]
		   ,[ModifiedBy]
	FROM [dbo].[Template] c
	WHERE c.CompanyId = @CompanyId
	AND (c.IsActive = 1)
END
GO

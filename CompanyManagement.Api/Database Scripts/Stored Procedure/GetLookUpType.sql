GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetLookUpType') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetLookUpType
END
GO
-- =============================================
-- Author:		Rajib Mukhopadhyay
-- Create date: 0 MAR 2021
-- Description:	Get company Template
-- =============================================
CREATE PROCEDURE [dbo].[GetLookUpType]
(
	@LookUpType nvarchar(200)
)
AS
--DECLARE @LookUpType nvarchar(200) = 'BusinessType'
BEGIN
	SET NOCOUNT ON;

	SELECT
		   [LookUpId]
		   ,[LookUpType]
		   ,[LookUpValue]
		   ,[LookUpDescription]
           ,[IsActive]
           ,[CreatedDate]
           ,[CreatedBy]
           ,[ModifiedDate]
	FROM [dbo].[LookUp] c
	WHERE c.LookUpType = @LookUpType
	AND (c.IsActive = 1)
END
GO

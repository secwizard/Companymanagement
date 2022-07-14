GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SP_GetTemplateSectionTypeMaster') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SP_GetTemplateSectionTypeMaster
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetTemplateSectionTypeMaster]  
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	SELECT [TemplateSectionType] as TemplateSectionType
      ,[TemplateSectionName] as TemplateSectionName
      ,[HelpLink] as HelpLink
  FROM [dbo].[TemplateSectionTypeMaster]
	
END

GO

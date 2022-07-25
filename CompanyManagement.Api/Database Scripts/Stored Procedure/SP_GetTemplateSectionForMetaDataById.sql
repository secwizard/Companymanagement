GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SP_GetTemplateSectionForMetaDataById') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SP_GetTemplateSectionForMetaDataById
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetTemplateSectionForMetaDataById] 
	-- Add the parameters for the stored procedure here
    
AS
BEGIN
	SELECT [TemplateSectionForId] as TemplateSectionForId,
	[TemplateSectionForName] as TemplateSectionForName

    FROM [TemplateSectionForMetaData] where  TemplateSectionForId != 3
 
END

GO

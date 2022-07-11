GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SP_GetZoneSettingByZoneId') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SP_GetZoneSettingByZoneId
END
GO
     
CREATE PROCEDURE [dbo].[SP_GetZoneSettingByZoneId]      
    @ZoneId INT   
AS      
-- =============================================      
-- Author: Kuntal Das      
-- Create date: 08 March 2022      
-- Description:  Get  ZoneSetting value against ZoneId     
-- =============================================      
BEGIN      
 SELECT [ZoneId]  
      ,[CompanyId] 
	  ,[FromPostalCode]
	  ,[ToPostalCode]
      ,[ZoneName]  
      ,[PatternValue]  
      ,[PatternType]  
      ,[Isdefault]  
      ,[IsActive]  
      ,[CreatedBy]  
      ,[UpdatedBy]  
      ,[CreatedOnUTC]  
      ,[UpdatedOnUTC]  
  FROM [dbo].[ZoneSetting] WHERE  [ZoneId]=@ZoneId AND IsActive=1  
END
GO

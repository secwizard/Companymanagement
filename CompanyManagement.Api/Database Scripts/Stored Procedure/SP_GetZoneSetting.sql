GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SP_GetZoneSetting') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SP_GetZoneSetting
END
GO
     
CREATE PROCEDURE [dbo].[SP_GetZoneSetting]      
    @CompanyId INT   
AS      
-- =============================================      
-- Author: Kuntal Das      
-- Create date: 07 March 2022      
-- Description:  Get all ZoneSetting value against ComapnyId     
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
  FROM [dbo].[ZoneSetting] WHERE  CompanyId=@CompanyId AND IsActive=1  
END
GO

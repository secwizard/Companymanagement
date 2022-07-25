GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetCompanyTemplateByTypeAndName') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetCompanyTemplateByTypeAndName
END
GO
-- =============================================    
-- Author:  Rajib Mukhopadhyay    
-- Create date: 02 MAR 2021    
-- Description: Get company Template    
-- =============================================    
CREATE PROCEDURE [dbo].[GetCompanyTemplateByTypeAndName]    
(    
 @CompanyId Bigint,   
 @TemplateType nvarchar(max),  
 @Name nvarchar(max)     
)    
AS    
--DECLARE @CompanyId Bigint = 1    
BEGIN    
 SET NOCOUNT ON;    
    
 SELECT    
 [TemplateId]    
 ,c.[CompanyId]    
 ,[TemplateType]    
 ,c.[Name]    
 ,[Title]    
 ,c.[HTMLData]    
 ,c.[IsActive]    
 ,c.[CreatedDate]    
 ,c.[CreatedBy]    
 ,c.[ModifiedDate]    
    ,c.[ModifiedBy]  ,
	co.AdminPhoneCode  
 FROM [dbo].[Template] c  
 join dbo.Company co on c.CompanyId=co.CompanyId  
 WHERE c.CompanyId = @CompanyId    
 AND c.TemplateType=@TemplateType  
 AND c.Name=@Name  
 AND (c.IsActive = 1)    
END 
GO
